using UnityEngine;

namespace Assets.Script.Src.Physics
{
    public interface IRaycast
    {
        bool CheckForObstacleBetweenPoints(Vector3 startPoint, Vector3 endPoint);
    }
}