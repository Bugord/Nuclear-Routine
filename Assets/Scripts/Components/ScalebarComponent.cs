using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScalebarComponent : MonoBehaviour
{
    [SerializeField] private Transform bar;

    private Vector3 _startPosition;
    
    private Scalebar _scalebar;
    
    void Awake()
    {
        _startPosition = bar.localPosition;
        _scalebar = GetComponent<Scalebar>();
        _scalebar!.ValueChanged += UpdateBar;
    }

    private void UpdateBar(float value)
    {
        bar.localPosition = Vector3.Lerp(_startPosition, Vector3.zero, value);
    }

    private void OnDestroy()
    { 
        _scalebar.ValueChanged -= UpdateBar;
    }
}
