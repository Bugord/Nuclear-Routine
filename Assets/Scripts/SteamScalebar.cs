using System;
using System.Collections;
using System.Collections.Generic;
using ScriptableObjects.ScalebarsParameters;
using UnityEngine;

public class SteamScalebar : Scalebar
{
    public event Action SteamExplode; 

    [SerializeField] private List<SteamScalebarParametersSO> _steamScalebarParameters;
    [SerializeField] private SoundController steamSoundController;
    [SerializeField] private SoundController hornSoundController;
    [SerializeField] private SoundController pressureSoundController;
    [SerializeField] private SteamSoundPaletteSo palette;

    private HeatScalebar _heatScalebar;
    private WaterScalebar _waterScalebar;
    
    [SerializeField] private float _timeToExplode;
    private Coroutine _explodeTimer;
    private bool _isCounting;
    private void Awake()
    {
        _heatScalebar = ScalebarManager.Instance.HeatScalebar;
        _waterScalebar = ScalebarManager.Instance.WaterScalebar;
    }

    protected override void Tick()
    {
        var currentValue = Value;
        var currentParameter = _steamScalebarParameters[_currentDifficultyId];
        currentValue += (-currentParameter.baseDecrease + _waterScalebar.Value * currentParameter.waterMod *
            _heatScalebar.Value * currentParameter.heatMod) * Time.deltaTime;

        ChangeValue(currentValue);

        if (currentValue.IsBetweenRange(0.7f, 1))
        {
            pressureSoundController.Play(palette.pressureClip, currentValue, false);
        }
        else
        {
            pressureSoundController.Stop();
        }
        
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

    public void DecreaseValue(float value, float decreaseFactor)
    {
        if (Value - value <= 0)
        {
            decreaseFactor = 0;
        }
        if (decreaseFactor.IsBetweenRange(0.8f, 1))
        {
            hornSoundController.Play(palette.hornClips.GetRandomItem());
        }

        steamSoundController.Play(palette.steamClips.GetRandomItem(), decreaseFactor);
        ChangeValue(Value - value);
    }

    public void StopSteamSound()
    {
        steamSoundController.Stop();
    }
    
    IEnumerator ExplodeTimer()
    {
        yield return new WaitForSeconds(_timeToExplode);
        SteamExplode?.Invoke();
    }
}