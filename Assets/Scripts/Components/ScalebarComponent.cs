using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScalebarComponent : MonoBehaviour
{
    [SerializeField] private Image bar;

    private Scalebar _scalebar;
    
    void Awake()
    {
        _scalebar = GetComponent<Scalebar>();
        _scalebar!.ValueChanged += UpdateBar;
    }

    private void UpdateBar(float value)
    {
        bar.fillAmount = value;
    }

    private void OnDestroy()
    { 
        _scalebar.ValueChanged -= UpdateBar;
    }
}
