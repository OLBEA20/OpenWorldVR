using UnityEngine;

namespace Assets.Script.Src.Interaction.Teleportation
{
    public class Teleporter
    {
        private readonly Transform _origin;
        private readonly TeleportationTarget _teleportationTarget;
        private readonly LineRenderer[] _lineRenderer;
        private readonly float _steps = 0.005f;
        private readonly float _gravity = 500;
        private bool _collided;

        private readonly Arc _arc;

        public Teleporter(Transform origin, TeleportationTarget teleporationTarget, LineRenderer[] lineRenderer)
        {
            _origin = origin;
            _teleportationTarget = teleporationTarget;
            _arc = new Arc(_steps, _origin);
            _lineRenderer = lineRenderer;
        }

        public void Teleport(Transform objectToTeleport)
        {
            _teleportationTarget.Teleport(objectToTeleport);
        }

        public void UpdateTeleportationArc(Vector3 pointingDirection)
        {
            _collided = false;
            UpdateTeleporationLines(pointingDirection); 
        }

        private void UpdateTeleporationLines(Vector3 pointingDirection)
        {
            var i = 0;
            _teleportationTarget.Hide();
            var lastPosition = _arc.CalculateCoordinateAtTime(pointingDirection, _gravity, i);
            foreach (var lineRenderer in _lineRenderer)
            {
                var nextPosition = _arc.CalculateCoordinateAtTime(pointingDirection, _gravity, ++i);
                UpdateLineRenderer(lineRenderer, lastPosition, nextPosition);
                _teleportationTarget.UpdateTeleportationTarget(lastPosition, nextPosition); 
                lastPosition = nextPosition;
            }
        }

        private void UpdateLineRenderer(LineRenderer lineRenderer, Vector3 startPosition, Vector3 endPosition)
        {
                lineRenderer.enabled = true;
                lineRenderer.SetPosition(0, startPosition);
                lineRenderer.SetPosition(1, endPosition);
        } 

        public void HideTeleportationArc()
        {
            foreach (var lineRenderer in _lineRenderer)
            {
                lineRenderer.enabled = false;
            }
            _teleportationTarget.Hide();
        }
    }
}