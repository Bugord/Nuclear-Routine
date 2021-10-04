using System;
using System.Collections;
using DefaultNamespace;
using DG.Tweening;
using UnityEngine;

public class GameOverController : MonoBehaviour
{
    public static GameOverController Instance;
    [SerializeField] private SoundController gameOverSoundController;
    [SerializeField] private SoundController explosionSoundController;
    [SerializeField] private SoundController winSoundController;

    [SerializeField] private float timeToWin;
    [SerializeField] private EndGameModal _endGameModal;

    public bool isLost { get; private set; }
    
    private void Awake()
    {
        Instance = this;
        
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
        StartCoroutine(LostTimer(LostReason.Power));
    }

    private void OnSteamExplode()
    {
        Debug.Log("STEAM KABOOOOOM");
        ScalebarManager.Instance.isFreezed = true;
        gameOverSoundController.Play(SoundManager.Instance.GetAudioClip("GameLost"));
        explosionSoundController.Play(SoundManager.Instance.GetAudioClip("Explosion"));
        StartCoroutine(CameraShake());
        StartCoroutine(LostTimer(LostReason.Steam));
    }

    private void OnHeatExplode()
    {
        Debug.Log("KABOOOOOM");
        ScalebarManager.Instance.isFreezed = true;
        gameOverSoundController.Play(SoundManager.Instance.GetAudioClip("GameLost"));
        explosionSoundController.Play(SoundManager.Instance.GetAudioClip("Explosion"));
        StartCoroutine(CameraShake());
        StartCoroutine(LostTimer(LostReason.Heat));
    }

    IEnumerator CameraShake()
    {
        yield return new WaitForSeconds(0.5f);
        LightManager.Instance.EnableAlarmLight(false);
        Camera.main.DOShakePosition(3, 0.2f);
    }

    private void OnVictory()
    {
        if (ScalebarManager.Instance.isFreezed)
        {
            return;
        }
        _endGameModal.OpenEndDialog(false);
        winSoundController.Play(SoundManager.Instance.GetAudioClip("WinSound"));
        ScalebarManager.Instance.isFreezed = true;    
    }

    private void Update()
    {
        if (WatchScript.Instance.TimePassed >= timeToWin)
        {
            OnVictory();
        }
    }

    private IEnumerator LostTimer(LostReason lostReason)
    {
        if (isLost)
        {
            yield break;
        }
        isLost = true;
        yield return new WaitForSeconds(2);
        _endGameModal.OpenEndDialog(true, lostReason);
    }
}