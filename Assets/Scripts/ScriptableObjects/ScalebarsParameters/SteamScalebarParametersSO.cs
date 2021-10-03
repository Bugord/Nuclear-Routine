using UnityEngine;

namespace ScriptableObjects.ScalebarsParameters
{
    [CreateAssetMenu(menuName = "Scalebar Parameters/Steam Scalebar")]
    public class SteamScalebarParametersSO : ScriptableObject
    {
        public float heatMod;
        public float waterMod;
        public float baseDecrease;
    }
}
