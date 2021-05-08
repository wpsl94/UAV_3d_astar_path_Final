using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GridMaster;

    public class FindWalkable : MonoBehaviour
    {
        public GridBase MyGridBase;
        Vector3 LeftUp;
        Vector3 LeftDown;
        Vector3 RightUp;
        Vector3 RightDown;
        Vector3 MyPosition;
        Vector3 MyScale;
        //x축이 Left/Right
        //Z축이 Up/Down
        // Start is called before the first frame update
        void Start()
        {/*
        MyPosition = transform.position;
        MyScale = transform.localScale;
        RightUp.x = MyPosition.x + (MyScale.x / 2);
        RightUp.z = MyPosition.z + (MyScale.z / 2);

        RightDown.x= MyPosition.x + (MyScale.x / 2);
        RightDown.z = MyPosition.z - (MyScale.x / 2);

        LeftUp.x = MyPosition.x - (MyScale.x / 2);
        LeftUp.z = MyPosition.z + (MyScale.z / 2);

        LeftDown.x = MyPosition.x - (MyScale.x / 2);
        LeftDown.z = MyPosition.z - (MyScale.z / 2);
        Debug.Log(RightDown.x + "," + RightDown.z + "," + LeftDown.x + "," + LeftDown.z);
        //RightUp.x = RightUp.x - MyGridBase.transform.position.x;
        //LeftUp.x = LeftUp.x - MyGridBase.transform.position.x;
        int xMax = Mathf.RoundToInt(RightUp.x);
        int xMin = Mathf.RoundToInt(LeftUp.x);

        int zMax = Mathf.RoundToInt(RightUp.z);
        int zMin = Mathf.RoundToInt(RightDown.z);
        Debug.Log(xMax + "," + xMin + "," + zMax + "," + zMin+",,,"+MyGridBase.maxY);
        for (int x=xMin; x<=xMax; x++)
        {
            for(int z=zMin; z <= zMax; z++)
            {
                for(int y = 0; y < MyGridBase.maxY; y++)
                {
                    Debug.Log(x + "," + y + "," + z);
                    //Vector3 tmpV = new Vector3(fx,fy,fz);
                    MyGridBase.grid[x, y, z].isWalkable = false;
                    if (MyGridBase.grid[x, y, z].isWalkable) Debug.Log(x + "," + y + "," +z + " Walkable");
                    if (!MyGridBase.grid[x, y, z].isWalkable) Debug.Log(x + "," + y + "," + z + "Not Walkable");
                    //Debug.Log("for test");
                }
            }
        }
    }
    */
        }
        private void Update()
        {
            MyPosition = transform.position;
            MyScale = transform.localScale;
            RightUp.x = MyPosition.x + (MyScale.x / 2);
            RightUp.z = MyPosition.z + (MyScale.z / 2);

            RightDown.x = MyPosition.x + (MyScale.x / 2);
            RightDown.z = MyPosition.z - (MyScale.x / 2);

            LeftUp.x = MyPosition.x - (MyScale.x / 2);
            LeftUp.z = MyPosition.z + (MyScale.z / 2);

            LeftDown.x = MyPosition.x - (MyScale.x / 2);
            LeftDown.z = MyPosition.z - (MyScale.z / 2);
            Debug.Log(RightDown.x + "," + RightDown.z + "," + LeftDown.x + "," + LeftDown.z);
            //RightUp.x = RightUp.x - MyGridBase.transform.position.x;
            //LeftUp.x = LeftUp.x - MyGridBase.transform.position.x;
            int xMax = Mathf.RoundToInt(RightUp.x);
            int xMin = Mathf.RoundToInt(LeftUp.x);

            int zMax = Mathf.RoundToInt(RightUp.z);
            int zMin = Mathf.RoundToInt(RightDown.z);
            Debug.Log(xMax + "," + xMin + "," + zMax + "," + zMin + ",,," + MyGridBase.maxY);
            for (int x = xMin; x <= xMax; x++)
            {
                for (int z = zMin; z <= zMax; z++)
                {
                    for (int y = 0; y < MyGridBase.maxY; y++)
                    {
                        Debug.Log(x + "," + y + "," + z);
                        //Vector3 tmpV = new Vector3(fx,fy,fz);
                        MyGridBase.grid[x, y, z].isWalkable = false;
                        if (MyGridBase.grid[x, y, z].isWalkable) Debug.Log(x + "," + y + "," + z + " Walkable");
                        if (!MyGridBase.grid[x, y, z].isWalkable) Debug.Log(x + "," + y + "," + z + "Not Walkable");
                        //Debug.Log("for test");
                    }
                }
            }
        }

    }

