using UnityEngine;

namespace Assets.Script.Src.Interaction
{
    public class GenericGrabbable : IGrabbable{
        public IRigidbody Rigidbody { get; set;}

        public GenericGrabbable(IRigidbody rigidbody)
        {
            Rigidbody = rigidbody;
        }

        public void Grab(FixedJoint fixedJoint)
        {
            fixedJoint.connectedBody = Rigidbody.GetRigidbody();
        }

        public void Throw(FixedJoint fixedJoint, Vector3 force, Vector3 torque)
        {
            fixedJoint.connectedBody = null;
            Rigidbody.AddForce(force);
            Rigidbody.AddTorque(torque);
        }
    }
}
