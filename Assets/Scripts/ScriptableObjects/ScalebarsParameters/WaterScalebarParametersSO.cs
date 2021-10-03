using UnityEngine;

namespace ScriptableObjects.ScalebarsParameters
{
        [CreateAssetMenu(menuName = "Scalebar Parameters/Water Scalebar")]
        public class WaterScalebarParametersSO : ScriptableObject
        {
                public float baseWaterReduction = 0.1f;
                public float heatMod;
                public float heatMinMod;
                public float heatMaxMod;
        }
}
