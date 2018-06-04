using Assets.Script.Src.Interaction;
using Assets.Script.Src.Interaction.Grab;
using NUnit.Framework;
using NSubstitute;
using UnityEngine;

namespace Assets.Editor.Script.Tests.Interaction
{
    public class GenericGrabbableTest
    {
        private GameObject _hand;
        private FixedJoint _fixedJoint;
        private IRigidbody _unityRigidbody;

        [SetUp]
        public void BeforeEveryTest()
        {
            _hand = new GameObject();
            _fixedJoint = _hand.AddComponent<FixedJoint>();
            _unityRigidbody = Substitute.For<IRigidbody>();
        }

        [Test]
        public void WhenGrabbingObject_ThenObjectShouldBeConnected()
        {
            var genericGrabbable = new GenericGrabbable(new UnityRigidbody(new Rigidbody()));

            genericGrabbable.Grab(_fixedJoint);
            
            Assert.AreEqual(genericGrabbable.Rigidbody.GetRigidbody(), _fixedJoint.connectedBody);
        }

        [Test]
        public void WhenThrowingObject_ThenObjectShouldBeDisconnected()
        {
            var genericGrabbable = new GenericGrabbable(_unityRigidbody);

            genericGrabbable.Throw(_fixedJoint, new Vector3(), new Vector3());

            Assert.IsNull(_fixedJoint.connectedBody);
        }

        [Test]
        public void WhenThrowingObject_ThenForceIsAppliedToObject()
        {
            var genericGrabbable = new GenericGrabbable(_unityRigidbody);
            var force = new Vector3();

            genericGrabbable.Throw(_fixedJoint, force, new Vector3());
            
            _unityRigidbody.Received().AddForce(force);
        }

        [Test]
        public void WhenThrowingObject_ThenTorqueIsAppliedToObject()
        {
            var genericGrabbable = new GenericGrabbable(_unityRigidbody);
            var torque = new Vector3();

            genericGrabbable.Throw(_fixedJoint, new Vector3(), torque);

            _unityRigidbody.Received().AddTorque(torque);
        }
    }
}
