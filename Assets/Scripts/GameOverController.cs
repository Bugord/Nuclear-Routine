using System;
using UnityEngine;

public class GameOverController : MonoBehaviour
{
    private void Awake()
    {
        ScalebarManager.Instance.HeatScalebar.HeatExplode += OnHeatExplode;
        ScalebarManager.Instance.SteamScalebar.SteamExplode += OnSteamExplode;
        ScalebarManager.Instance.PowerScalebar.Blackout += OnBlackout;
    }

    private void OnBlackout()
    {
        Debug.Log("BLACKOUT");
    }

    private void OnSteamExplode()
    {
        Debug.Log("STEAM KABOOOOOM");
    }

    private void OnHeatExplode()
    {
        Debug.Log("KABOOOOOM");
    }
}