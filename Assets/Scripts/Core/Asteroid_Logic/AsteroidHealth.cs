using UnityEngine;

namespace Core
{
    public class AsteroidHealth
    {
        private float _value = 3;
        // TODO add async timer on Invincible state
        public void TakeDamage()
        {
            Decrease();
            
            if (_value == 0)
            {
                Debug.Log("Game Over");
            }
        }

        private void Decrease()
        {
            _value = Mathf.Max(_value - 1, 0);
        }
    }
}