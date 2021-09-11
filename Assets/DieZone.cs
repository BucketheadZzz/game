using Assets.Helpers;
using Assets.Scripts.Player;
using UnityEngine;

public class DieZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider collider)
    {
        var player = collider.GetPlayerGameObject();
        if (player != null)
        {
            player.GetComponent<Player>().Suicide(); 
        }
    }
}
