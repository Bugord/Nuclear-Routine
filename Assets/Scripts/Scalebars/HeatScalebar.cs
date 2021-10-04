using System;
using System.Collections;
using System.Collections.Generic;
using ScriptableObjects.ScalebarsParameters;
using UnityEngine;

public class HeatScalebar : Scalebar
{
    public event Action HeatExplode;
    [SerializeField] private List<HeatScalebarParametersSO> _heatScalebarParameters;
    private WaterScalebar _waterScalebar;
    private FuelController _fuelController;
    
    [SerializeField] private float _timeToExplode;
    private Coroutine _explodeTimer;
    private bool _isCounting;
    
    
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

        if (Value < 1 && _isCounting)
        {
            LightManager.Instance.EnableNormalLight();
            StopCoroutine(_explodeTimer);
            _isCounting = false;
        }
        if (Value == 1 && !_isCounting)
        {
            LightManager.Instance.EnableAlarmLight(true);
            _explodeTimer = StartCoroutine(ExplodeTimer());
            _isCounting = true;
        }
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

    IEnumerator ExplodeTimer()
    {
        yield return new WaitForSeconds(_timeToExplode);
        HeatExplode?.Invoke();
    }
}