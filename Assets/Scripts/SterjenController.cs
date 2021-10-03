using System;
using System.Collections;
using System.Collections.Generic;
using ScriptableObjects;
using UnityEngine;

public enum SterjenGroup
{
    Red,
    Green,
    Blue
}

public class SterjenController : MonoBehaviour
{
    public SterjenGroup SterjenGroup => _sterjenGroup;
    [SerializeField] private SterjenPaletteSo _sterjenPaletteSo;
    [SerializeField] private SterjenGroup _sterjenGroup;
    [SerializeField] private float deepDistance;
    private float _maxDeepPosY;
    private float _minDeepPosY;
    
    private void Awake()
    {
        _maxDeepPosY = transform.position.y;
        _minDeepPosY = transform.position.y + deepDistance;
    }

    public void ChangeDeep(float deepValue)
    {
        transform.position = new Vector3(transform.position.x, Mathf.Lerp(_minDeepPosY, _maxDeepPosY, deepValue));
    }

    public void OnValidate()
    {
        var spriteRenderer = GetComponent<SpriteRenderer>();
        switch (SterjenGroup)
        {
            case SterjenGroup.Red:
                spriteRenderer.sprite = _sterjenPaletteSo.redSterjen;
                break;
            case SterjenGroup.Blue:
                spriteRenderer.sprite = _sterjenPaletteSo.blueSterjen;
                break;    
            case SterjenGroup.Green:
                spriteRenderer.sprite = _sterjenPaletteSo.greenSterjen;
                break;
        }
    }
}
