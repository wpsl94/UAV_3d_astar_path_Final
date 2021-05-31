using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pastPath : MonoBehaviour
{
    public LineRenderer linerederer;//라인렌더러 저장할 변수
    public GameObject lineprefabs;//프리팹화 시킨 라인 저장할 용도;  
    public Transform pivotPoint;
    private Vector3 tmp;
    public void Start()
    {
        MakePath();
    }

    public void Update()
    {
        DrawPath();
    }

    public void MakePath()
    {
        GameObject pLine = Instantiate(lineprefabs);//프리팹으로 저장한 라인 게임 오브젝트로 나타냄

        linerederer = pLine.GetComponent<LineRenderer>();//새로 생성된 오브젝트의 라인렌더러 컴포넌트 값 가져옴

        linerederer.positionCount = 1;
        tmp = pivotPoint.position;
        tmp.y -= 7;
        linerederer.SetPosition(0, tmp);//라인렌더러에 pivotPoint로 설정한 물체의 위치 연동 

    }
    public void DrawPath()
    {
        linerederer.positionCount = linerederer.positionCount + 1;
        tmp = pivotPoint.position;
        tmp.y -= 7;
        linerederer.SetPosition(linerederer.positionCount - 1, tmp);
    }
}
