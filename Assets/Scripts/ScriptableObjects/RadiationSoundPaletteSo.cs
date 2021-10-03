using UnityEngine;

[CreateAssetMenu(fileName = "Radiation Sound Palette", menuName = "Radiation Sound Palette")]
public class RadiationSoundPaletteSo : ScriptableObject
{
    [SerializeField] private AudioClip lowRadiation;
    [SerializeField] private AudioClip mediumRadiation;
    [SerializeField] private AudioClip highRadiation;

    public AudioClip GetRadiationClip(float level)
    {
        level = Mathf.Clamp01(level);
        if (level.IsBetweenRange(0, 0.33f))
        {
            return lowRadiation;
        }
        return level.IsBetweenRange(0.33f, 0.66f) ? mediumRadiation : highRadiation;
    }
}
