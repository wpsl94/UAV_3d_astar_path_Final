using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
public class ChangePath : MonoBehaviour
{
    public LineRenderer linerederer_Recommend;//라인렌더러 저장할 변수
    public LineRenderer linerederer_Shortest;//라인렌더러 저장할 변수
    public LineRenderer linerederer_Drone;

    public Transform drone;

    public string[] values;//좌표 x,y,z를 저장할 배열
    public string[] vl;//좌표값이 몇 줄 존재하는지 저장할 배열
    public float tx;//string형태의 좌표를 float형태로 바꾸기 위해 사용될 float변수들
    public float ty;
    public float tz;
    public Vector3 v3;//float형태의 좌표를 vector3형태로 바꾸고 저장할 변수   
    public int count = 0;

    // Start is called before the first frame updat
    // Update is called once per frame
    public void clearPath()
    {
        linerederer_Recommend.SetPosition(0, Vector3.zero);
        linerederer_Recommend.SetPosition(1, Vector3.zero);
        linerederer_Shortest.SetPosition(0, Vector3.zero);
        linerederer_Shortest.SetPosition(1, Vector3.zero);
        linerederer_Drone.SetPosition(0, Vector3.zero);
        linerederer_Drone.SetPosition(1, Vector3.zero);
    }

    public void FixedUpdate()
    {
        MakePath();
    }

    public void activeFalse()
    {

    }

    public void MakePath()
    {
        TextAsset _dronepath = Resources.Load("6", typeof(TextAsset)) as TextAsset;

        StringReader sr = new StringReader(_dronepath.text);
        string str = null;

        for (int j = 0; j < count; j++)
        {
            str = sr.ReadLine();
        }

        str = sr.ReadLine();//한 줄 씩 읽기

        if (str == null)
        {
            sr.Close();
            return;
        }

        for (int i = 0; i < 3; i++)
        {
            values = str.Split('\t'); // 텍스트에서 한줄에 x,y,z 좌표값이 ,로 나뉘어져 있기에 ,로 나뉜 좌표값들을 따로 따로 읽어낸다.                  
        }

        //Debug.Log(values[0]+','+ values[1] + ',' + values[2]); //-> txt파일의 좌표와 유니티 상에 표시되는 좌표가 일치하는지 확인

        tx = (float.Parse(values[0]) * 0.03f); //string 값을 float 값으로 변환
        tz = (float.Parse(values[1]) * 0.03f);
        ty = ((float.Parse(values[2]) * 0.03f) + 0.9f);

        v3 = new Vector3(1, 1, 1); //float값을 vector3값으로 변환
        v3.x = v3.x * tx;
        v3.y = v3.y * ty;
        v3.z = v3.z * tz;

        drone.position = v3;

        count++;

        if (values.Length == 0)//읽을 줄이 존재하지 않으면 파일 닫기
        {
            sr.Close();
            return;
        }

    }

    public void loadPath()
    {

    }
}
