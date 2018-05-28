using UnityEngine;

namespace Assets.Script.Src.Physics
{
    public class UnityRaycast : IRaycast
    {
        public bool CheckForObstacleBetweenPoints(Vector3 startPoint, Vector3 endPoint)
        {
            var ray = new Ray(startPoint, endPoint - startPoint);
            return UnityEngine.Physics.Raycast(ray, (endPoint - startPoint).magnitude);
        }
    }
}