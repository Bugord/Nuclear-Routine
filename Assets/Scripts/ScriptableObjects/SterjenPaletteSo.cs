using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "Sterjen Palette", menuName = "Sterjen Palette")]
    public class SterjenPaletteSo : ScriptableObject
    {
        public Sprite redSterjen;
        public Sprite blueSterjen;
        public Sprite greenSterjen;
    }
}