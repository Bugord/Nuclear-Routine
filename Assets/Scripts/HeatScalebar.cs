using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HeatScalebar : Scalebar
{
    [SerializeField] private float baseCool;
    [SerializeField] private float waterMinLevel;
    [SerializeField] private float waterMaxLevel;
    [SerializeField] private float waterMinLevelMod;
    [SerializeField] private float waterMaxLevelMod;
    [SerializeField] private float fuelDeepMod;
    [SerializeField] private float fuelBaseValue;

    private WaterScalebar _waterScalebar;
    private List<FuelScalebar> _fuelScalebars;

    private void Awake()
    {
        _waterScalebar = ScalebarManager.Instance.WaterScalebar;
        _fuelScalebars = ScalebarManager.Instance.FuelScalebars;
    }

    protected override void Tick()
    {
        var currentValue = Value;
        var waterModValue = GetWaterMod();

        currentValue += (-baseCool * waterModValue + GetFuelValue()) * Time.deltaTime;
        ChangeValue(currentValue);
    }

    private float GetWaterMod()
    {
        if (_waterScalebar.Value < waterMinLevel)
        {
            return waterMinLevelMod;
        }

        if (_waterScalebar.Value > waterMaxLevel)
        {
            return waterMaxLevelMod;
        }

        return 1;
    }

    private float GetFuelValue()
    {
        var enabledFuelScalebars = _fuelScalebars.Where(x => x.Value > 0).ToList();
        return fuelBaseValue * enabledFuelScalebars.Count + enabledFuelScalebars.Sum(x => x.Deep * fuelDeepMod);
    }
}