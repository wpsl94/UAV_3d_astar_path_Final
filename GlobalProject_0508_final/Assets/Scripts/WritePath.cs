using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class WritePath : MonoBehaviour
{
    string filePath = "Assets/Text/Path.txt";
    string filePath2 = "Assets/Text/Path";
    string DronePath;
    public GameObject Drone;

    // Start is called before the first frame update
    void Start()
    {
        //txt 파일 없으면 새로 생성
        if (false == File.Exists(filePath))
        {
            var file = File.CreateText(filePath2 + ".txt");
            file.Close();


        }
    }

    // Update is called once per frame
    
    void Update()
    {   //1프레임마다 드론의 위치 텍스트 파일에 기록
        StreamWriter sw;
        sw = File.AppendText(filePath);
        DronePath = Drone.transform.position.x.ToString() + "," + Drone.transform.position.y.ToString() + "," + Drone.transform.position.z.ToString();
        sw.WriteLine(DronePath);
        sw.Flush();
        sw.Close();
    }

}
