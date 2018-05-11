using UnityEngine;

namespace Assets.Script.Src.Interaction.Teleportation
{
    public class Teleporter
    {
        private readonly Transform _origin;
        private LineRenderer _lineRenderer;

        public Teleporter(Transform origin, LineRenderer lineRenderer)
        {
            _origin = origin;
            _lineRenderer = lineRenderer;
        }

        public void Teleport(Transform objectToTeleport, Vector3 pointingDirection, float maxDistance)
        {
            var ray = new Ray(_origin.position, pointingDirection);
            RaycastHit hitInfo;
            if (Physics.Raycast(ray, out hitInfo, maxDistance))
            {
                objectToTeleport.position = hitInfo.point;
            }
        }

        public void UpdateTeleportationLine(Vector3 pointingDirection, float maxDistance)
        {
            var ray = new Ray(_origin.position, pointingDirection);
            _lineRenderer.SetPosition(0, _origin.position);
            _lineRenderer.SetPosition(1, ray.GetPoint(maxDistance));
        }
    }
}
