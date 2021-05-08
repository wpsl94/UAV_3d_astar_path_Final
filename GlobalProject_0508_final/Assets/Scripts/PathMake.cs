using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class PathMake : MonoBehaviour
{
    
    public LineRenderer linerederer;//라인렌더러 저장할 변수
    public GameObject lineprefabs;//프리팹화 시킨 라인 저장할 용도
    public string[] values;//좌표 x,y,z를 저장할 배열
    public string[] vl;//좌표값이 몇 줄 존재하는지 저장할 배열
    public float tx;//string형태의 좌표를 float형태로 바꾸기 위해 사용될 float변수들
    public float ty;
    public float tz;
    public Vector3 v3;//float형태의 좌표를 vector3형태로 바꾸고 저장할 변수   
 
    void Start()
    {
        MakePath();
    }
 
    public void MakePath()
    {              
        string dronepath = Application.dataPath + @"\Text\BiconPath.txt";//좌표값이 적혀있는 txt파일 위치
        
        GameObject pLine = Instantiate(lineprefabs);//프리팹으로 저장한 라인 게임 오브젝트로 나타냄
 
        linerederer = pLine.GetComponent<LineRenderer>();//새로 생성된 오브젝트의 라인렌더러 컴포넌트 값 가져옴
        linerederer.positionCount = 0;//포지션 카운트값을 0으로 초기화 -> 초기값 연동가능하게
        
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
                    values=str.Split('\t'); // 텍스트에서 한줄에 x,y,z 좌표값이 ,로 나뉘어져 있기에 ,로 나뉜 좌표값들을 따로 따로 읽어낸다.                  
                }
                
                //Debug.Log(values[0]+','+ values[1] + ',' + values[2]); //-> txt파일의 좌표와 유니티 상에 표시되는 좌표가 일치하는지 확인
                
                tz = float.Parse(values[0]); //string 값을 float 값으로 변환
                ty = float.Parse(values[1]);
                tx = float.Parse(values[2]);

                
                if(tx == 0 || ty==0 || tz == 0)
                {
                    continue;
                }
                

                v3 = new Vector3(1, 1, 1); //float값을 vector3값으로 변환
                v3.x = v3.x * tx;
                v3.y = v3.y * ty;
                v3.z = v3.z * tz;
                
                linerederer.positionCount = linerederer.positionCount + 1; //라인렌더러 포지션 값이 호출되면 포지션 카운트 값을 1씩 증가시킨다.
                linerederer.SetPosition(linerederer.positionCount - 1, v3); //라인렌더러 초기 포지션 카운트값에 v3값을 연동          
                
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
}
