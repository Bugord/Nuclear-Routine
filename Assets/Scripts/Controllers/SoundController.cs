using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundController : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    
    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
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
        if (audioSource.clip != clip)
        {
            Stop();
        }
        Play(clip, volumeFactor);
    }

    public void Play(AudioClip clip, float volumeFactor = 1)
    {
        var distance = (transform.position - Vector3.zero).x;
        var soundFactor = distance * 100 / Screen.width;
        audioSource.volume = (1 - Mathf.Abs(soundFactor)) * volumeFactor;
        if (!audioSource.isPlaying)
        {
            audioSource.clip = clip;
            audioSource.panStereo = soundFactor;
            if (audioSource.volume > 0)
            {
                audioSource.Play();
            }
        }
    }

    public void Stop()
    {
        audioSource.Stop();
    }
}
