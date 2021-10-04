namespace GameEvent
{
    public class PowerUsageEvent : BaseGameEvent
    {
        protected override void OnEventStart()
        {
            base.OnEventStart();
            
            ScalebarManager.Instance.SteamScalebar.SetParameterId(1);
        }

        protected override void OnEventEnd()
        {
            // ScalebarManager.Instance.PowerScalebar.SetParameterId(0);

            base.OnEventEnd();
        }
    }
}