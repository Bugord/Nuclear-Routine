using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(SpriteRenderer))]
public class ButtonComponent : MonoBehaviour
{
    [SerializeField] private ButtonPaletteSo palette;
    
    private SpriteRenderer _renderer;
    
    private void Awake()
    {
        _renderer = GetComponent<SpriteRenderer>();
        _renderer.sprite = palette.released;
    }

    protected virtual void OnMouseDown()
    {
        _renderer.sprite = palette.pressed;
    }

    protected virtual void OnMouseDrag()
    {
    }

    protected virtual void OnMouseUp()
    {
        _renderer.sprite = palette.released;
    }
}
