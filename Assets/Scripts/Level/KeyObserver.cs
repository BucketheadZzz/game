using System;
using System.Linq;
using Assets.Scripts.Common;
using UnityEngine;

namespace Assets.Scripts.Level
{
    [Serializable]
    public class KeyObserver : IObserver
    {
        private KeyGenerator keyGenerator;

        public void Start()
        {
            //there is only one key generator
            keyGenerator = Resources.FindObjectsOfTypeAll(typeof(KeyGenerator)).First() as KeyGenerator;
            keyGenerator?.Init();
        }
    }
}
