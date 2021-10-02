using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScalebarComponent : MonoBehaviour
{
    [SerializeField] private SpriteRenderer bar;

    private Vector2 _minSize;
    private Vector2 _maxSize;
    
    private Scalebar _scalebar;
    
    void Awake()
    {
        var size = bar.size;
        _minSize = new Vector2(size.x, 0);
        _maxSize = size;
        _scalebar = GetComponent<Scalebar>();
        _scalebar!.ValueChanged += UpdateBar;
    }

    private void UpdateBar(float value)
    {
        bar.size = Vector2.Lerp(_minSize, _maxSize, value);
    }

    private void OnDestroy()
    { 
        _scalebar.ValueChanged -= UpdateBar;
    }
}
