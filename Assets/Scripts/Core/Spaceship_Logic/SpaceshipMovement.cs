using System;
using UnityEngine;

namespace Core
{
    public class SpaceshipMovement
    {
        public float Speed;
        public float MaxSpeed = 10;

        public float Acceleration;
        public float Deceleration;

        public float BrakeSpeed;
        public float RotationSpeed;
        public float MaxRotationSpeed = 90;
        public float RotationAcceleration;
        public float RotationDeceleration;
        public float Angle;
        public Vector2 Position;
        public Vector2 Velocity;
        public Quaternion Rotation;

        public event Action<float> VelocityChanged;
        public event Action<Vector2> PositionChanged;
        public event Action<float> AngleChanged;

        public SpaceshipMovement()
        {
            Velocity = Vector2.up;
            Direction = Vector2.up;
            Position = Vector2.zero;
            Rotation = Quaternion.identity;
            Speed = 0;
            RotationSpeed = 0;
            Angle = 0;

            // Acceleration = MaxSpeed;
            Acceleration = MaxSpeed * 0.001f;
            Deceleration = MaxSpeed * 0.0005f;

            // RotationAcceleration = MaxRotationSpeed;
            RotationAcceleration = MaxRotationSpeed * 0.05f;
            RotationDeceleration = MaxRotationSpeed * 0.0005f;

            BrakeSpeed = MaxSpeed * 0.01f;
        }

        public void Update(float dt)
        {
            UpdateRotation(dt);
            UpdatePosition(dt);
        }

        public Vector2 Direction;

        public void Accelerate()
        {
            Speed = Mathf.Min(Speed + Acceleration, MaxSpeed);
            
            Velocity = Direction * Speed;
            
            VelocityChanged?.Invoke(Velocity.magnitude);
        }

        public void Decelerate()
        {
            Speed = Mathf.Max(Speed - Deceleration, 0);
            Velocity = Velocity.normalized * Speed;
            
            VelocityChanged?.Invoke(Velocity.magnitude);
        }

        public void Brake()
        {
            Speed = Mathf.Max(Speed - BrakeSpeed, 0);
            Direction = Rotation * Vector2.up;
            Velocity = Direction * Speed;
            
            VelocityChanged?.Invoke(Velocity.magnitude);
        }

        public void RotateAccelerationRight()
        {
            RotationSpeed = Mathf.Max(RotationSpeed - RotationAcceleration, -MaxRotationSpeed);
        }

        public void RotateAccelerationLeft()
        {
            RotationSpeed = Mathf.Min(RotationSpeed + RotationAcceleration, MaxRotationSpeed);
        }

        public void RotateDeceleration()
        {
            if (RotationSpeed < 0)
            {
                RotationSpeed = Mathf.Min(RotationSpeed + RotationDeceleration, 0);
            }
            else if (RotationSpeed > 0)
            {
                RotationSpeed = Mathf.Max(RotationSpeed - RotationDeceleration, 0);
            }
        }

        private void UpdatePosition(float dt)
        {
            Position += Velocity * dt;
            PositionChanged?.Invoke(Position);
        }

        private void UpdateRotation(float dt)
        {
            Angle += RotationSpeed * dt;
            Rotation = Quaternion.Euler(0, 0, Angle);
            AngleChanged?.Invoke(Rotation.eulerAngles.z);
            // TODO: Make between -180 to 180. Not 0 to 360
            Direction = (Rotation * Vector2.up).normalized;
        }
    }
}