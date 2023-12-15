using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunCtrl : MonoBehaviour
{
    void Update()
    {
        transform.Rotate(Vector3.right, 0.208f * Time.deltaTime);
    }
}
