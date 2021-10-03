using System.Collections.Generic;
using ScriptableObjects.ScalebarsParameters;
using UnityEngine;

public class PowerScalebar : Scalebar
{
    [SerializeField] private List<PowerScalebarParametersSO> _powerScalebarParameters;
    private SteamScalebar _steamScalebar;

    private void Awake()
    {
        _steamScalebar = ScalebarManager.Instance.SteamScalebar;
    }

    protected override void Tick()
    {
        var currentParameters = _powerScalebarParameters[_currentDifficultyId];
        var currentValue = Value;
        if (_steamScalebar.Value < currentParameters.steamMin)
        {
            currentValue -= currentParameters.baseValue * currentParameters.decreaseMod * Time.deltaTime;
        }
        else if (_steamScalebar.Value > currentParameters.steamMax)
        {
            currentValue += currentParameters.baseValue * currentParameters.increasedMod * Time.deltaTime;
        }
        else
        {
            currentValue += currentParameters.baseValue * Time.deltaTime;
        }
        
        ChangeValue(currentValue);
    }
}