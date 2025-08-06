using System;
using UnityEngine;

namespace Core
{
    public class SpaceshipHealth
    {
        public int Value { get; private set; }
        // TODO add async timer on Invincible state
        
        public event Action<int> HealthChanged;

        public SpaceshipHealth()
        {
            Value = 3;
        }
        
        public void TakeDamage()
        {
            Decrease();
            
            if (Value == 0)
            {
                Debug.Log("Game Over");
            }
        }

        private void Decrease()
        {
            Value = Mathf.Max(Value - 1, 0);
            HealthChanged?.Invoke(Value);
        }
    }
}