using System;
using UnityEngine;

namespace Core
{
    public class AsteroidFactory : MonoBehaviour
    {
        public AsteroidPresentation asteroidPresentationPrefab;
        
        private void Start()
        {
            Create();
        }

        public void Create()
        {
            var position = Vector3.one;
            var rotation = Quaternion.identity;
            Instantiate(asteroidPresentationPrefab, position, rotation);
        }
    }
}