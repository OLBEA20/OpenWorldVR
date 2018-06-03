using UnityEngine;

namespace Assets.Script.Src.Interaction.Teleportation
{
    public interface ITeleportationTarget
    {

        void Hide();
        void UpdateTeleportationTarget(Vector3 lastPosition, Vector3 nextPosition);
        void Show();
        void Teleport(Transform objectToTeleport);
        void Teleport(Transform objectToTeleport, Vector3 offset);

    }
}
