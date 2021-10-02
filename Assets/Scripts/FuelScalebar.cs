using System;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class FuelScalebar : Scalebar
{
    public float Deep => deep;
    [Range(0,1f)]
    [SerializeField] private float deep;

    [SerializeField] private float deepDistance;
    private float _maxDeepPosY;
    private float _minDeepPosY;
    private void Awake()
    {
        _maxDeepPosY = transform.position.y - deepDistance / 2f;
        _minDeepPosY = transform.position.y + deepDistance / 2f;
        transform.position = new Vector3(transform.position.x, Mathf.Lerp(_minDeepPosY, _maxDeepPosY, deep));
    }

    protected override void Tick()
    {
        
    }

    public void ChangeDeep(float value)
    {
        deep = Mathf.Clamp01(deep + value);
        transform.position = new Vector3(transform.position.x, Mathf.Lerp(_minDeepPosY, _maxDeepPosY, deep));
    }
}