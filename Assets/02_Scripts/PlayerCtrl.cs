using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerCtrl : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float rotSpeed = 10f;
    public float gravity = 9.8f;

    private Vector3 velocity;
    private bool isGrounded;

    private float xRotation;
    private float yRotation;
    private Camera cam;

    private CharacterController characterController;

    void Start()
    {
        characterController = GetComponent<CharacterController>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        cam = Camera.main;

        cam.transform.eulerAngles = Vector3.zero;
    }


    void Update()
    {
        isGrounded = characterController.isGrounded;

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

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

        characterController.Move(moveVector * moveSpeed * Time.deltaTime);

        //transform.position += moveVector.normalized * moveSpeed * Time.deltaTime;


        float mouseX = Input.GetAxisRaw("Mouse X") * rotSpeed * Time.deltaTime;
        float mouseY = Input.GetAxisRaw("Mouse Y") * rotSpeed * Time.deltaTime;

        yRotation += mouseX;
        xRotation -= mouseY;

        xRotation = Mathf.Clamp(xRotation, -50f, 50f);

        cam.transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        transform.rotation = Quaternion.Euler(0, yRotation, 0);

        velocity.y -= gravity * Time.deltaTime;
        characterController.Move(velocity * Time.deltaTime);
    }
}