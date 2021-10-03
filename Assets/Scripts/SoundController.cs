using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundController : MonoBehaviour
{
    [SerializeField] private AudioSource audio;
    
    void Awake()
    {
        audio = GetComponent<AudioSource>();
    }

    public void Play(AudioClip clip, bool skipCurrent)
    {
        if (skipCurrent)
        {
            Stop();
        }
        Play(clip, 1);
    }
    
    public void Play(AudioClip clip, float volumeFactor, bool skipCurrent, bool allowReplace = false)
    {
        if (skipCurrent)
        {
            Stop();
        }
        if (audio.clip != clip)
        {
            Stop();
        }
        Play(clip, volumeFactor);
    }

    public void Play(AudioClip clip, float volumeFactor = 1)
    {
        var distance = (transform.position - Vector3.zero).x;
        var soundFactor = distance * 100 / Screen.width;
        audio.volume = (1 - Mathf.Abs(soundFactor)) * volumeFactor;
        if (!audio.isPlaying)
        {
            audio.clip = clip;
            audio.panStereo = soundFactor;
            if (audio.volume > 0)
            {
                audio.Play();
            }
        }
    }

    public void Stop()
    {
        audio.Stop();
    }
}
