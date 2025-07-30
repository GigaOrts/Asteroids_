using System;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace Core
{
    public class AsteroidPresentation : MonoBehaviour
    {
        private Transform _transform;
        private Camera _camera;

        public AsteroidPhysics Physics { get; private set; }

        [Inject]
        public void Construct(AsteroidPhysics physics)
        {
            Physics = physics;
        }

        private void Start()
        {
            _camera = Camera.main;
            _transform = GetComponent<Transform>();
            
            Physics.Angle = Random.Range(0, 360);
            Physics.Rotation = Quaternion.Euler(0, 0, Physics.Angle);
            Physics.MoveDirection = (Physics.Rotation * Vector2.up).normalized;
            
            Physics.Speed_Velocity = 3;
            Physics.Position = 
                new Vector2(
                    Random.Range(-_camera.orthographicSize, _camera.orthographicSize), 
                    Random.Range(-_camera.orthographicSize, _camera.orthographicSize));
            
            Debug.Log($"Initialized Asteroid Position: {Physics.Position}");
        }

        private void Update()
        {
            TeleportNearBorder();
            
            Physics.Accelerate(Time.deltaTime);

            Physics.Update(Time.deltaTime);

            _transform.position = Physics.Position;
            _transform.rotation = Physics.Rotation;
        }

        private void TeleportNearBorder()
        {
            if (_transform.position.y > _camera.orthographicSize)
            {
                Physics.Position = new Vector3(Physics.Position.x, -_camera.orthographicSize);
            }
            else if (_transform.position.x > _camera.orthographicSize)
            {
                Physics.Position = new Vector3(-_camera.orthographicSize, Physics.Position.y);
            }
            else if (_transform.position.y < -_camera.orthographicSize)
            {
                Physics.Position = new Vector3(Physics.Position.x, _camera.orthographicSize);
            }
            else if (_transform.position.x < -_camera.orthographicSize)
            {
                Physics.Position = new Vector3(_camera.orthographicSize, Physics.Position.y);
            }
        }
        
        private void OnCollisionEnter2D(Collision2D other)
        {
            Debug.Log($"{gameObject.name} - Collision");
            Physics.OnCollision(other);
        }
    }
}