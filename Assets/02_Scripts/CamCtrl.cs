using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamCtrl : MonoBehaviour
{
    public float rotSpeed = 200;

    private float mx;
    private float my;

    void Start()
    {
        transform.eulerAngles = Vector3.zero;
    }

    void Update()
    {
        float h = Input.GetAxis("Mouse X");
        float v = Input.GetAxis("Mouse Y");

        mx += h * rotSpeed * Time.deltaTime;
        my += v * rotSpeed * Time.deltaTime;

        my = Mathf.Clamp(my, -50, 50);

        transform.eulerAngles = new Vector3(-my, mx, 0);
    }
}