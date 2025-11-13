using UnityEngine;

namespace Player
{
    public class PlayerStats : MonoBehaviour
    {
        [SerializeField] private float playerHp;
        [SerializeField] private float playerStamina;

        public float PlayerHp => playerHp;
        public float PlayerStamina => playerStamina;
        void Start()
        {
        
        }

    
        void Update()
        {
        
        }
    }
}
