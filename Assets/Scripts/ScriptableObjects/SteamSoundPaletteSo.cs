using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Steam Sound Palette", menuName = "Steam Sound Palette")]
public class SteamSoundPaletteSo : ScriptableObject
{
    public List<AudioClip> steamClips;
    public List<AudioClip> hornClips;
    public AudioClip pressureClip;
}
