using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using GridMaster;
public class AutoMove : MonoBehaviour
{
    public GridBase gridBase_Drone;
    private string[] values;//좌표 x,y,z를 저장할 배열
    private string[] vl;//좌표값이 몇 줄 존재하는지 저장할 배열
    private Vector3 v3;//float형태의 좌표를 vector3형태로 바꾸고 저장할 변수
    string dronepath = "Assets/Text/pathdata.txt";//좌표값이 적혀있는 txt파일 위치
    private float tx;//string형태의 좌표를 float형태로 바꾸기 위해 사용될 float변수들
    private float ty;
    private float tz;
    private Vector3 currPosition;
    private float speed = 0.001f;
    private List<Vector3> vecArr=new List<Vector3>();
    private float timer = 0.0f;
    private int i = 0;

    // Start is called before the first frame update
    void Start()
    {
        
        if (File.Exists(dronepath))//경로에 파일이 존재하면 읽는다.
        {
            FileStream file = new FileStream(dronepath, FileMode.Open, FileAccess.Read);
            StreamReader sr = new StreamReader(file);
            string str = null;
            vl = File.ReadAllLines(dronepath);//좌표값이 몇 줄 존재하는지 읽어온다

            


            for (int j = 0; j < vl.Length; j++)
            {
                str = sr.ReadLine();//한 줄 씩 읽기

                for (int i = 0; i < 3; i++)
                {
                    values = str.Split('\t'); // 텍스트에서 한줄에 x,y,z 좌표값이 ,로 나뉘어져 있기에 ,로 나뉜 좌표값들을 따로 따로 읽어낸다.                  
                }

                //Debug.Log(values[0]+','+ values[1] + ',' + values[2]); //-> txt파일의 좌표와 유니티 상에 표시되는 좌표가 일치하는지 확인

                v3 = new Vector3(1, 1, 1); //float값을 vector3값으로 변환
                v3.x = float.Parse(values[0]) + 2;
                //if out of range occurs..
                if (v3.x > gridBase_Drone.maxX - 1) v3.x = gridBase_Drone.maxX - 1.1f;
                else if (v3.x < 0) v3.x = 0.0f;

                v3.y = float.Parse(values[1]) + 2;
                //if out of range occurs..
                if (v3.y > gridBase_Drone.maxY - 1) v3.y = gridBase_Drone.maxY - 1.1f;
                else if (v3.y < 0) v3.y = 0.0f;

                v3.z = float.Parse(values[2]) + 2;
                if (v3.z > gridBase_Drone.maxZ - 1) v3.z = gridBase_Drone.maxZ - 1.1f;
                else if (v3.z < 0) v3.z = 0.0f;

                Debug.Log(v3.x + " " + v3.y + " " + v3.z);
                vecArr.Add(v3);
               
                if (values.Length == 0)//읽을 줄이 존재하지 않으면 파일 닫기
                {
                    sr.Close();
                    return;
                }
            }

        }

        else//경로에 파일이 존재하지 않는다면 에러 표시
        {
            Debug.LogError("파일을 찾지 못하였습니다.");
            return;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        transform.position = vecArr[i];
        i++;

    }
}
