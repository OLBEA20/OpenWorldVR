using Assets.Script.Src.LineRenderer;
using Assets.Script.Src.Utilities;
using UnityEngine;

namespace Assets.Script.Src.Interaction.Teleportation
{
    public class Teleporter : ITeleporter
    {
        private readonly ITeleportationTarget _teleportationTarget;
        private readonly ILineRenderer[] _lineRenderers;

        private readonly IArc _arc;

        public Teleporter(ITeleportationTarget teleporationTarget, IArc arc, ILineRenderer[] lineRenderers)
        {
            _teleportationTarget = teleporationTarget;
            _arc = arc; 
            _lineRenderers = lineRenderers;
        }

        public void TeleportObjectWithOffset(Transform objectToTeleport, Vector3 offset)
        {
            _teleportationTarget.Teleport(objectToTeleport, offset);
        }

        public void DrawTeleportationArc(Vector3 pointingDirection, float velocity)
        {
            var time = 0;
            _teleportationTarget.Hide();
            var lastPosition = _arc.CalculateCoordinateAtTime(pointingDirection, velocity, time);
            foreach (var lineRenderer in _lineRenderers)
            {
                var nextPosition = _arc.CalculateCoordinateAtTime(pointingDirection, velocity, ++time);
                lineRenderer.ShowLine();
                lineRenderer.UpdatePositions(lastPosition, nextPosition);
                _teleportationTarget.UpdateTeleportationTarget(lastPosition, nextPosition); 
                lastPosition = nextPosition;
            }
        }

        public void HideTeleportationArc()
        {
            foreach (var lineRenderer in _lineRenderers)
            {
                lineRenderer.HideLine();
            }
            _teleportationTarget.Hide();
        }
    }
}