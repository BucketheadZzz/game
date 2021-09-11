using System.Collections.Generic;
using Assets.Scripts.Common;

namespace Assets.Scripts.Level
{
    public class KeysBroadcaster : BaseBroadcaster<KeysBroadcaster>
    {
        public event BroadcastEventObj KeyCountGenerated;
        public event BroadcastEventObj KeyFound;

        protected override IDictionary<Events, BroadcastEventObj> eventMatrix =>
            new Dictionary<Events, BroadcastEventObj>
            {
                {Events.KeyNumberGenerated, RaiseKeyCountGenerated},
                {Events.KeyFound, RaiseKeyFound},
            };

        private void RaiseKeyCountGenerated(object info)
        {
            KeyCountGenerated?.Invoke(info);
        }

        private void RaiseKeyFound(object info)
        {
            KeyFound?.Invoke(info);
        }
    }
}
