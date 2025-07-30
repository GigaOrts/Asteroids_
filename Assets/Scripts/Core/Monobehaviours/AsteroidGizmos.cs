using System;
using UnityEngine;
using Zenject;

namespace Core
{
    [RequireComponent(typeof(AsteroidPresentation))]
    public class AsteroidGizmos : MonoBehaviour
    {
        private AsteroidPhysics _physics;

        private void Awake()
        {
            _physics = GetComponent<AsteroidPresentation>().Physics;
        }

        private void LateUpdate()
        {
            Debug.DrawLine(_physics.Position, _physics.Position + _physics.Velocity, Color.green);
            Debug.DrawLine(_physics.Position, _physics.Position + _physics.MoveDirection, Color.blue);
            
            Debug.Log($"Gizmos position: {_physics.Position}, Velocity: {_physics.Velocity}, Move Direction: {_physics.MoveDirection}");

        }
    }
}