using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundController : MonoBehaviour
{
    [SerializeField] private AudioSource audio;
    
    void Awake()
    {
        audio = GetComponent<AudioSource>();
    }

    public void Play(AudioClip clip)
    {
        if (!audio.isPlaying)
        {
            audio.clip = clip;
            audio.pitch = Random.Range(0.5f, 1.3f);
            var distance = (transform.position - Vector3.zero).x;
            var soundFactor = distance * 100 / Screen.width;
            audio.panStereo = soundFactor;
            audio.volume = 1 - Mathf.Abs(soundFactor);
            if (audio.volume > 0)
            {
                audio.Play();
            }
        }
    }
}
