using System.Collections.Generic;
using ScriptableObjects.ScalebarsParameters;
using UnityEngine;

public class WaterScalebar : Scalebar
{
    [SerializeField] private List<WaterScalebarParametersSO> _waterScalebarParameters;
    private HeatScalebar _heatScalebar;

    private void Awake()
    {
        _heatScalebar = ScalebarManager.Instance.HeatScalebar;
    }

    protected override void Tick()
    {
        var currentValue = Value;
        var heatModValue = Mathf.Clamp(_waterScalebarParameters[_currentDifficultyId].heatMod * _heatScalebar.Value,
            _waterScalebarParameters[_currentDifficultyId].heatMinMod,
            _waterScalebarParameters[_currentDifficultyId].heatMaxMod);
        currentValue -= (_waterScalebarParameters[_currentDifficultyId].baseWaterReduction + heatModValue) * Time.deltaTime;
        ChangeValue(currentValue);
    }

    public void AddWater(float delta)
    {
        var currentValue = Value;
        currentValue += delta;

        ChangeValue(currentValue);
    }
}