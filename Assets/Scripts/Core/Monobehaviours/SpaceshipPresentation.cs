using System;
using UnityEngine;
using Zenject;

namespace Core
{
    public class SpaceshipPresentation : MonoBehaviour
    {
        private Transform _transform;
        private SpaceshipPhysics _physics;
        private SpaceshipInput _input;
        private SpaceshipHealth _health;
        private Camera _camera;

        [Inject]
        public void Construct(SpaceshipPhysics physics, SpaceshipInput input, SpaceshipHealth health)
        {
            _physics = physics;
            _input = input;
            _health = health;
        }

        private void Start()
        {
            _camera = Camera.main;

            _transform = GetComponent<Transform>();
        }

        private void Update()
        {
            TeleportNearBorder();
            Move();
            Rotate();

            _physics.Update(Time.deltaTime);

            _transform.position = _physics.Position;
            _transform.rotation = _physics.Rotation;
        }

        private void TeleportNearBorder()
        {
            if (_transform.position.y > _camera.orthographicSize)
            {
                _physics.Position = new Vector3(_physics.Position.x, -_camera.orthographicSize);
            }
            else if (_transform.position.x > _camera.orthographicSize)
            {
                _physics.Position = new Vector3(-_camera.orthographicSize, _physics.Position.y);
            }
            else if (_transform.position.y < -_camera.orthographicSize)
            {
                _physics.Position = new Vector3(_physics.Position.x, _camera.orthographicSize);
            }
            else if (_transform.position.x < -_camera.orthographicSize)
            {
                _physics.Position = new Vector3(_camera.orthographicSize, _physics.Position.y);
            }
        }

        private void Move()
        {
            if (_input.AccelerationHold)
            {
                _physics.Accelerate(Time.deltaTime);
                _physics.RotateDeceleration(Time.deltaTime);
            }
            else if (_input.BrakeHold)
            {
                _physics.Brake();
                _physics.RotateDeceleration(Time.deltaTime);
            }
            else if (_input.AccelerationHold == false)
            {
                _physics.Decelerate(Time.deltaTime);
            }
        }

        private void Rotate()
        {
            if (_input.RotationRightHold)
            {
                _physics.RotateAccelerationRight(Time.deltaTime);
            }
            else if (_input.RotationLeftHold)
            {
                _physics.RotateAccelerationLeft(Time.deltaTime);
            }
            else
            {
                _physics.RotateDeceleration(Time.deltaTime);
            }
        }


        private void OnCollisionEnter2D(Collision2D other)
        {
            _physics.OnCollision(other);
            _health.TakeDamage();
        }
    }
}