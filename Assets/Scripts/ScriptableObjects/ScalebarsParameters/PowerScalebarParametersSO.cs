using UnityEngine;

namespace ScriptableObjects.ScalebarsParameters
{
    [CreateAssetMenu(menuName = "Scalebar Parameters/Power Scalebar")]
    public class PowerScalebarParametersSO : ScriptableObject
    {
        public float baseValue;
        public float increasedMod;
        public float decreaseMod;
        public float steamMin;
        public float steamMax;
    }
}