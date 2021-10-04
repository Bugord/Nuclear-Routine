using System;

namespace GameEvent
{
    public class HeatDecreaseEvent : BaseGameEvent
    {
        protected override void OnEventStart()
        {
            base.OnEventStart();
            ScalebarManager.Instance.HeatScalebar.SetParameterId(1);
        }
        protected override void OnEventEnd()
        {
            // ScalebarManager.Instance.HeatScalebar.SetParameterId(0);
            base.OnEventEnd();
        }

    }
}