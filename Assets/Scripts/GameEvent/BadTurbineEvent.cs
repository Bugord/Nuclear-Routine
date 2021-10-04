namespace GameEvent
{
    public class BadTurbineEvent : BaseGameEvent
    {
        protected override void OnEventStart()
        {
            base.OnEventStart();
            
            ScalebarManager.Instance.SteamScalebar.SetParameterId(2);
        }

        protected override void OnEventEnd()
        {
            // ScalebarManager.Instance.SteamScalebar.SetParameterId(0);

            base.OnEventEnd();
        }
    }
}