using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public float time;

    void Start()
    {
        
    }

    void Update()
    {
        time += Time.deltaTime;
    }
}
