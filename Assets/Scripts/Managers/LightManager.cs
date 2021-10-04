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

    [SerializeField] private SoundController lightSoundController;

    public Action<bool> LightSwitched;

    private int pizdecCount;

    void Awake()
    {
        Instance = this;
    }

    public void EnableAlarmLight(bool isSoundOn)
    {
        pizdecCount++;
        LightSwitched?.Invoke(false);
        lightSoundController.Play(SoundManager.Instance.GetAudioClip("LightTurnOff"));
        alarmSound.SetActive(isSoundOn);
        normalLight.SetActive(false);
        alarmLight.SetActive(true);
        reserveLight.SetActive(true);
    }
    
    public void EnableReserveLight()
    {
        pizdecCount++;
        lightSoundController.Play(SoundManager.Instance.GetAudioClip("LightTurnOff"));
        LightSwitched?.Invoke(false);
        normalLight.SetActive(false);
        alarmLight.SetActive(false);
        reserveLight.SetActive(true);
    }

    public void EnableNormalLight()
    {
        pizdecCount--;
        if (pizdecCount != 0)
        {
            return;
        }
        lightSoundController.Play(SoundManager.Instance.GetAudioClip("LightTurnOn"));
        LightSwitched?.Invoke(true);
        normalLight.SetActive(true);
        alarmLight.SetActive(false);
        reserveLight.SetActive(false);
    }
}
