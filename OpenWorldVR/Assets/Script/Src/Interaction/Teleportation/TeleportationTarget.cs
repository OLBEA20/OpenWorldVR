using UnityEngine;

namespace Assets.Script.Src.Interaction.Teleportation
{
    public class TeleportationTarget
    {
        private readonly Transform _target;
        private readonly MeshRenderer _meshRenderer;
        public bool Enabled;

        public TeleportationTarget(Transform target, MeshRenderer meshRenderer, bool enabled)
        {
            _target = target;
            _meshRenderer = meshRenderer;
            Enabled = enabled; 
        }

        public void Hide()
        {
            _meshRenderer.enabled = false;
            Enabled = false;
        }

        public void UpdateTeleportationTarget(Vector3 lastPosition, Vector3 nextPosition)
        {
            if (CheckForObstacleBetween(lastPosition, nextPosition))
            {
                Show();
                _target.position = lastPosition;
            }
        }

        public void Show()
        {
            _meshRenderer.enabled = true;
            Enabled = true;
        }

        public void Teleport(Transform objectToTeleport)
        {
            if(Enabled)
            {
                objectToTeleport.position = _target.position;
            }
        }

        private bool CheckForObstacleBetween(Vector3 startPoint, Vector3 endPoint)
        {
            var ray = new Ray(startPoint, endPoint - startPoint);
            return Physics.Raycast(ray , (endPoint - startPoint).magnitude);
        }
    }
}
