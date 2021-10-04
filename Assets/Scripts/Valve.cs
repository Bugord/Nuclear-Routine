using System;
using DefaultNamespace;
using DefaultNamespace.Controllers;
using UnityEngine;

public class Valve : MonoBehaviour
{
    [SerializeField] private float waterMod;
    [SerializeField] private ValveSoundPaletteSo soundPalette;
    [SerializeField] private SoundController valveSoundController;
    [SerializeField] private SoundController waterSoundController;

    private Camera _myCam;
    private Vector3 _screenPos;
    private float _angleOffset;
    private bool _isDragged;
    private float _lastAngle;

    private float value;
    private bool _isOver;

    [SerializeField] private GameObject dynamicValve;
    private bool _canDrop;
    [SerializeField] private float waterToDrop;
    private float _waterCounter;

    void Start()
    {
        _myCam = Camera.main;
    }

    private void OnMouseEnter()
    {
        if (!_isDragged)
            CursorManager.Instance.SetCursor(CursorType.BeforeGrab);
        _isOver = true;
    }

    private void OnMouseDown()
    {
        SetAngleOffset();
        _isDragged = true;
        CursorManager.Instance.SetCursor(CursorType.Grab);
    }

    private void OnMouseExit()
    {
        if (!_isDragged)
            CursorManager.Instance.SetCursor(CursorType.Pointer);
        _isOver = false;
    }

    private void SetAngleOffset()
    {
        _screenPos = _myCam.WorldToScreenPoint(transform.position);
        var v3 = Input.mousePosition - _screenPos;
        _angleOffset = (Mathf.Atan2(transform.right.y, transform.right.x) - Mathf.Atan2(v3.y, v3.x)) * Mathf.Rad2Deg;
    }

    private void OnMouseUp()
    {
        _isDragged = false;
        valveSoundController.Stop();
        waterSoundController.Play(soundPalette.waterEndSound, true);
        CursorManager.Instance.SetCursor(_isOver ? CursorType.BeforeGrab : CursorType.Pointer);
    }

    private void OnMouseDrag()
    {
        if (ScalebarManager.Instance.isFreezed)
            return;

        var v3 = Input.mousePosition - _screenPos;
        var angle = Mathf.Atan2(v3.y, v3.x) * Mathf.Rad2Deg;

        if (_lastAngle > angle)
        {
            SetAngleOffset();
        }

        var delta = angle - _lastAngle;
        if (delta > 0 && delta < 90)
        {
            ScalebarManager.Instance.WaterScalebar.AddWater(delta * waterMod);
            if (_canDrop)
            {
                _waterCounter += delta * waterMod;
                if (_waterCounter >= waterToDrop)
                {
                    _waterCounter = 0;
                    Drop();
                }
            }
        }

        if (delta > 3 && delta < 90)
        {
            valveSoundController.Play(soundPalette.valveSound);
            waterSoundController.Play(soundPalette.waterSound);
        }

        transform.eulerAngles = new Vector3(0, 0, angle + _angleOffset);
        _lastAngle = angle;
    }

    public void SetCanDrop()
    {
        _canDrop = true;
        Drop();
    }

    private void Drop()
    {
        dynamicValve.transform.position = transform.position;
        dynamicValve.transform.GetChild(0).rotation = transform.rotation;
        dynamicValve.SetActive(true);
        transform.parent.gameObject.SetActive(false);
        dynamicValve.GetComponent<DynamicValveController>().Drop();
    }
}