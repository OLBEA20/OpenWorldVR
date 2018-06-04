using Assets.Script.Src.Input;
using Assets.Script.Src.Interaction.Grab;
using NSubstitute;
using NSubstitute.Core.Arguments;
using NUnit.Framework;
using UnityEngine;

namespace Assets.Editor.Script.Tests.Interaction.Grab
{
    public class GrabbingMechanismTest
    {
        private const int GrabbableLayer = 8;
        private const int NotGrabbableLayer = 0;

        private readonly LayerMask _grabbableMask = 256;
        private readonly Transform _head = new GameObject().transform;
        private readonly FixedJoint _fixedJoint = new GameObject().AddComponent<FixedJoint>();
        private readonly GameObject _anotherGrabbableObject = new GameObject();
        private readonly GameObject _aNotGrabbableObject = new GameObject();
        private GameObject _aGrabbableObject;

        private IGrabbable _grabbable;
        private IController _controller;

        private GrabbingMechanism _grabbingMechanism;

        [SetUp]
        public void BeforeEveryTest()
        {
            InitializeGrabbableObjects();
            _controller = Substitute.For<IController>();
            _aNotGrabbableObject.layer = NotGrabbableLayer;
           _grabbingMechanism = new GrabbingMechanism(_head, _fixedJoint, _grabbableMask, _controller, 10f); 
        }

        private void InitializeGrabbableObjects()
        {
            _grabbable = Substitute.For<IGrabbable>();
            _aGrabbableObject = new GameObject();
            _aGrabbableObject.AddComponent<MonoGrabbable>().SetGrabbable(_grabbable);
            _aGrabbableObject.layer = GrabbableLayer;
            _anotherGrabbableObject.layer = GrabbableLayer;
        }

        [Test]
        public void GivenObjectGrabbable_WhenSettingObjectToGrab_ThenObjectToGrabIsSet()
        {
            _grabbingMechanism.SetObjectToGrab(_aGrabbableObject);

            Assert.AreEqual(_aGrabbableObject, _grabbingMechanism.ObjectToGrab);
        }

        [Test]
        public void GivenObjectNotGrabbable_WhenSettingObjectToGrab_ThenObjectToGrabIsNotSet()
        {
            _grabbingMechanism.SetObjectToGrab(_aNotGrabbableObject);

            Assert.AreNotEqual(_aGrabbableObject, _grabbingMechanism.ObjectToGrab);
        }

        [Test]
        public void GivenObjectNotGrabbable_WhenRemovingObjectToGrab_ThenNothingHappens()
        {
            _grabbingMechanism.SetObjectToGrab(_aGrabbableObject);
            
            _grabbingMechanism.RemoveObjectToGrab(_aNotGrabbableObject);

            Assert.AreEqual(_aGrabbableObject, _grabbingMechanism.ObjectToGrab);
        }

        [Test]
        public void GivenObjectIsNotTheObjectToGrab_WhenRemovingObjectToGrab_ThenNothingHappen()
        {
            _grabbingMechanism.SetObjectToGrab(_anotherGrabbableObject);

            _grabbingMechanism.RemoveObjectToGrab(_aGrabbableObject);

            Assert.AreEqual(_anotherGrabbableObject, _grabbingMechanism.ObjectToGrab);
        }

        [Test]
        public void GivenObjectIsObjectToGrab_WhenRemovingObjectToGrab_ThenObjectIsRemoved()
        {
            _grabbingMechanism.SetObjectToGrab(_aGrabbableObject);

            _grabbingMechanism.RemoveObjectToGrab(_aGrabbableObject);

            Assert.AreNotEqual(_aGrabbableObject, _grabbingMechanism.ObjectToGrab);
        }

        [Test]
        public void GivenNoObjectToGrab_WhenGrabbing_ThenNoObjectIsGrabbed()
        {
            _grabbingMechanism.Grab();

            Assert.IsFalse(_grabbingMechanism.IsHandFull);
        }

        [Test]
        public void GivenHandIsFull_WhenGrabbing_ThenNoObjectIsNotGrabbed()
        {
            _grabbingMechanism.SetObjectToGrab(_aGrabbableObject);
            _grabbingMechanism.Grab();

            _grabbingMechanism.Grab();

            _grabbable.Received(1).Grab(_fixedJoint);
        }

        [Test]
        public void GivenHandIsEmptyAndGrabbableObject_WhenGrabbing_ThenObjectShouldBeGrabbed()
        {
            _grabbingMechanism.SetObjectToGrab(_aGrabbableObject); 

            _grabbingMechanism.Grab();
            
            _grabbable.Received().Grab(_fixedJoint);
        }

        [Test]
        public void GivenHandIsEmptyAndGrabbableObject_WhenGrabbing_ThenHandShouldBeFull()
        {
            _grabbingMechanism.SetObjectToGrab(_aGrabbableObject);

            _grabbingMechanism.Grab();

            Assert.IsTrue(_grabbingMechanism.IsHandFull); 
        }

        [Test]
        public void WhenGrabbing_ThenObjectToGrabIsReseted()
        {
            _grabbingMechanism.SetObjectToGrab(_aGrabbableObject);

            _grabbingMechanism.Grab();

            Assert.AreNotEqual(_aGrabbableObject, _grabbingMechanism.ObjectToGrab);
        }

        [Test]
        public void GivenHandIsEmpty_WhenReleasingObject_ThenNothingHappen()
        {
            _grabbingMechanism.ReleaseObject();

            _grabbable.DidNotReceive().Throw(Arg.Any<FixedJoint>(), Arg.Any<Vector3>(), Arg.Any<Vector3>());
        }

        [Test]
        public void GivenHandIsFull_WhenReleasingObject_ThenShouldBeEmpty()
        {
            _grabbingMechanism.SetObjectToGrab(_aGrabbableObject);
            _grabbingMechanism.Grab();

            _grabbingMechanism.ReleaseObject();

            Assert.IsFalse(_grabbingMechanism.IsHandFull);
        }

        [Test]
        public void WhenReleasingObject_ThenObjectIsThrown()
        {
            _grabbingMechanism.SetObjectToGrab(_aGrabbableObject);
            _grabbingMechanism.Grab();

            _grabbingMechanism.ReleaseObject();

            _grabbable.Received().Throw(_fixedJoint, Arg.Any<Vector3>(), Arg.Any<Vector3>());
        }
    }
}
