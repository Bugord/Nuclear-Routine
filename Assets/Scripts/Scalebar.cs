using System;
using UnityEngine;

public enum Difficulty
{
    Easy,
    Normal,
    Hard
}
public abstract class Scalebar : MonoBehaviour
{
    public float Value => _value;
    [SerializeField] private float _value;
    public Action<float> ValueChanged { get; set; }
    [SerializeField] protected Difficulty _currentDifficulty;
    protected int _currentDifficultyId;

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
        if (!ScalebarManager.Instance.isFreezed)
        {
            Tick();
        }
    }
}