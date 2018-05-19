using UnityEngine;

namespace Assets.Script.Src.Interaction.Teleportation
{
    public class Teleporter
    {
        private readonly Transform _origin;
        private LineRenderer[] _lineRenderer;

        public Teleporter(Transform origin, LineRenderer[] lineRenderer)
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
            int i = 0;
            Vector3 lastPosition = _origin.position;
            foreach (LineRenderer lineRenderer in _lineRenderer)
            {
                lineRenderer.enabled = true;
                Vector3 nextPosition = _origin.position + ((float)++i/10) * pointingDirection;
                nextPosition.y -= Mathf.Pow(((float)i)/30, 2);
                lineRenderer.SetPosition(0, lastPosition);
                lineRenderer.SetPosition(1, nextPosition);
                lastPosition = nextPosition;
            }
        }

        public void HideTeleportationLine()
        {
            foreach (LineRenderer lineRenderer in _lineRenderer)
            {
                lineRenderer.enabled = false;
            }
        }
    }
}
