using Assets.Scripts.Level;
using UnityEngine;

namespace Assets.Scripts.Common
{
    public class Door : MonoBehaviour
    {

        [SerializeField]
        private bool isOpenableDoor;

        private GameObject thedoor;
        private int requiredKeyCount;
        private bool isOpen;

        private void Awake()
        {
            KeysBroadcaster.Instance.KeyCountGenerated += OnKeyCountGenerated;
        }

        private void OnKeyCountGenerated(object info)
        {
            requiredKeyCount = (int)info;
        }

        void OnTriggerEnter(Collider obj)
        {
            if (isOpenableDoor && !isOpen)
            {
                var player = obj.gameObject.GetComponent<Player.Player>();
                if (player && player.ItemsCount == requiredKeyCount)
                {
                    thedoor = GameObject.FindWithTag("SF_Door");
                    thedoor.GetComponent<Animation>().Play("open");
                    isOpen = true;
                }
            }
        }

        void OnTriggerExit(Collider obj)
        {
            if (isOpenableDoor && isOpen)
            {
                var player = obj.gameObject.GetComponent<Player.Player>();
                if (player)
                {
                    thedoor = GameObject.FindWithTag("SF_Door");
                    thedoor.GetComponent<Animation>().Play("close");
                    isOpen = false;
                }
            }
        }
    }
}