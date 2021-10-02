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

    public UnityEvent onMouseDown;
    public UnityEvent onMouseDrag;
    public UnityEvent onMouseUp;
    
    private void Awake()
    {
        _renderer = GetComponent<SpriteRenderer>();
        _renderer.sprite = palette.released;
    }

    private void OnMouseDown()
    {
        _renderer.sprite = palette.pressed;
        onMouseDown?.Invoke();
    }

    private void OnMouseDrag()
    {
        onMouseDrag?.Invoke();
    }

    private void OnMouseUp()
    {
        _renderer.sprite = palette.released;
        onMouseUp?.Invoke();
    }
}
