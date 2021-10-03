using UnityEngine;

public class ReactorButton : ButtonComponent
{
   [SerializeField] private SterjenGroup sterjenGroup;
   [SerializeField] private bool isUp;

   protected override void OnMouseDrag()
   {
      base.OnMouseDrag();

      if (isUp)
      {
         ScalebarManager.Instance.FuelController.FuelUp(sterjenGroup);
         return;
      }
      ScalebarManager.Instance.FuelController.FuelDown(sterjenGroup);
   }
}
