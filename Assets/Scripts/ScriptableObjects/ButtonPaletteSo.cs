using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Button Palette", menuName = "Button Palette")]
public class ButtonPaletteSo : ScriptableObject
{
    public Sprite pressed;
    public Sprite released;
}
