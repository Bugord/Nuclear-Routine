using System;
using UnityEngine;

public class SteamScalebar : Scalebar
{
    [SerializeField] private float heatMod;
    [SerializeField] private float waterMod;
    [SerializeField] private float baseDecrease;
    
    private HeatScalebar _heatScalebar;
    private WaterScalebar _waterScalebar;
    
    private void Awake()
    {
        _heatScalebar = ScalebarManager.Instance.HeatScalebar;
        _waterScalebar = ScalebarManager.Instance.WaterScalebar;
    }

    protected override void Tick()
    {
        var currentValue = Value;
        currentValue += (-baseDecrease + _waterScalebar.Value * waterMod * _heatScalebar.Value * heatMod) * Time.deltaTime;
        
        ChangeValue(currentValue);
    }

    public void DecreaseValue(float value)
    {
        ChangeValue(Value - value);
    }
}