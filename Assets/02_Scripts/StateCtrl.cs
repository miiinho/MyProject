using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StateCtrl : MonoBehaviour
{
    public Slider slider;

    void Start()
    {
        slider = GetComponent<Slider>();

        slider.value = 1f;
    }

    void Update()
    {
        slider.value -= 0.0005f * Time.deltaTime;
    }
}
