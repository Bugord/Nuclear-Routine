using System;
using System.Collections;
using System.Collections.Generic;
using ScriptableObjects.ScalebarsParameters;
using UnityEngine;

public class PowerScalebar : Scalebar
{
    public event Action Blackout;
    [SerializeField] private List<PowerScalebarParametersSO> _powerScalebarParameters;
    private SteamScalebar _steamScalebar;
    
    [SerializeField] private float _timeToBlackout;
    private Coroutine _blackoutTimer;
    private bool _isCounting;

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
        
        
        if (Value > 0 && _isCounting)
        {
            LightManager.Instance.EnableNormalLight();
            StopCoroutine(_blackoutTimer);
            _isCounting = false;
        }
        if (Value == 0 && !_isCounting)
        {
            LightManager.Instance.EnableReserveLight();
            _blackoutTimer = StartCoroutine(BlackoutTimer());
            _isCounting = true;
        }
    }

    private float _blackoutTime;
    
    IEnumerator BlackoutTimer()
    {
        _blackoutTime = 0;
        while (_blackoutTime < _timeToBlackout)
        {
            if (ScalebarManager.Instance.isFreezed)
            {
                yield return null;
            }
            _blackoutTime += Time.deltaTime;
            yield return null;
        }
        Blackout?.Invoke();
    }
}