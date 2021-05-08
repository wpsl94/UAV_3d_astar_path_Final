using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pastPath : MonoBehaviour
{
    public LineRenderer linerederer;//라인렌더러 저장할 변수
    public GameObject lineprefabs;//프리팹화 시킨 라인 저장할 용도;  
    public Transform pivotPoint;
    //public Transform startPoint;
    public bool use = true;
    //public bool draw = false;
    
    public void Start()
    {
        MakePath();      
    }
   
    public void Update()
    {
        DrawPath();
        /*
        if (Input.GetKey(KeyCode.I) || Input.GetKey(KeyCode.J) || Input.GetKey(KeyCode.K) || Input.GetKey(KeyCode.L) || Input.GetKey(KeyCode.U) || Input.GetKey(KeyCode.P) && use)
        {
            MakePath();
            use = false;
        }
        // 키가 입력 되어 드론이 움직이는 순간 MakePath 함수를 한 번만 호출시킴
        if (Input.GetKey(KeyCode.I) || Input.GetKey(KeyCode.J) || Input.GetKey(KeyCode.K) || Input.GetKey(KeyCode.L) || Input.GetKey(KeyCode.U) || Input.GetKey(KeyCode.P))
            DrawPath();
        // 키가 입력 되는 동안 DrawPath 함수 호출
        
        
        if (pivotPoint.position == startPoint.position && use)
        {
            MakePath();
            use = false;
            draw = true;
        }
        if (draw)
        {
            DrawPath();
        }
        왜 안될까??
        */
    }

    public void MakePath()
    {
        GameObject pLine = Instantiate(lineprefabs);//프리팹으로 저장한 라인 게임 오브젝트로 나타냄

        linerederer = pLine.GetComponent<LineRenderer>();//새로 생성된 오브젝트의 라인렌더러 컴포넌트 값 가져옴

        linerederer.positionCount = 1;
        linerederer.SetPosition(0, pivotPoint.position);//라인렌더러에 pivotPoint로 설정한 물체의 위치 연동 

    }
    public void DrawPath()
    {
        linerederer.positionCount = linerederer.positionCount + 1;
        linerederer.SetPosition(linerederer.positionCount - 1, pivotPoint.position);      
    }
}
