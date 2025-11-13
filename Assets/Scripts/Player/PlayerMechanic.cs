using UnityEngine;

namespace Player
{
    public class PlayerMechanic : MonoBehaviour
    {
        [SerializeField] private Rigidbody playerRigidBody;
        [SerializeField] private int playerSpeed;
        [SerializeField] private int playerRunSpeed;
        [SerializeField] private int playerJumpHeight;
        
        private void Start()
        {
        
        }

    
        private void Update()
        {
            Movement();
            Jump();
            Sprint();
        }

        private void Movement()
        {
            var horizontalInput = Input.GetAxis("Horizontal");
            var verticalInput = Input.GetAxis("Vertical");
            playerRigidBody.linearVelocity =
                new Vector3(horizontalInput * playerSpeed, playerRigidBody.linearVelocity.y, verticalInput * playerSpeed);
        }

        private void Jump()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                playerRigidBody.linearVelocity = new Vector3(playerRigidBody.linearVelocity.x, playerJumpHeight,
                    playerRigidBody.linearVelocity.z);
            }
        }

        private void Sprint()
        {
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                playerSpeed *= playerRunSpeed;
            }
        }
    }
}
