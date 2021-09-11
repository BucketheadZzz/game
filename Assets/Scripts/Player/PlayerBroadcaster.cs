using System.Collections.Generic;
using Assets.Scripts.Common;

namespace Assets.Scripts.Player
{
    public class PlayerBroadcaster : BaseBroadcaster<PlayerBroadcaster>
    {
        public event BroadcastEventObj HpChanged;
        public event BroadcastEventObj PlayerDied;

        protected override IDictionary<Events, BroadcastEventObj> eventMatrix =>
            new Dictionary<Events, BroadcastEventObj>()
            {
                {Events.PlayerHpChanged, RaiseHpChanged},
                {Events.PlayerDied, RaisePlayerDied}
            };

        private void RaiseHpChanged(object info)
        {
            HpChanged?.Invoke(info);
        }

        private void RaisePlayerDied(object info)
        {
            PlayerDied?.Invoke(info);
        }
    }
}
