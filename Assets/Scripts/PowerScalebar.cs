using UnityEngine;

public class PowerScalebar : Scalebar
{
    [SerializeField] private float baseValue;
    [SerializeField] private float increasedMod;
    [SerializeField] private float decreaseMod;
    [SerializeField] private float steamMin;
    [SerializeField] private float steamMax;
    
    private SteamScalebar _steamScalebar;

    private void Awake()
    {
        _steamScalebar = ScalebarManager.Instance.SteamScalebar;
    }

    protected override void Tick()
    {
        var currentValue = Value;
        if (_steamScalebar.Value < steamMin)
        {
            currentValue -= baseValue * decreaseMod * Time.deltaTime;
        }
        else if (_steamScalebar.Value > steamMax)
        {
            currentValue += baseValue * increasedMod * Time.deltaTime;
        }
        else
        {
            currentValue += baseValue * Time.deltaTime;
        }
        
        ChangeValue(currentValue);
    }
}