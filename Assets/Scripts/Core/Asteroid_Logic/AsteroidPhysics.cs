using System;
using UnityEngine;

namespace Core
{
    public class AsteroidPhysics
    {
        public float Speed_Velocity;
        public float MaxSpeed_Velocity = 2.5f;

        public float Acceleration_Velocity;
        public float Angle;
        
        public Vector2 Position;
        public Vector2 MoveDirection;
        public Vector2 Velocity;
        
        public Quaternion Rotation;

        public bool CanMove;

        public AsteroidPhysics()
        {
            Velocity = Vector2.up;
            MoveDirection = Vector2.up;
            Rotation = Quaternion.identity;
            Speed_Velocity = 0;
            Angle = 0;
            
            Acceleration_Velocity = MaxSpeed_Velocity * 0.5f;
            CanMove = true;
        }

        private float canMoveTimer;
        private float canMoveDuration = 0.5f;

        public void Update(float dt)
        {
            UpdatePosition(dt);

            LockMovementCountdown(dt);
        }

        private void LockMovementCountdown(float dt)
        {
            if (!CanMove)
            {
                canMoveTimer += dt;

                if (canMoveTimer >= canMoveDuration)
                {
                    canMoveTimer = 0;
                    CanMove = true;
                }
            }
        }

        public void Accelerate(float dt)
        {
            if (!CanMove)
                return;
            
            Speed_Velocity = Mathf.Min(Speed_Velocity + Acceleration_Velocity * dt, MaxSpeed_Velocity);

            Velocity = MoveDirection * Speed_Velocity;
        }

        private void UpdatePosition(float dt)
        {
            Position += Velocity * dt;
        }

        public void OnCollision(Collision2D other)
        {
            if (!CanMove)
                return;
            
            Vector2 pushDirection = (Position - (Vector2)other.transform.position).normalized;
            
            var pushFactor = 0.5f;
            // TODO if u stay, Speed is 0, so u dont move on bump
            Velocity += pushDirection * Speed_Velocity * pushFactor;
            
            MoveDirection = Velocity.normalized;
            
            if (Velocity.magnitude > MaxSpeed_Velocity)
            {
                Velocity = Velocity.normalized * MaxSpeed_Velocity;
            }

            CanMove = false;
        }
    }
}