using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using GridMaster;
public class MoveDrone : MonoBehaviour
{
    public GridBase gridBase_Drone;
    Rigidbody RigidDrone;
    Rigidbody RigidBuilding;
    Vector3 DronePosition;
    //RaycastHit hit;
    bool isCollided = false;
    bool noFlyZone = false;
    bool noFlyZoneStay = false;
    float maxDistance = 0.1f;
    float timer = 0.0f;
    int waitingTime = 2;
    Vector3 originalPosition_Building;

    public GameObject Building;
    public TextMeshProUGUI message;
    static float moveSpeed = 0.5f;


    // Start is called before the first frame update
    void Start()
    {
        
        RigidDrone = GetComponent<Rigidbody>();
        
        RigidBuilding = Building.GetComponent<Rigidbody>();
        originalPosition_Building = Building.transform.position;
        
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        
        while (isCollided)
        {
            message.text = "Collision!" + "\n" + "Drone can't move for 2 seconds"; //충돌 메세지 출력
            RigidDrone.constraints = RigidbodyConstraints.FreezePosition;
            //RigidBuilding.isKinematic = true;
            //빌딩 넘어지지 않게 고정, 드론 못 움직이게 고정
            Invoke("Clear", 2);
            //2초 뒤 고정 해제

            isCollided = false; //while문 탈출

        }

        Debug.Log("Escaped while");
        
    }
    //Invoke = 2초 뒤 Clear 함수 호출로 constraints 해제, 텍스트 초기화


    private void OnTriggerStay(Collider other)
    {

        if (other.gameObject.tag == "NoFlyZone")
            message.text = "You are in the no Fly Zone now";
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "NoFlyZone")
            message.text = "";
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Building")
        {
            isCollided = true;
        }
    }
    private void ClearText()
    {
        message.text = "";
    }
    private void Clear()
    {
        message.text = "";
        RigidDrone.constraints = RigidbodyConstraints.None;

        //RigidBuilding.isKinematic = false;


    }
    
    public void Movement()
    {
        if (Input.GetKey(KeyCode.I))//앞으로 이동
        {
            DronePosition += Vector3.forward * moveSpeed * Time.deltaTime;
            RigidDrone.MovePosition(DronePosition);

        }
        if (Input.GetKey(KeyCode.K))//뒤
        {
            DronePosition -= Vector3.forward * moveSpeed * Time.deltaTime;
            RigidDrone.MovePosition(DronePosition);
        }
        if (Input.GetKey(KeyCode.J))//왼쪽
        {
            DronePosition += Vector3.left * moveSpeed * Time.deltaTime;
            RigidDrone.MovePosition(DronePosition);
        }
        if (Input.GetKey(KeyCode.L))//오른쪽
        {
            DronePosition += Vector3.right * moveSpeed * Time.deltaTime;
            RigidDrone.MovePosition(DronePosition);
        }
        if (Input.GetKey(KeyCode.U))//아래
        {
            DronePosition += Vector3.down * moveSpeed * Time.deltaTime;
            RigidDrone.MovePosition(DronePosition);
        }
        if (Input.GetKey(KeyCode.O))//위
        {
            DronePosition += Vector3.up * moveSpeed * Time.deltaTime;
            RigidDrone.MovePosition(DronePosition);
        }
    }
}
