using System.Collections.Generic;
using Assets.Scripts.Common;

namespace Assets.Scripts.Player
{
    public class PlayerBroadcaster : BaseBroadcaster<PlayerBroadcaster>
    {
        public event BroadcastEventObj HpChanged;

        protected override IDictionary<Events, BroadcastEventObj> eventMatrix =>
            new Dictionary<Events, BroadcastEventObj>()
            {
                {Events.PlayerHpChanged, RaiseHpChanged}
            };

        private void RaiseHpChanged(object info)
        {
            HpChanged?.Invoke(info);
        }
    }
}
