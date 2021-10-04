using System;
using Newtonsoft.Json.Bson;
using UnityEngine;

public class ReactorButton : ButtonComponent
{
    [SerializeField] private SterjenGroup sterjenGroup;
    [SerializeField] private bool isUp;
    private bool _isDisabled;
    public SterjenGroup SterjenGroup => sterjenGroup;

    protected override void OnMouseDrag()
    {
        if (_isDisabled || ScalebarManager.Instance.isFreezed) return;

        base.OnMouseDrag();

        if (isUp)
        {
            ScalebarManager.Instance.FuelController.FuelUp(sterjenGroup);
            return;
        }

        ScalebarManager.Instance.FuelController.FuelDown(sterjenGroup);
    }

    public void DisableButton()
    {
        _isDisabled = true;
        SetSpritePressed();
    }

    private protected new void OnMouseDown()
    {
        if (_isDisabled || ScalebarManager.Instance.isFreezed) return;
        base.OnMouseDown();
    }

    private protected new void OnMouseUp()
    {
        if (_isDisabled || ScalebarManager.Instance.isFreezed) return;
        base.OnMouseUp();
    }
}