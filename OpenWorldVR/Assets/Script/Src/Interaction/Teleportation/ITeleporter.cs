using UnityEngine;

namespace Assets.Script.Src.Interaction.Teleportation
{
    public interface ITeleporter
    {
        void TeleportObjectWithOffset(Transform objectToTeleport, Vector3 offset);
        void DrawTeleportationArc(Vector3 pointingDirection, float velocity);
        void HideTeleportationArc();
    }
}