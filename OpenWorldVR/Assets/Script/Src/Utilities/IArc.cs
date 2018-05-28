using UnityEngine;

namespace Assets.Script.Src.Utilities
{
    public interface IArc
    {
        Vector3 CalculateCoordinateAtTime(Vector3 initialTangent, float velocity, float time);
    }
}