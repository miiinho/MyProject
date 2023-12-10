using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerCtrl : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float rotSpeed = 10f;

    private float xRotation;
    private float yRotation;
    private Camera cam;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        cam = Camera.main;

        cam.transform.eulerAngles = Vector3.zero;
    }


    void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            moveSpeed = 15f;
        }

        else if (Input.GetKey(KeyCode.LeftControl))
        {
            moveSpeed = 5f;
        }

        else
        {
            moveSpeed = 10f;
        }

        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 moveVector = transform.forward * v + transform.right * h;

        transform.position += moveVector.normalized * moveSpeed * Time.deltaTime;

        //Vector3 dir = new Vector3(h, 0, v);


        //if (!(h == 0 && v == 0))
        //{
        //    transform.position += dir * moveSpeed * Time.deltaTime;
        //    transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(dir), Time.deltaTime * rotSpeed);
        //}

        float mouseX = Input.GetAxisRaw("Mouse X") * rotSpeed * Time.deltaTime;
        float mouseY = Input.GetAxisRaw("Mouse Y") * rotSpeed * Time.deltaTime;

        yRotation += mouseX;
        xRotation -= mouseY;

        xRotation = Mathf.Clamp(xRotation, -50f, 50f);

        cam.transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        transform.rotation = Quaternion.Euler(0, yRotation, 0);
    }
}