using UnityEngine;
using Vector3 = UnityEngine.Vector3;

namespace Assets.Scripts
{
    public class CameraFollow : MonoBehaviour
    {
        [Header("Player position")]
        [SerializeField]
        private Transform playerPosition;

        private Vector3 cameraOffset;
        private const float smoothness = 0.3f;

    
        private void Start()
        {
            cameraOffset = transform.position - playerPosition.position;
        }

        private void LateUpdate()
        {
            var newPosition = playerPosition.position + cameraOffset;

            transform.position = Vector3.Slerp(transform.position, newPosition, smoothness);
        }
    }
}
