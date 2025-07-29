using System;
using System.Globalization;
using Core;
using TMPro;
using UnityEngine;
using Zenject;

namespace UI
{
    public class SpaceshipUI : MonoBehaviour
    {
        public TextMeshProUGUI SpaceshipSpeedText;
        public TextMeshProUGUI SpaceshipAngleText;
        public TextMeshProUGUI SpaceshipPositionText;

        private void Awake()
        {
            CultureInfo.CurrentCulture = CultureInfo.GetCultureInfo("en-US");
        }

        [Inject]
        public void Construct(SpaceshipPhysics physics)
        {
            physics.VelocityChanged += UpdateVelocity;
            physics.AngleChanged += UpdateAngle;
            physics.PositionChanged += UpdatePosition;
        }

        private void UpdateVelocity(float velocity)
        {
            SpaceshipSpeedText.text = $"VELOCITY: {velocity:f}";
        }

        private void UpdateAngle(float angle)
        {
            SpaceshipAngleText.text = $"ANGLE: {angle:f}°";
        }

        private void UpdatePosition(Vector2 position)
        {
            SpaceshipPositionText.text = $"POSITION: {position.x:f0}, {position.y:f0}";
        }
    }
}