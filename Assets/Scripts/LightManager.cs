using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightManager : MonoBehaviour
{
    public static LightManager Instance;

    [SerializeField] private GameObject normalLight;
    [SerializeField] private GameObject alarmLight;
    [SerializeField] private GameObject alarmSound;
    [SerializeField] private GameObject reserveLight;

    public Action<bool> LightSwitched;

    void Awake()
    {
        Instance = this;
    }

    public void EnableAlarmLight(bool isSoundOn)
    {
        LightSwitched?.Invoke(false);
        alarmSound.SetActive(isSoundOn);
        normalLight.SetActive(false);
        alarmLight.SetActive(true);
        reserveLight.SetActive(true);
    }
    
    public void EnableReserveLight()
    {
        LightSwitched?.Invoke(false);
        normalLight.SetActive(false);
        alarmLight.SetActive(false);
        reserveLight.SetActive(true);
    }

    public void EnableNormalLight()
    {
        LightSwitched?.Invoke(true);
        normalLight.SetActive(true);
        alarmLight.SetActive(false);
        reserveLight.SetActive(false);
    }
}
