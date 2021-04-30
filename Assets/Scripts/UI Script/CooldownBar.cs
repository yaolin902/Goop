using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CooldownBar : MonoBehaviour
{
    public Slider slider;
    // Start is called before the first frame update
    void Start()
    {
        slider.value = slider.maxValue;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (slider.value < slider.maxValue)
            slider.value += Time.deltaTime;
    }

    public void set_max_value(float max) {
        slider.maxValue = max;
        slider.value = 0;
    }
}
