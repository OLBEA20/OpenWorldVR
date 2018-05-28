using Assets.Script.Src.Physics;
using UnityEngine;

namespace Assets.Script.Src.Interaction.Teleportation
{
    
    public class TeleportationTarget
    {
        private readonly Vector3 _offset = Vector3.zero; 
        private readonly Transform _target;
        private readonly MeshRenderer _meshRenderer;
        private readonly IRaycast _raycast;
        public bool Enabled;

        public TeleportationTarget(Transform target, MeshRenderer meshRenderer, IRaycast raycast, bool enabled)
        {
            _target = target;
            _meshRenderer = meshRenderer;
            _raycast = raycast;
            Enabled = enabled;
        }

        public void Hide()
        {
            _meshRenderer.enabled = false;
            Enabled = false;
        }

        public void UpdateTeleportationTarget(Vector3 lastPosition, Vector3 nextPosition)
        {
            if (_raycast.CheckForObstacleBetweenPoints(lastPosition, nextPosition) && !Enabled)
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
                Teleport(objectToTeleport, Vector3.zero);
            }
        }

        public void Teleport(Transform objectToTeleport, Vector3 offset) 
        {
            if (Enabled)
            {
                objectToTeleport.position = _target.position - new Vector3(offset.x, 0, offset.z);
            }
        }
    }
}