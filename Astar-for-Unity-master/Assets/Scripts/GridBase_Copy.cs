using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GridMaster
{
    public class GridBase_Copy : MonoBehaviour
    {
        private int isFirst = 0;

        public LineRenderer linerederer_Recommend;//라인렌더러 저장할 변수
        public LineRenderer linerederer_Shortest;//라인렌더러 저장할 변수

        //Setting up the grid
        public int maxX = 10;
        public int maxY = 3;
        public int maxZ = 10;

        //Offset relates to the world positions only
        public float offsetX = 1;
        public float offsetY = 1;
        public float offsetZ = 1;

        public Node[,,] grid; // our grid

        public GameObject gridFloorPrefab;
        public GameObject Drone;
        public Vector3 startNodePosition;
        private Vector3 midNodePosition1;
        public Vector3 endNodePosition;
        public Vector3 midNodePosition2;
        private Vector3 realStartPosition;

        public int agents;

        void Start()
        {

            startNodePosition = Drone.transform.position;
            //The typical way to create a grid
            grid = new Node[maxX, maxY, maxZ];

            //포물선 모양을 만들기 위해 중간지점 위치를 높게 선정
            midNodePosition1 = startNodePosition;
            midNodePosition1.y = maxY - 1;
            midNodePosition2 = endNodePosition;
            midNodePosition2.y = maxY - 1;
            Debug.Log(midNodePosition1.x + " " + midNodePosition1.y + " " + midNodePosition1.z);
            Debug.Log(midNodePosition2.x + " " + midNodePosition2.y + " " + midNodePosition2.z);


            for (int x = 0; x < maxX; x++)
            {
                for (int y = 0; y < maxY; y++)
                {
                    for (int z = 0; z < maxZ; z++)
                    {
                        //Apply the offsets and create the world object for each node
                        float posX = x * offsetX;
                        float posY = y * offsetY;
                        float posZ = z * offsetZ;
                        GameObject go = Instantiate(gridFloorPrefab, new Vector3(posX, posY, posZ),
                            Quaternion.identity) as GameObject;
                        //Rename it
                        go.transform.name = x.ToString() + " " + y.ToString() + " " + z.ToString();
                        //and parent it under this transform to be more organized
                        go.transform.parent = transform;

                        //Create a new node and update it's values
                        Node node = new Node();
                        node.x = x;
                        node.y = y;
                        node.z = z;
                        node.worldObject = go;

                        //BoxCastAll is only Unity 5.3+ remove this and it will play on all versions 5+
                        //in theory it should play with every Unity version, but i haven't tested it
                        RaycastHit[] hits = Physics.BoxCastAll(new Vector3(posX, posY, posZ), new Vector3(1, 0, 1), Vector3.forward);

                        for (int i = 0; i < hits.Length; i++)
                        {
                            node.isWalkable = false;
                        }

                        //then place it to the grid
                        grid[x, y, z] = node;
                    }
                }
            }

        }
        //path 표시하기 전에 걍 grid전부setactive(true) 해주면 되지 않나
        //Just a quick and dirty way to visualize the path
        public bool start;
        void Update()
        {
            if (isFirst == 0)
            {
                startNodePosition = Drone.transform.position;
                realStartPosition = startNodePosition;
                midNodePosition1 = startNodePosition;
                midNodePosition1.y = maxY - 1;
                midNodePosition2 = endNodePosition;
                midNodePosition2.y = maxY - 1;

                grid[1, 0, 1].isWalkable = false;

                //pass the target nodes
                Node startNode = GetNodeFromVector3(startNodePosition);
                Node end = GetNodeFromVector3(endNodePosition);
                Node mid1 = GetNodeFromVector3(midNodePosition1);
                Node mid2 = GetNodeFromVector3(midNodePosition2);

                for (int i = 0; i < agents; i++)
                {
                    //Pathfinding.PathfindMaster.GetInstance().RequestPathfind(startNode, end, ShowPath);
                    Pathfinding.PathfindMaster.GetInstance().RequestPathfind(mid1, mid2, ShowPath_Shortest);


                }
                //for문돌려서 0층~꼭대기층까지 position 계산한 걸 getnodefromvector3로 node로 바꾸고 해당 노드를 true로 바꾼다
                for (int tmpY = 0; tmpY < maxY; tmpY++)
                {
                    for (int tmpX = 0; tmpX < maxX; tmpX++)
                    {
                        for (int tmpZ = 0; tmpZ < maxZ; tmpZ++)
                        {
                            Vector3 tmpVec = new Vector3(tmpX, tmpY, tmpZ);
                            Node tmpNode = GetNodeFromVector3(tmpVec);
                            tmpNode.worldObject.SetActive(true);
                        }
                    }
                }
                isFirst = 1;
            }

            startNodePosition = Drone.transform.position;
            midNodePosition1 = startNodePosition;
            midNodePosition1.y = maxY - 1;
            midNodePosition2 = endNodePosition;
            midNodePosition2.y = maxY - 1;

            if (start)
            {

                start = false;
                //Create the new pathfinder class
                // Pathfinding.Pathfinder path = new Pathfinding.Pathfinder();

                //to test the avoidance, just make a node unwalkable
                grid[1, 0, 1].isWalkable = false;

                //pass the target nodes
                Node startNode = GetNodeFromVector3(startNodePosition);
                Node end = GetNodeFromVector3(endNodePosition);
                Node mid1 = GetNodeFromVector3(midNodePosition1);
                Node mid2 = GetNodeFromVector3(midNodePosition2);
                //path.startPosition = startNode;
                //path.endPosition = end;
                //for문돌려서 0층~꼭대기층까지 position 계산한 걸 getnodefromvector3로 node로 바꾸고 해당 노드를 true로 바꾼다
                for (int tmpY = 0; tmpY < maxY; tmpY++)
                {
                    for (int tmpX = 0; tmpX < maxX; tmpX++)
                    {
                        for (int tmpZ = 0; tmpZ < maxZ; tmpZ++)
                        {
                            Vector3 tmpVec = new Vector3(tmpX, tmpY, tmpZ);
                            Node tmpNode = GetNodeFromVector3(tmpVec);
                            tmpNode.worldObject.SetActive(true);
                        }
                    }
                }
                //find the path
                //List<Node> p = path.FindPath();
                //startNode.worldObject.SetActive(false);

                for (int i = 0; i < agents; i++)
                {
                    //Pathfinding.PathfindMaster.GetInstance().RequestPathfind(startNode, end, ShowPath);
                    Pathfinding.PathfindMaster.GetInstance().RequestPathfind(mid1, mid2, ShowPath_Recommend);


                }
                end.worldObject.SetActive(false);
            }
        }

        public void ShowPath_Shortest(List<Node> path)
        {
            linerederer_Shortest.positionCount = 0;
            linerederer_Shortest.SetPosition(linerederer_Shortest.positionCount, realStartPosition);
            Vector3 v3;
            foreach (Node n in path)
            {
                v3.x = n.x;
                v3.y = n.y;
                v3.z = n.z;
                linerederer_Shortest.positionCount = linerederer_Shortest.positionCount + 1; //라인렌더러 포지션 값이 호출되면 포지션 카운트 값을 1씩 증가시킨다.
                linerederer_Shortest.SetPosition(linerederer_Shortest.positionCount - 1, v3);
                n.worldObject.SetActive(false);

            }
            linerederer_Shortest.positionCount = linerederer_Shortest.positionCount + 1; //라인렌더러 포지션 값이 호출되면 포지션 카운트 값을 1씩 증가시킨다.
            linerederer_Shortest.SetPosition(linerederer_Shortest.positionCount - 1, endNodePosition);
            //Debug.Log("agent complete");
        }

        public void ShowPath_Recommend(List<Node> path)
        {
            linerederer_Recommend.positionCount = 0;
            linerederer_Recommend.SetPosition(linerederer_Recommend.positionCount, Drone.transform.position);
            Vector3 v3;
            foreach (Node n in path)
            {
                v3.x = n.x;
                v3.y = n.y;
                v3.z = n.z;
                linerederer_Recommend.positionCount = linerederer_Recommend.positionCount + 1; //라인렌더러 포지션 값이 호출되면 포지션 카운트 값을 1씩 증가시킨다.
                linerederer_Recommend.SetPosition(linerederer_Recommend.positionCount - 1, v3);
                n.worldObject.SetActive(false);

            }
            linerederer_Recommend.positionCount = linerederer_Recommend.positionCount + 1; //라인렌더러 포지션 값이 호출되면 포지션 카운트 값을 1씩 증가시킨다.
            linerederer_Recommend.SetPosition(linerederer_Recommend.positionCount - 1, endNodePosition);


            //Debug.Log("agent complete");
        }

        public Node GetNode(int x, int y, int z)
        {
            //Used to get a node from a grid,
            //If it's greater than all the maximum values we have
            //then it's going to return null

            Node retVal = null;

            if (x < maxX && x >= 0 &&
                y >= 0 && y < maxY &&
                z >= 0 && z < maxZ)
            {
                retVal = grid[x, y, z];
            }

            return retVal;
        }

        public Node GetNodeFromVector3(Vector3 pos)
        {
            int x = Mathf.RoundToInt(pos.x);
            int y = Mathf.RoundToInt(pos.y);
            int z = Mathf.RoundToInt(pos.z);

            Node retVal = GetNode(x, y, z);
            return retVal;
        }

        //Singleton
        public static GridBase_Copy instance;
        public static GridBase_Copy GetInstance()
        {
            return instance;
        }

        void Awake()
        {
            instance = this;
        }
    }
}
