using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthIndicator : MonoBehaviour
{
    [SerializeField]
    private int _maxValue;

    [SerializeField]
    private TMP_Text _value;
    
    [SerializeField]
    private Slider _bar;

    void Start()
    {
        _bar.maxValue = _maxValue;
    }

    public void SetValue(int value)
    {
        _value.text = value.ToString();
        _bar.value = value;
    }
}
