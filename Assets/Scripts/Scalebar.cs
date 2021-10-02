using System;
using UnityEngine;

public abstract class Scalebar : MonoBehaviour
{
    public float Value { get; private set; }
    public Action<float> ValueChanged { get; set; }

    public void ChangeValue(float value)
    {
        ValueChanged?.Invoke(value);
    }

    private void Tick()
    {
        
    }
}
