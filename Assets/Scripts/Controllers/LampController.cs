using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class LampController : MonoBehaviour
{
    [SerializeField] private Light2D lightSource;
    private bool _isBlinking;
    [SerializeField] private Vector2 noiseOffset;
    private Vector2 noisePosition;

    private void Start()
    {
        lightSource = GetComponent<Light2D>();
        noisePosition = noiseOffset;
        LightManager.Instance.LightSwitched += SwitchLight;
    }

    private void SwitchLight(bool isOn)
    {
        DOTween.Sequence(this).AppendInterval(0.1f).AppendCallback(() =>
        {
            DOTween.To(() => lightSource.intensity, x => lightSource.intensity = x, isOn ? 0.3f : 0, 0.2f);
        }).Play();
    }

    private void Update()
    {
        noisePosition += Vector2.right*(Time.deltaTime * 5);
        if (Mathf.PerlinNoise(noisePosition.x, noisePosition.y) > 0.5f)
        {
            if (!_isBlinking)
            {
                StartCoroutine(Blink(0.2f));
            }
        }
    }

    private IEnumerator Blink(float time)
    {
        _isBlinking = true;
        lightSource.enabled = false;
        yield return new WaitForSeconds(time);
        lightSource.enabled = true;
        yield return new WaitForSeconds(10);
        _isBlinking = false;
    }
}
