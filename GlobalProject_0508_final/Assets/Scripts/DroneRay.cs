using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DroneRay : MonoBehaviour
{
    Rigidbody RigidDrone;
    public static Vector3 DronePosition;
    static float moveSpeed = 1.0f;
    bool forwardMove, backMove, rightMove, leftMove, upMove, downMove;
    bool fr, f_r, fu, f_u, _fr, _f_r, _fu, _f_u, ru, r_u, _ru, _r_u;
    bool fru, fr_u, f_ru, f_r_u, _fru, _fr_u, _f_ru, _f_r_u;
    public TextMeshProUGUI message;
    Transform d_trans;
    
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
    // Start is called before the first frame update
    void Start()
    {
        RigidDrone = GetComponent<Rigidbody>();
        d_trans = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        stopToObstacle();
        Movement();
    }
    void stopToObstacle()
    {
        float x = d_trans.localScale.x / 2;
        float y = d_trans.localScale.y / 2;
        float z = d_trans.localScale.z / 2;
        float xz = Mathf.Sqrt(Mathf.Pow(x, 2) + Mathf.Pow(z, 2)) / 2;
        float xy = Mathf.Sqrt(Mathf.Pow(x, 2) + Mathf.Pow(y, 2)) / 2;
        float yz = Mathf.Sqrt(Mathf.Pow(y, 2) + Mathf.Pow(z, 2)) / 2;
        float xyz = Mathf.Sqrt(Mathf.Pow(x,2)+ Mathf.Pow(y, 2) + Mathf.Pow(z, 2)) / 2;

        Debug.DrawRay(transform.position, transform.forward * (x+0.05f), Color.green);
        forwardMove = Physics.Raycast(transform.position, transform.forward, (x + 0.05f), LayerMask.GetMask("Ground"));

        Debug.DrawRay(transform.position, -transform.forward * (x + 0.05f), Color.green);
        backMove = Physics.Raycast(transform.position, -transform.forward, (x + 0.05f), LayerMask.GetMask("Ground"));

        Debug.DrawRay(transform.position, transform.right * (z + 0.05f), Color.green);
        rightMove = Physics.Raycast(transform.position, transform.right, (z + 0.05f), LayerMask.GetMask("Ground"));

        Debug.DrawRay(transform.position, -transform.right * (z + 0.05f), Color.green);
        leftMove = Physics.Raycast(transform.position, -transform.right, (z + 0.05f), LayerMask.GetMask("Ground"));

        Debug.DrawRay(transform.position, transform.up * (y + 0.05f), Color.green);
        upMove = Physics.Raycast(transform.position, transform.up, (y + 0.05f), LayerMask.GetMask("Ground"));

        Debug.DrawRay(transform.position, -transform.up * (y + 0.05f), Color.green);
        downMove = Physics.Raycast(transform.position, -transform.up, (y + 0.05f), LayerMask.GetMask("Ground"));

        Debug.DrawRay(transform.position, (transform.forward + transform.right) * (xz + 0.05f), Color.green);
        fr = Physics.Raycast(transform.position, (transform.forward + transform.right), (xz + 0.05f), LayerMask.GetMask("Ground"));

        Debug.DrawRay(transform.position, (transform.forward - transform.right) * (xz + 0.05f), Color.green);
        f_r = Physics.Raycast(transform.position, (transform.forward - transform.right), (xz + 0.05f), LayerMask.GetMask("Ground"));

        Debug.DrawRay(transform.position, (transform.forward + transform.up) * (xy + 0.05f), Color.green);
        fu = Physics.Raycast(transform.position, (transform.forward + transform.up), (xy + 0.05f), LayerMask.GetMask("Ground"));

        Debug.DrawRay(transform.position, (transform.forward - transform.up) * (xy + 0.05f), Color.green);
        f_u = Physics.Raycast(transform.position, (transform.forward - transform.up), (xy + 0.05f), LayerMask.GetMask("Ground"));

        Debug.DrawRay(transform.position, (-transform.forward + transform.right) * (xz + 0.05f), Color.green);
        _fr = Physics.Raycast(transform.position, (-transform.forward + transform.right), (xz + 0.05f), LayerMask.GetMask("Ground"));

        Debug.DrawRay(transform.position, (-transform.forward - transform.right) * (xz + 0.05f), Color.green);
        _f_r = Physics.Raycast(transform.position, (-transform.forward - transform.right), (xz + 0.05f), LayerMask.GetMask("Ground"));

        Debug.DrawRay(transform.position, (-transform.forward + transform.up) * (xy + 0.05f), Color.green);
        _fu = Physics.Raycast(transform.position, (-transform.forward + transform.up), (xy + 0.05f), LayerMask.GetMask("Ground"));

        Debug.DrawRay(transform.position, (-transform.forward - transform.up) * (xy + 0.05f), Color.green);
        _f_u = Physics.Raycast(transform.position, (-transform.forward - transform.up), (xy + 0.05f), LayerMask.GetMask("Ground"));

        Debug.DrawRay(transform.position, (transform.right + transform.up) * (yz + 0.05f), Color.green);
        ru = Physics.Raycast(transform.position, (transform.right + transform.up), (yz + 0.05f), LayerMask.GetMask("Ground"));

        Debug.DrawRay(transform.position, (transform.right - transform.up) * (yz + 0.05f), Color.green);
        r_u = Physics.Raycast(transform.position, (transform.right - transform.up), (yz + 0.05f), LayerMask.GetMask("Ground"));

        Debug.DrawRay(transform.position, (-transform.right + transform.up) * (yz + 0.05f), Color.green);
        _ru = Physics.Raycast(transform.position, (-transform.right + transform.up), (yz + 0.05f), LayerMask.GetMask("Ground"));

        Debug.DrawRay(transform.position, (-transform.right - transform.up) * (yz + 0.05f), Color.green);
        _r_u = Physics.Raycast(transform.position, (-transform.right - transform.up), (yz + 0.05f), LayerMask.GetMask("Ground"));

        Debug.DrawRay(transform.position, (transform.forward + transform.right + transform.up) * (xyz + 0.05f), Color.green);
        fru = Physics.Raycast(transform.position, (transform.forward + transform.right + transform.up), (xyz + 0.05f), LayerMask.GetMask("Ground"));

        Debug.DrawRay(transform.position, (transform.forward + transform.right - transform.up) * (xyz + 0.05f), Color.green);
        fr_u = Physics.Raycast(transform.position, (transform.forward + transform.right - transform.up), (xyz + 0.05f), LayerMask.GetMask("Ground"));

        Debug.DrawRay(transform.position, (transform.forward - transform.right + transform.up) * (xyz + 0.05f), Color.green);
        f_ru = Physics.Raycast(transform.position, (transform.forward - transform.right + transform.up), (xyz + 0.05f), LayerMask.GetMask("Ground"));

        Debug.DrawRay(transform.position, (transform.forward - transform.right - transform.up) * (xyz + 0.05f), Color.green);
        f_r_u = Physics.Raycast(transform.position, (transform.forward - transform.right - transform.up), (xyz + 0.05f), LayerMask.GetMask("Ground"));

        Debug.DrawRay(transform.position, (-transform.forward + transform.right + transform.up) * (xyz + 0.05f), Color.green);
        _fru = Physics.Raycast(transform.position, (-transform.forward + transform.right + transform.up), (xyz + 0.05f), LayerMask.GetMask("Ground"));

        Debug.DrawRay(transform.position, (-transform.forward + transform.right - transform.up) * (xyz + 0.05f), Color.green);
        _fr_u = Physics.Raycast(transform.position, (-transform.forward + transform.right - transform.up), (xyz + 0.05f), LayerMask.GetMask("Ground"));

        Debug.DrawRay(transform.position, (-transform.forward - transform.right + transform.up) * (xyz + 0.05f), Color.green);
        _f_ru = Physics.Raycast(transform.position, (-transform.forward - transform.right + transform.up), (xyz + 0.05f), LayerMask.GetMask("Ground"));

        Debug.DrawRay(transform.position, (-transform.forward - transform.right - transform.up) * (xyz + 0.05f), Color.green);
        _f_r_u = Physics.Raycast(transform.position, (-transform.forward - transform.right - transform.up), (xyz + 0.05f), LayerMask.GetMask("Ground"));

        if(forwardMove || backMove || rightMove ||leftMove || upMove || downMove || 
            fr || f_r || fu || f_u || _fr || _f_r || _fu || _f_u || ru || r_u || _ru || _r_u ||
            fru || fr_u || f_ru || f_r_u || _fru || _fr_u || _f_ru || _f_r_u)
        {
            message.text = "You bumped into a building.";
        }
        else
            message.text = "";
    }
    public void Movement()
    {
        if (Input.GetKey(KeyCode.I))//앞으로 이동
        {
            if (!forwardMove && !fr && !f_r && !fu && !f_u && !fru && !fr_u && !f_ru && !f_r_u)
            {
                DronePosition += Vector3.forward * moveSpeed * Time.deltaTime;
                RigidDrone.MovePosition(DronePosition);
            }
        }
        if (Input.GetKey(KeyCode.K))//뒤
        {
            if (!backMove && !_fr && !_f_r && !_fu && !_f_u && !_fru && !_fr_u && !_f_ru && !_f_r_u)
            {
                DronePosition -= Vector3.forward * moveSpeed * Time.deltaTime;
                RigidDrone.MovePosition(DronePosition);
            }
        }
        if (Input.GetKey(KeyCode.J))//왼쪽
        {
            if (!leftMove && !f_r && !_f_r && !_ru && !_r_u && !f_ru && !f_r_u && !_f_ru && !_f_r_u)
            {
                DronePosition += Vector3.left * moveSpeed * Time.deltaTime;
                RigidDrone.MovePosition(DronePosition);
            }
        }
        if (Input.GetKey(KeyCode.L))//오른쪽
        {
            if (!rightMove && !fr && !_fr && !ru && !r_u && !fru && !fr_u && !_fru && !_fr_u)
            {
                DronePosition += Vector3.right * moveSpeed * Time.deltaTime;
                RigidDrone.MovePosition(DronePosition);
            }
        }
        if (Input.GetKey(KeyCode.U))//아래
        {
            if (!downMove && !f_u && !_f_u && !r_u && !_r_u && !fr_u && !f_r_u && !_fr_u && !_f_r_u)
            {
                DronePosition -= Vector3.up * moveSpeed * Time.deltaTime;
                RigidDrone.MovePosition(DronePosition);
            }
        }
        if (Input.GetKey(KeyCode.O))//위
        {
            if (!upMove && !fu && !_fu && !ru && !_ru && !fru && !f_ru && !_fru && !_f_ru)
            {
                DronePosition += Vector3.up * moveSpeed * Time.deltaTime;
                RigidDrone.MovePosition(DronePosition);
            }
        }
    }
}
