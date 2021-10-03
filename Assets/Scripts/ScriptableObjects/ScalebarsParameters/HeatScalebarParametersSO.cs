using UnityEngine;

namespace ScriptableObjects.ScalebarsParameters
{
    [CreateAssetMenu(menuName = "Scalebar Parameters/Heat Scalebar")]
    public class HeatScalebarParametersSO : ScriptableObject
    {
        public float baseCool;
        public float waterMinLevel;
        public float waterMaxLevel;
        public float waterMinLevelMod;
        public float waterMaxLevelMod;
        public float waterBaseMod;
        public float fuelDeepMod;
    }
}
