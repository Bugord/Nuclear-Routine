using System;
using System.Globalization;
using TMPro;
using UnityEngine;

public class WatchScript : MonoBehaviour
{
    public static WatchScript Instance;
    [SerializeField] private TextMeshPro label;
    [SerializeField] private float timeMultiplier;

    [SerializeField] private float _startTimeInSeconds;
    [SerializeField] private float _timeInSeconds;
    private const float SecondsInDay = 60 * 60 * 24;

    public float TimePassed => _timeInSeconds - _startTimeInSeconds;

    private void Awake()
    {
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (ScalebarManager.Instance.isFreezed)
        {
            return;
        }
        
        var timeSpan = TimeSpan.FromSeconds(_timeInSeconds);
        DateTime time = DateTime.Today.Add(timeSpan);
        string displayTime = time.ToString("hh:mm tt", CultureInfo.InvariantCulture);
        label.SetText(displayTime);
        _timeInSeconds += Time.deltaTime * timeMultiplier;
        if (_timeInSeconds >= SecondsInDay)
        {
            _timeInSeconds = 0;
        }
    }
}
