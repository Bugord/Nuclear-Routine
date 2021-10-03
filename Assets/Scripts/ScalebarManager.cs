using UnityEngine;

public class ScalebarManager : MonoBehaviour
{
    public static ScalebarManager Instance { get; private set; }
    
    public FuelController FuelController;
    public HeatScalebar HeatScalebar;
    public PowerScalebar PowerScalebar;
    public SteamScalebar SteamScalebar;
    public WaterScalebar WaterScalebar;

    public bool isFreezed;
    
    private void Awake()
    {
        Instance = this;
    }
}