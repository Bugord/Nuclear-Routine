using UnityEngine;

public class FuelScalebar : Scalebar
{
    public float Deep => deep;
    [Range(0,1f)]
    [SerializeField] private float deep;
    
    protected override void Tick()
    {
        
    }

    public void ChangeDeep(float value) => deep = Mathf.Clamp01(deep + value); 
}