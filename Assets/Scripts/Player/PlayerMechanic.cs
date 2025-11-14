using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Player
{
    public class PlayerMechanic : MonoBehaviour
    {
        [SerializeField] private Rigidbody playerRigidBody;
        [SerializeField] private Transform mainCamera;
        
        [SerializeField] private int playerSpeed;
        [SerializeField] private int playerRunSpeed;
        [SerializeField] private int playerJumpHeight;
        [SerializeField] private float maxWallRunDuration;
        [SerializeField] private float wallRunDuration;
        

        [SerializeField] private bool isGrounded;
        [SerializeField] private bool isOnTheWall;
        
        private void Start()
        {
        
        }

    
        private void Update()
        {
            Movement();
            Jump();
            WallRun();
            Sprint();
        }

        private void Movement()
        {
            var horizontalInput = Input.GetAxis("Horizontal");
            var verticalInput = Input.GetAxis("Vertical");
            

            var forward = mainCamera.transform.forward;
            var right = mainCamera.transform.right;

            forward.y = 0;
            right.y = 0;

            forward.Normalize();
            right.Normalize();

            var moveDirection = (forward * verticalInput + right * horizontalInput).normalized * playerSpeed;
            if (horizontalInput != 0 || verticalInput != 0)
            {
                playerRigidBody.linearVelocity =
                    new Vector3(moveDirection.x, playerRigidBody.linearVelocity.y, moveDirection.z);
            }
            else
            {
                playerRigidBody.linearVelocity = new Vector3(0, playerRigidBody.linearVelocity.y, 0);
            }
            
        }

        private void Jump()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (isGrounded == true)
                {
                    playerRigidBody.linearVelocity = new Vector3(playerRigidBody.linearVelocity.x, playerJumpHeight,
                        playerRigidBody.linearVelocity.z);
                    isGrounded = false;
                }
                
            }
        }

        private void WallRun()
        {
            if (isOnTheWall == true && wallRunDuration < maxWallRunDuration)
            {
                wallRunDuration += Time.deltaTime;
            }
            else
            {
                wallRunDuration = 0;
                isOnTheWall = false;
            }
        }

        private void Sprint()
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                playerSpeed *= playerRunSpeed;
            }
        }

        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.tag == "Ground")
            {
                isGrounded = true;
            }

            if (other.gameObject.tag == "Wall")
            {
                isOnTheWall = true;
            }
        }
    }
}
