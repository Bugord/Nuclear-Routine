using System.Collections.Generic;
using ScriptableObjects.ScalebarsParameters;
using UnityEngine;

public class HeatScalebar : Scalebar
{
    [SerializeField] private List<HeatScalebarParametersSO> _heatScalebarParameters;
    private WaterScalebar _waterScalebar;
    private FuelController _fuelController;

    private void Awake()
    {
        _waterScalebar = ScalebarManager.Instance.WaterScalebar;
        _fuelController = ScalebarManager.Instance.FuelController;
    }

    protected override void Tick()
    {
        var currentValue = Value;
        var waterModValue = GetWaterMod();

        var baseCoolValue = -_heatScalebarParameters[_currentDifficultyId].baseCool;
        currentValue += (baseCoolValue * waterModValue + GetFuelValue()) * Time.deltaTime;
        ChangeValue(currentValue);
    }

    private float GetWaterMod()
    {
        if (_waterScalebar.Value < _heatScalebarParameters[_currentDifficultyId].waterMinLevel)
        {
            return _heatScalebarParameters[_currentDifficultyId].waterMinLevelMod;
        }

        if (_waterScalebar.Value > _heatScalebarParameters[_currentDifficultyId].waterMaxLevel)
        {
            return _heatScalebarParameters[_currentDifficultyId].waterMaxLevelMod;
        }

        return  _heatScalebarParameters[_currentDifficultyId].waterBaseMod;
    }

    private float GetFuelValue()
    {
        return _fuelController.GetTotalDeep() * _heatScalebarParameters[_currentDifficultyId].fuelDeepMod;
    }
}