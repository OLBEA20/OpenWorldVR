using UnityEngine;

namespace Assets.Script.Src.Utilities
{
    public class Arc : IArc{
        private readonly float _stepsSize;
        private readonly Transform _origin; 

        public Arc(float stepsSize, Transform origin)
        {
            _stepsSize = stepsSize;
            _origin = origin;
        }

        public Vector3 CalculateCoordinateAtTime(Vector3 initialTangent, float velocity, float time)
        {
            var coordinates = _origin.position + (_stepsSize * time * initialTangent);
            coordinates.y -= Mathf.Pow(time / velocity, 2);
            return coordinates; 
        }
    }
}
