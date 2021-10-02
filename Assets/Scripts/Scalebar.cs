using System;
using UnityEngine;

public abstract class Scalebar : MonoBehaviour
{
    public float Value => _value;
    [SerializeField] private float _value;
    public Action<float> ValueChanged { get; set; }

    protected void ChangeValue(float value)
    {
        if (_value == value)
            return;

        if (value < 0)
        {
            if (_value <= 0)
                return;
            _value = 0;
        }
        else if (value > 1)
        {
            if (_value > 1)
                return;
            _value = 1;
        }
        else
        {
            _value = value;
        }

        ValueChanged?.Invoke(value);
    }

    protected abstract void Tick();

    private void Update()
    {
        Tick();
    }
}