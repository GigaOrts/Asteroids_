using UnityEngine;
using Zenject;

namespace Core
{
    public class SpaceshipPresentation : MonoBehaviour
    {
        private Transform _transform;
        private SpaceshipMovement _movement;
        private SpaceshipInput _input;
        private Camera _camera;

        [Inject]
        public void Construct(SpaceshipMovement movement, SpaceshipInput input)
        {
            _movement = movement;
            _input = input;
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

            _movement.Update(Time.deltaTime);

            _transform.position = _movement.Position;
            _transform.rotation = _movement.Rotation;
        }

        private void TeleportNearBorder()
        {
            if (_transform.position.y > _camera.orthographicSize)
            {
                _movement.Position = new Vector3(_movement.Position.x, -_camera.orthographicSize);
            }
            else if (_transform.position.x > _camera.orthographicSize)
            {
                _movement.Position = new Vector3(-_camera.orthographicSize, _movement.Position.y);
            }
            else if (_transform.position.y < -_camera.orthographicSize)
            {
                _movement.Position = new Vector3(_movement.Position.x, _camera.orthographicSize);
            }
            else if (_transform.position.x < -_camera.orthographicSize)
            {
                _movement.Position = new Vector3(_camera.orthographicSize, _movement.Position.y);
            }
        }

        private void Move()
        {
            if (_input.AccelerationHold)
            {
                _movement.Accelerate(Time.deltaTime);
                _movement.RotateDeceleration(Time.deltaTime);
            }
            else if (_input.BrakeHold)
            {
                _movement.Brake();
                _movement.RotateDeceleration(Time.deltaTime);
            }
            else if (_input.AccelerationHold == false)
            {
                _movement.Decelerate(Time.deltaTime);
            }
        }

        private void Rotate()
        {
            if (_input.RotationRightHold)
            {
                _movement.RotateAccelerationRight(Time.deltaTime);
            }
            else if (_input.RotationLeftHold)
            {
                _movement.RotateAccelerationLeft(Time.deltaTime);
            }
            else
            {
                _movement.RotateDeceleration(Time.deltaTime);
            }
        }
    }
}