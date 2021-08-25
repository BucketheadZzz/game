using System;
using UnityEngine;

namespace Assets.Scripts.Common
{
    [Serializable]
    public class PlayerInfo
    {
        [SerializeField]
        private int maxHp = 100;

        [SerializeField]
        private int currentHp = 100;

        public int Max
        {
            get { return maxHp; }
            set { maxHp = value; }
        }

        public int CurrentHp
        {
            get { return currentHp; }
            set { currentHp = value; }
        }
    }
}
