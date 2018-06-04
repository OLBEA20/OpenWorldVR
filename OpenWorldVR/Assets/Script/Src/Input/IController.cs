using UnityEngine;

namespace Assets.Script.Src.Input
{
    public interface IController
    {
        Vector3 GetVelocity();
        Vector3 GetAngularVelocity();
    }
}