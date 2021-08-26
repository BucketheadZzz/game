using System.Collections.Generic;

namespace Assets.Scripts.Common
{
    public class BaseBroadcaster<T> where T: class, new()
    {
        private static T instance;
        private static object syncRoot = new object();

        public delegate void BroadcastEventObj(object info);

        protected virtual IDictionary<Events, BroadcastEventObj> eventMatrix => new Dictionary<Events, BroadcastEventObj>();

        public static T Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                            instance = new T();
                    }
                }
                return instance;
            }
        }

        public void BroadcastEvent(Events type, object info = null)
        {
            eventMatrix[type]?.Invoke(info);
        }
    }
}
