using System.Collections;
using System.Collections.Generic;
using GameEvent;
using TMPro;
using UnityEngine;

public class ComputerController : MonoBehaviour
{
    [SerializeField] private TextMeshPro header;
    [SerializeField] private TextMeshPro electricityCounter;

    private float _electricity;
    
    // Update is called once per frame
    void Update()
    {
        if (ScalebarManager.Instance.isFreezed)
        {
            return;
        }

        string text = "";

        if (ScalebarManager.Instance.HeatScalebar.Value < 0.1f)
        {
            text += "Reactor cooled down!\n";
        } else if (ScalebarManager.Instance.HeatScalebar.Value > 0.9f)
        {
            text += "Reactor overheat!\n";
        }

        if (ScalebarManager.Instance.PowerScalebar.Value < 0.2f)
        {
            text += "Low power!\n";
        } 
        
        if (ScalebarManager.Instance.WaterScalebar.Value < 0.2f)
        {
            text += "Low water level!\n";
        } 
        
        if (ScalebarManager.Instance.SteamScalebar.Value < 0.1f)
        {
            text += "Low steam pressure!\n";
        } else if (ScalebarManager.Instance.SteamScalebar.Value > 0.9f)
        {
            text += "High steam pressure!\n";
        }
        
        header.SetText(text);
        _electricity += ScalebarManager.Instance.PowerScalebar.Value;
        electricityCounter.SetText($"{(int)_electricity} kW");
    }
}
