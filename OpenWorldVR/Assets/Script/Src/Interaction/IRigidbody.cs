using UnityEngine;

namespace Assets.Script.Src.Interaction
{
    public interface IRigidbody
    {
        void AddForce(Vector3 force);
        void AddTorque(Vector3 torque);
        Rigidbody GetRigidbody();
    }

    public class UnityRigidbody : IRigidbody
    {
        public Rigidbody Rigidbody;

        public UnityRigidbody(Rigidbody rigidbody)
        {
            Rigidbody = rigidbody;
        }

        public void AddForce(Vector3 force)
        {
            Rigidbody.AddForce(force);
        }

        public void AddTorque(Vector3 torque)
        {
            Rigidbody.AddTorque(torque);
        }

        public Rigidbody GetRigidbody()
        {
            return Rigidbody;
        }
    }
}
