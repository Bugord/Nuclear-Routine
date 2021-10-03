using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Button Sound Palette", menuName = "Button Sound Palette")]
public class ButtonSoundPaletteSo : ScriptableObject
{
    public AudioClip pressed;
    public AudioClip clamped;
    public AudioClip released;
}
