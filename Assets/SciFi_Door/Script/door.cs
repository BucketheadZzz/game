using UnityEngine;

namespace Assets.SciFi_Door.Script
{
    public class door : MonoBehaviour {
        GameObject thedoor;

        void OnTriggerEnter ( Collider obj  ){
            thedoor= GameObject.FindWithTag("SF_Door");
            thedoor.GetComponent<Animation>().Play("open");
        }

        void OnTriggerExit ( Collider obj  ){
            thedoor= GameObject.FindWithTag("SF_Door");
            thedoor.GetComponent<Animation>().Play("close");
        }
    }
}