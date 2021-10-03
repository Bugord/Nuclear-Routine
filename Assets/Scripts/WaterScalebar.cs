using UnityEngine;

public class WaterScalebar : Scalebar
{
    [SerializeField] private float baseWaterReduction = 0.1f;
    [SerializeField] private float heatMod, heatMinMod, heatMaxMod;
    
    private HeatScalebar _heatScalebar; 
        
    private void Awake()
    {
        _heatScalebar = ScalebarManager.Instance.HeatScalebar;
    }

    protected override void Tick()
    {
        var currentValue = Value;
        var heatModValue = Mathf.Clamp(heatMod * _heatScalebar.Value, heatMinMod, heatMaxMod);
        currentValue -= (baseWaterReduction + heatModValue) * Time.deltaTime;
        ChangeValue(currentValue);
    }

    public void AddWater(float delta)
    {
        var currentValue = Value;
        currentValue += delta;
        
        ChangeValue(currentValue);
    }
}