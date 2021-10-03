using System;
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

    void Start()
    {
        _myCam = Camera.main;
    }

    private void OnMouseDown()
    {
        SetAngleOffset();
        _isDragged = true;
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
    }

    private void OnMouseDrag()
    {
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
        }

        if (delta > 3 && delta < 90)
        {
            valveSoundController.Play(soundPalette.valveSound);
            waterSoundController.Play(soundPalette.waterSound);
        }
        
        transform.eulerAngles = new Vector3(0, 0, angle + _angleOffset);
        _lastAngle = angle;
    }
}