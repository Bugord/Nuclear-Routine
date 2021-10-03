using System;
using DG.Tweening;
using UnityEngine;

public class GameOverController : MonoBehaviour
{
    [SerializeField] private SoundController gameOverSoundController;
    [SerializeField] private SoundController explosionSoundController;
    private void Awake()
    {
        ScalebarManager.Instance.HeatScalebar.HeatExplode += OnHeatExplode;
        ScalebarManager.Instance.SteamScalebar.SteamExplode += OnSteamExplode;
        ScalebarManager.Instance.PowerScalebar.Blackout += OnBlackout;
    }

    private void OnBlackout()
    {
        Debug.Log("BLACKOUT");
        ScalebarManager.Instance.isFreezed = true;
        LightManager.Instance.EnableAlarmLight(false);
        gameOverSoundController.Play(SoundManager.Instance.GetAudioClip("GameLost"));
    }

    private void OnSteamExplode()
    {
        Debug.Log("STEAM KABOOOOOM");
        ScalebarManager.Instance.isFreezed = true;
        LightManager.Instance.EnableAlarmLight(false);
        gameOverSoundController.Play(SoundManager.Instance.GetAudioClip("GameLost"));
        explosionSoundController.Play(SoundManager.Instance.GetAudioClip("Explosion"));
        Camera.main.DOShakePosition(3, 0.2f);
    }

    private void OnHeatExplode()
    {
        Debug.Log("KABOOOOOM");
        ScalebarManager.Instance.isFreezed = true;
        LightManager.Instance.EnableAlarmLight(false);
        gameOverSoundController.Play(SoundManager.Instance.GetAudioClip("GameLost"));
        explosionSoundController.Play(SoundManager.Instance.GetAudioClip("Explosion"));
        Camera.main.DOShakePosition(3, 0.2f);
    }
}