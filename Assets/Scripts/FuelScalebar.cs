using System;
using UnityEngine;

public class FuelScalebar : Scalebar
{
    public float Deep => deep;
    [Range(0,1f)]
    [SerializeField] private float deep;
    [SerializeField] private float deepDecreaseMod;
    [SerializeField] private float baseDecrease;
    protected override void Tick()
    {
        var currentValue = Value;
        currentValue -= (baseDecrease + deepDecreaseMod * deep) * Time.deltaTime;
        
        ChangeValue(currentValue);
    }
}