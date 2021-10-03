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

    public void DecreaseValue(float value)
    {
        if (value.IsBetweenRange(0.0098f, 0.01f))
        {
            hornSoundController.Play(palette.hornClips.GetRandomItem());
        }

        steamSoundController.Play(palette.steamClips.GetRandomItem(), value * 100);
        ChangeValue(Value - value);
    }

    public void StopSteamSound()
    {
        steamSoundController.Stop();
    }
}