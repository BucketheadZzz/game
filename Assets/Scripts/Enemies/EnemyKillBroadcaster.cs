using System;

namespace Assets.Scripts.Enemies
{
    public sealed class EnemyKillBroadcaster
    {

        private static readonly object syncRoot = new object();
        private static EnemyKillBroadcaster current;

        public event KillEvent EnemyKilled;

        public delegate void KillEvent();

        private EnemyKillBroadcaster()
        {
        }

        public static EnemyKillBroadcaster GetCurrentInstance()
        {
            if (current == null)
            {
                lock (syncRoot)
                {
                    if (current == null)
                        current = new EnemyKillBroadcaster();
                }
            }
            return current;
        }

        public void BroadcastEvent()
        {
            EnemyKilled?.Invoke();
        }
    }
}
