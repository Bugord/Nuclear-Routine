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
    [SerializeField] private SoundController turbineSoundController;
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
        currentValue += (-currentParameter.baseDecrease + _heatScalebar.Value * currentParameter.heatMod) * Time.deltaTime;

        if (_waterScalebar.Value > 0.1)
        {
            currentValue += 1 * currentParameter.waterMod * Time.deltaTime;
        }
        else
        {
            currentValue -= 1 * currentParameter.waterMod * Time.deltaTime;
        }
        
        if (Value > 0.7f)
        {
            currentValue += currentParameter.steamMod * Value * Time.deltaTime;
        }

        ChangeValue(currentValue);

        if (Value.IsBetweenRange(0.8f, 1))
        {
            pressureSoundController.Play(palette.pressureClip, currentValue, false);
        }
        else
        {
            pressureSoundController.Stop();
        }
        
        turbineSoundController.Play(palette.turbineClip, Value*1.5f);
        
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

    private float _explodeTime;
    
    IEnumerator ExplodeTimer()
    {
        while (_explodeTime < _timeToExplode)
        {
            if (ScalebarManager.Instance.isFreezed)
            {
                yield return null;
            }
            _explodeTime += Time.deltaTime;
            yield return null;
        }
        SteamExplode?.Invoke();
    }
}