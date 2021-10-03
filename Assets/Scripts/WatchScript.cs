using System;
using TMPro;
using UnityEngine;

public class WatchScript : MonoBehaviour
{
    [SerializeField] private TextMeshPro label;
    [SerializeField] private float timeMultiplier;
    
    [SerializeField] private float _timeInSeconds;
    private const float SecondsInDay = 60 * 60 * 24;

    // Update is called once per frame
    void Update()
    {
        var timeSpan = TimeSpan.FromSeconds(_timeInSeconds);
        DateTime time = DateTime.Today.Add(timeSpan);
        string displayTime = time.ToString("hh:mm tt");
        label.SetText(displayTime);
        _timeInSeconds += Time.deltaTime * timeMultiplier;
        if (_timeInSeconds >= SecondsInDay)
        {
            _timeInSeconds = 0;
        }
    }
}
