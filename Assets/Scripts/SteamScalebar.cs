using System.Collections.Generic;
using ScriptableObjects.ScalebarsParameters;
using UnityEngine;

public class SteamScalebar : Scalebar
{
    [SerializeField] private List<SteamScalebarParametersSO> _steamScalebarParameters;
    [SerializeField] private SoundController steamSoundController;
    [SerializeField] private SoundController hornSoundController;
    [SerializeField] private SteamSoundPaletteSo palette;

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
        var currentParameter = _steamScalebarParameters[_currentDifficultyId];
        currentValue += (-currentParameter.baseDecrease + _waterScalebar.Value * currentParameter.waterMod *
            _heatScalebar.Value * currentParameter.heatMod) * Time.deltaTime;

        ChangeValue(currentValue);
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
}