using UnityEngine;

public class Arc {
    private float _stepsSize;
    public Transform _origin { get; set; }

    public Arc(float stepsSize, Transform origin)
    {
        _stepsSize = stepsSize;
        _origin = origin;
    }

    public Vector3 CalculateCoordinateAtTime(Vector3 initialTangent, float velocity, float time)
    {
        Vector3 coordinates = _origin.position + (_stepsSize * time * initialTangent);
        coordinates.y -= Mathf.Pow(time / velocity, 2);
        return coordinates; 
    }
}
