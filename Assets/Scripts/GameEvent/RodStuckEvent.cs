using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace GameEvent
{
    public class RodStuckEvent : BaseGameEvent
    {
        [SerializeField] private List<ReactorButton> _upReactorButtons;
        protected override void OnEventStart()
        {
            var group = (SterjenGroup) Random.Range(0, 2);
            eventModalContent.description = eventModalContent.description.Replace("{group}", group.ToString());
            base.OnEventStart();

            _upReactorButtons.Where(x => x.SterjenGroup == group).ToList().ForEach(x => x.DisableButton());
            ScalebarManager.Instance.FuelController.StuckRods(group);
        }

        protected override void OnEventEnd()
        {
            base.OnEventEnd();
        }
    }
}