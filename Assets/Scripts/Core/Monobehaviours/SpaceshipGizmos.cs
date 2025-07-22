using System;
using UnityEngine;
using Zenject;

namespace Core
{
    public class SpaceshipGizmos : MonoBehaviour
    {
        private SpaceshipMovement _movement;
        
        [Inject]
        public void Construct(SpaceshipMovement movement)
        {
            _movement = movement;
        }

        private void LateUpdate()
        {
            Debug.DrawLine(_movement.Position, _movement.Position + _movement.Velocity, Color.green);
            Debug.DrawLine(_movement.Position, _movement.Position + _movement.Direction, Color.blue);
        }
    }
}