using Assets.Script.Src.Input;
using UnityEngine;

namespace Assets.Script.Src.Interaction.Grab
{
    public class GrabbingMechanism
    {
        private readonly float _playerStrength;

        private readonly Transform _head;
        private readonly LayerMask _grabbableMask;
        private readonly IController _controller; 

        private IGrabbable _objectInHand;

        public GameObject ObjectToGrab { get; private set; }

        public FixedJoint FixedJoint { private get; set; }

        public bool IsHandFull { get; private set; }

        public GrabbingMechanism(Transform head, FixedJoint fixedJoint, LayerMask grabbableMask, IController controller, float playerStrength)
        {
            _head = head;
            FixedJoint = fixedJoint;
            _grabbableMask = grabbableMask;
            _controller = controller;
            _playerStrength = playerStrength;
        }

        public void SetObjectToGrab(GameObject objectColliding)
        {
            if (((1 << objectColliding.layer) & _grabbableMask) != 0)
            {
                ObjectToGrab = objectColliding;
            }
        }

        public void RemoveObjectToGrab(GameObject objectUnColliding)
        {
            if (((1 << objectUnColliding.layer) & _grabbableMask) == 0) {return;}

            if (objectUnColliding.Equals(ObjectToGrab))
            {
                ObjectToGrab = null;
            }
        }

        public void Grab()
        {
            if (IsHandFull || !ObjectToGrab) {return;}

            _objectInHand = ObjectToGrab.GetComponent<MonoGrabbable>().GetGrabbable();
            _objectInHand.Grab(FixedJoint);
            ObjectToGrab = null;
            IsHandFull = true;
        }

        public void ReleaseObject()
        {
            if (!IsHandFull) {return;}

            _objectInHand.Throw(FixedJoint, _head.rotation * (_playerStrength * _controller.GetVelocity()), _head.rotation * _controller.GetAngularVelocity());
            IsHandFull = false;
        }
    }
}
