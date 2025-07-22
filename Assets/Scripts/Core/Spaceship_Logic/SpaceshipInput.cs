using UnityEngine;

namespace Core
{
    public class SpaceshipInput
    {
        public bool AccelerationHold => 
            Input.GetKey(SpaceshipKeyBinds.Acceleration) || 
            Input.GetKey(SpaceshipKeyBinds.AccelerationAlt);
        
        public bool RotationLeftHold => 
            Input.GetKey(SpaceshipKeyBinds.RotationLeft) || 
            Input.GetKey(SpaceshipKeyBinds.RotationLeftAlt);
        
        public bool RotationRightHold => 
            Input.GetKey(SpaceshipKeyBinds.RotationRight) || 
            Input.GetKey(SpaceshipKeyBinds.RotationRightAlt);
        
        public bool BrakeHold => 
            Input.GetKey(SpaceshipKeyBinds.Brake) || 
            Input.GetKey(SpaceshipKeyBinds.BrakeAlt);
    }
}