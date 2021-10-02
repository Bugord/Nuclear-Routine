using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowScalebarComponent : MonoBehaviour
{
    [SerializeField] private Transform arrow;
    
    [SerializeField] private float minAngle;
    [SerializeField] private float maxAngle;
    
    private Scalebar _scalebar;
    
    void Awake()
    {
        _scalebar = GetComponent<Scalebar>();
        _scalebar!.ValueChanged += UpdateBar;
    }

    private void UpdateBar(float value)
    {
        arrow.localRotation = Quaternion.Euler(0, 0, Mathf.Lerp(minAngle, maxAngle, value));
    }

    private void OnDestroy()
    {
        _scalebar.ValueChanged -= UpdateBar;
    }
}
