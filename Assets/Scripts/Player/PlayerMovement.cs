using System;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

namespace Assets.Scripts.Player
{
    [Serializable]
    public class PlayerMovement
    {
        #region Movement
        [SerializeField]
        private float playerSpeed = 10.0f;
        private float jumpHeight = 40.0f;
        private bool isJump;
        private Rigidbody rigidbody;

        #endregion

        public PlayerMovement(Rigidbody rigidbody)
        {
            this.rigidbody = rigidbody;
        }

        // Update is called once per frame
        public void Move()
        {

            var move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            move.Normalize();

            if (move != Vector3.zero)
            {
                move = move * playerSpeed * Time.deltaTime;
                rigidbody.MovePosition(rigidbody.position + move);  
                rigidbody.transform.forward = move;
            }

            if (Input.GetButtonDown("Jump") && !isJump)
            {
                Debug.Log("Jump");
                rigidbody.AddForce(Vector3.up * jumpHeight, ForceMode.Impulse);
                isJump = true;
            }
        }


        public void OnCollisionEnter(Collision collisionInfo)
        {
            isJump = collisionInfo.transform.tag == "Ground";
        }
    }

}
