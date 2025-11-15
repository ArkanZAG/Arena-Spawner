using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Player
{
    public class PlayerMechanic : MonoBehaviour
    {
        [SerializeField] private Rigidbody playerRigidBody;
        [SerializeField] private Transform mainCamera;
        [Header("Player Base Movement Stats")]
        [SerializeField] private int playerSpeed;
        [SerializeField] private int playerRunSpeed;
        [SerializeField] private int playerJumpHeight;
        [Header("Player Wall Run Stats")]
        [SerializeField] private int playerWallRunSpeed;
        [SerializeField] private float maxWallRunDuration;
        [SerializeField] private float wallRunGravity;
        [SerializeField] private float wallRunDuration;
        
        

        [SerializeField] private bool isGrounded;
        [SerializeField] private bool isOnTheWall;

        private Vector3 wallNormal;
        
        private void Start()
        {
        
        }

    
        private void Update()
        {
            Movement();
            Jump();
            WallRun();
            Sprint();
            CheckWall();
            
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
            if (!isGrounded && isOnTheWall && wallRunDuration < maxWallRunDuration)
            {
                wallRunDuration += Time.deltaTime;

                var direction = GetWallRunDirection();

                playerRigidBody.linearVelocity = direction * playerWallRunSpeed + Vector3.up * wallRunGravity;
                
                playerRigidBody.AddForce(wallNormal * 5f, ForceMode.Acceleration);
            }
            else
            {
                wallRunDuration = 0;
            }
        }

        private void CheckWall()
        {
            RaycastHit hit;

            if (Physics.Raycast(transform.position, transform.right, out hit, 0.1f))
            {
                wallNormal = hit.normal;
                isOnTheWall = true;
            }else if (Physics.Raycast(transform.position, -transform.right, out hit, 0.1f))
            {
                wallNormal = hit.normal;
                isOnTheWall = true;
            }
            else
            {
                isOnTheWall = false;
                wallRunDuration = 0;
            }
        }

        private Vector3 GetWallRunDirection()
        {
            var wallForward = Vector3.Cross(wallNormal, Vector3.up);

            if (Vector3.Dot(wallNormal, transform.forward) < 0)
            {
                wallForward = -wallForward;
            }

            return wallForward;
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
