using System;
using UnityEngine;

namespace Core
{
    public class SpaceshipMovement
    {
        public float SmoothRotationTime = 0.11f;
        public Vector2 SmoothRotationVelocity;
        
        public float Speed_Velocity;
        public float MaxSpeed_Velocity = 5;

        public float Acceleration_Velocity;
        public float Deceleration_Velocity;

        public float BrakeSpeed;
        public float RotationSpeed;
        public float MaxRotationSpeed = 360;
        public float RotationAcceleration;
        public float RotationDeceleration;
        
        public float Angle;
        
        public Vector2 Position;
        public Vector2 MoveDirection;
        public Vector2 Velocity;
        
        public Quaternion Rotation;

        public event Action<float> VelocityChanged;
        public event Action<Vector2> PositionChanged;
        public event Action<float> AngleChanged;

        public SpaceshipMovement()
        {
            Velocity = Vector2.up;
            MoveDirection = Vector2.up;
            Position = Vector2.zero;
            Rotation = Quaternion.identity;
            Speed_Velocity = 0;
            RotationSpeed = 0;
            Angle = 0;
            
            Acceleration_Velocity = MaxSpeed_Velocity * 0.5f;
            Deceleration_Velocity = MaxSpeed_Velocity * 0.5f;
            
            RotationAcceleration = MaxRotationSpeed * 0.9f;
            RotationDeceleration = MaxRotationSpeed * 0.3f;

            BrakeSpeed = MaxSpeed_Velocity * 0.01f;
        }

        public void Update(float dt)
        {
            UpdateRotation(dt);
            UpdatePosition(dt);
        }
        
        public void Accelerate(float dt)
        {
            if (Vector2.Dot(Velocity, MoveDirection) < -0.8)
            {
                SmoothRotationTime = 0.05f;
            }
            else
            {
                SmoothRotationTime = 0.11f;
            }
            
            Speed_Velocity = Mathf.Min(Speed_Velocity + Acceleration_Velocity * dt, MaxSpeed_Velocity);

            var smoothVelocity = Vector2.SmoothDamp(
                Velocity, 
                MoveDirection, 
                ref SmoothRotationVelocity, 
                SmoothRotationTime);
            
            Velocity = smoothVelocity.normalized * Speed_Velocity;
            
            VelocityChanged?.Invoke(Velocity.magnitude);
        }

        public void Decelerate(float dt)
        {
            Speed_Velocity = Mathf.Max(Speed_Velocity - Deceleration_Velocity * dt, 0f);
            Velocity = Velocity.normalized * Speed_Velocity;

            if (Speed_Velocity == 0)
            {
                Velocity = Vector2.zero;
            }
            
            VelocityChanged?.Invoke(Velocity.magnitude);
        }

        //TODO Switch acceleration and shit to SmoothDump
        
        public void Brake()
        {
            // Speed_Velocity = Mathf.Max(Speed_Velocity - BrakeSpeed, 0);
            // Velocity = MoveDirection * Speed_Velocity;
            // VelocityChanged?.Invoke(Velocity.magnitude);
        }

        public void RotateAccelerationRight(float dt)
        {
            RotationSpeed = Mathf.Max(RotationSpeed - RotationAcceleration * dt, -MaxRotationSpeed);
        }

        public void RotateAccelerationLeft(float dt)
        {
            RotationSpeed = Mathf.Min(RotationSpeed + RotationAcceleration * dt, MaxRotationSpeed);
        }

        public void RotateDeceleration(float dt)
        {
            if (RotationSpeed < 0)
            {
                RotationSpeed = Mathf.Min(RotationSpeed + RotationDeceleration * dt, 0);
            }
            else if (RotationSpeed > 0)
            {
                RotationSpeed = Mathf.Max(RotationSpeed - RotationDeceleration * dt, 0);
            }
        }
        
        private void UpdateRotation(float dt)
        {
            Angle += RotationSpeed * dt;
            Rotation = Quaternion.Euler(0, 0, Angle);
            // TODO: Make between -180 to 180. Not 0 to 360
            
            MoveDirection = (Rotation * Vector2.up).normalized;
            AngleChanged?.Invoke(Rotation.eulerAngles.z);
        }

        private void UpdatePosition(float dt)
        {
            Position += Velocity * dt;
            
            PositionChanged?.Invoke(Position);
        }
    }
}