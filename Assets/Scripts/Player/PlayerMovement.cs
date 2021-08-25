using System;
using UnityEngine;

namespace Assets.Scripts.Player
{
    [Serializable]
    public class PlayerMovement
    {
        #region Movement
        [SerializeField]
        private float playerSpeed = 8.0f;
        private Vector3 playerVelocity;
        private bool groundedPlayer;
        private float jumpHeight = 8.0f;
        private float gravityValue = -9.81f;
        private CharacterController characterController;
        private Transform transform;
        private bool isJump;

        #endregion

        public PlayerMovement(Transform transform, CharacterController characterController)
        {
            this.transform = transform;
            this.characterController = characterController;
        }

        // Update is called once per frame
        public void Move()
        {
            groundedPlayer = characterController.isGrounded;

            var move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

            if (move != Vector3.zero)
            {
                characterController.Move(move * Time.deltaTime * playerSpeed);
                transform.forward = move;
            }

            if (Input.GetButtonDown("Jump") && !isJump)
            {
                playerVelocity.y += Mathf.Sqrt(jumpHeight * -1.0f * gravityValue);
                isJump = true;
            }

            if (!groundedPlayer)
            {
                playerVelocity.y += gravityValue * Time.deltaTime;
            }
            else if (playerVelocity.y < 0)
            {
                playerVelocity.y = 0;
                isJump = false;
            }

            characterController.Move(playerVelocity * Time.deltaTime);
        }
    }

}
