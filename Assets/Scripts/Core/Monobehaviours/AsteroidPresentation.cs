using System;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace Core
{
    public class AsteroidPresentation : MonoBehaviour
    {
        private AsteroidPhysics _physics;
        private Transform _transform;
        private Camera _camera;

        [Inject]
        public void Construct(AsteroidPhysics physics)
        {
            _physics = physics;
        }

        private void Start()
        {
            _camera = Camera.main;
            _transform = GetComponent<Transform>();
            
            _physics.Angle = Random.Range(0, 360);
            _physics.Rotation = Quaternion.Euler(0, 0, _physics.Angle);
            _physics.MoveDirection = (_physics.Rotation * Vector2.up).normalized;
            
            _physics.Speed_Velocity = 3;
            _physics.Position = 
                new Vector2(
                    Random.Range(-_camera.orthographicSize, _camera.orthographicSize), 
                    Random.Range(-_camera.orthographicSize, _camera.orthographicSize));
            
            Debug.Log($"Initialized Asteroid Position: {_physics.Position}");
        }

        private void Update()
        {
            TeleportNearBorder();
            
            _physics.Accelerate(Time.deltaTime);

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
        
        private void OnCollisionEnter2D(Collision2D other)
        {
            Debug.Log($"{gameObject.name} - Collision");
            _physics.OnCollision(other);
        }
    }
}