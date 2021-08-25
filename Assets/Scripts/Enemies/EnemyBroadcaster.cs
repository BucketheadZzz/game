using System.Collections.Generic;
using Assets.Scripts.Common;

namespace Assets.Scripts.Enemies
{
    public sealed class EnemyBroadcaster : BaseBroadcaster<EnemyBroadcaster>
    {
        public event BroadcastEventObj CharacterKilled;
        public event BroadcastEventObj EnemiesSpawned;

        protected override IDictionary<Events, BroadcastEventObj> eventMatrix => new Dictionary<Events, BroadcastEventObj>
        {
            {Common.Events.CharacterKilled, RaiseCharacterKilled},
            {Common.Events.EnemiesSpawned, RaiseEnemiesSpawned}
        };

        private void RaiseCharacterKilled(object info)
        {
            CharacterKilled?.Invoke(info);
        }

        private void RaiseEnemiesSpawned(object info)
        {
            EnemiesSpawned?.Invoke(info);
        }
    }
}
