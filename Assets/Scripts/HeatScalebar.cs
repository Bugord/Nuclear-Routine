using UnityEngine;

public class HeatScalebar : Scalebar
{
    [SerializeField] private float baseCool;
    [SerializeField] private float waterMinLevel;
    [SerializeField] private float waterMaxLevel;
    [SerializeField] private float waterMinLevelMod;
    [SerializeField] private float waterMaxLevelMod;
    [SerializeField] private float fuelDeepMod;

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
        return _fuelController.GetTotalDeep() * fuelDeepMod;
    }
}