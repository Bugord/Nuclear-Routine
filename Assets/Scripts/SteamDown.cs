using System;
using DefaultNamespace;
using DG.Tweening;
using GameEvent;
using UnityEngine;

public class SteamDown : MonoBehaviour
{
    
    [SerializeField] private float maxDistance = -3f;
    [SerializeField] private float steamDecreaseSpeed;
    private float _initialYPos;
    private float _initialClickPosY;
    private bool _isDragged;
    private Camera _camera;
    private SteamScalebar _steamScalebar;
    private bool _isOver;

    private void Awake()
    {
        _steamScalebar = ScalebarManager.Instance.SteamScalebar;
    }

    private void Start()
    {
        _camera = Camera.main;
    }
    private void OnMouseEnter()
    {
        if(!_isDragged)
            CursorManager.Instance.SetCursor(CursorType.BeforeGrab);
        _isOver = true;
    }

    private void OnMouseDown()
    {
        if (_isDragged)
            return;
        
        CursorManager.Instance.SetCursor(CursorType.Grab);

        _isDragged = true;
        _initialYPos = transform.position.y;
        _initialClickPosY = _camera.ScreenToWorldPoint(Input.mousePosition).y;
    }
    
    private void OnMouseExit()
    {
        if(!_isDragged)
            CursorManager.Instance.SetCursor(CursorType.Pointer);
        _isOver = false;
    }

    private void OnMouseDrag()
    {
        if(ScalebarManager.Instance.isFreezed)
            return;
        
        var mouseWorldPos = _camera.ScreenToWorldPoint(Input.mousePosition);
        if (_initialYPos + (mouseWorldPos.y - _initialClickPosY) < _initialYPos + maxDistance)
        {
            transform.position = new Vector3(transform.position.x, _initialYPos + maxDistance);
            _steamScalebar.DecreaseValue(steamDecreaseSpeed * Time.deltaTime, 1);
        }
        else if (mouseWorldPos.y < _initialClickPosY)
        {
            transform.position =
                new Vector3(transform.position.x, _initialYPos + (mouseWorldPos.y - _initialClickPosY));
            var decreaseValueFactor = ((mouseWorldPos.y - _initialClickPosY) / maxDistance);
            _steamScalebar.DecreaseValue(steamDecreaseSpeed * decreaseValueFactor * Time.deltaTime, decreaseValueFactor);
        }
        else
        {
            transform.position = new Vector3(transform.position.x, _initialYPos);
        }
    }

    private void OnMouseUp()
    {
        transform.DOMoveY(_initialYPos, 0.3f).OnComplete(() => _isDragged = false);
        _steamScalebar.StopSteamSound();
        CursorManager.Instance.SetCursor(_isOver ? CursorType.BeforeGrab : CursorType.Pointer);
        _steamScalebar.HideSteam();
    }
    
}