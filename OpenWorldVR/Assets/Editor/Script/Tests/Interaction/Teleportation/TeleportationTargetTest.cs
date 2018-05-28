using Assets.Script.Src.Interaction.Teleportation;
using Assets.Script.Src.Physics;
using UnityEngine;
using NUnit.Framework;
using NSubstitute;

namespace Assets.Editor.Script.Tests.Interaction.Teleportation
{
    public class TeleportationTargetTest : MonoBehaviour
    {
        private TeleportationTarget _teleportationTarget;
        private Vector3 _teleportPosition;
        private Vector3 _originPosition;
        private Transform _transformToTeleport;
        private Transform _transform;
        private MeshRenderer _meshRenderer;
        private IRaycast _raycast;

        [SetUp]
        public void BeforeEveryTest()
        {
            _raycast = new TestableRaycast(true);

            _teleportPosition = Vector3.back;
            _transformToTeleport = new GameObject().transform;
            _originPosition = Vector3.back;
            _transformToTeleport.position = _originPosition;

            var bufferGameObject = new GameObject();

            _transform = bufferGameObject.transform;
            _teleportPosition = _transform.position;
            _meshRenderer = bufferGameObject.AddComponent<MeshRenderer>();
            _teleportationTarget = new TeleportationTarget(_transform, _meshRenderer, _raycast, false);
        }

        [Test]
        public void WhenHidingTeleportationTarget_ThenTeleportationTargetShouldNotBeEnabled()
        {
            _teleportationTarget.Hide();

            Assert.IsFalse(_teleportationTarget.Enabled);
        }

        [Test]
        public void WhenHidingTeleportationTarget_ThenTeleportationTargetShouldNotBeVisible()
        {
            _teleportationTarget.Hide();

            Assert.IsFalse(_meshRenderer.enabled);
        }

        [Test]
        public void WhenShowingTeleportationTarget_ThenTeloportationTargetShouldBeEnabled()
        {
            _teleportationTarget.Show();

            Assert.IsTrue(_teleportationTarget.Enabled);
        }

        [Test]
        public void WhenShowingTeleportationTarget_ThenTeleporationTargetShouldBeVisible()
        {
            _teleportationTarget.Show();

            Assert.IsTrue(_meshRenderer.enabled);
        }

        [Test]
        public void GivenTeleportationTargetIsEnabled_WhenTeleportatingObject_ThenObjectShouldBeTeleportedAtTheRightPosition()
        {
            _teleportationTarget.Show();

            _teleportationTarget.Teleport(_transformToTeleport);

            Assert.AreEqual(_teleportPosition, _transformToTeleport.position);
        }

        [Test]
        public void GivenOffset_WhenTeleportingObject_ThenXZComposantOfOffsetShouldBeConsidered()
        {
            _teleportationTarget.Enabled = true;
            var offset = new Vector3(1, 1, 1);

            _teleportationTarget.Teleport(_transformToTeleport, offset);

            var expectedPosition = _teleportPosition - new Vector3(offset.x, 0, offset.z);
            Assert.AreEqual(expectedPosition, _transformToTeleport.position);
        }

        [Test]
        public void GivenTeleportationTargetIsDisabled_WhenTeleporatingObject_ThenObjectShouldNotBeTeleported()
        {
            _teleportationTarget.Hide();

            _teleportationTarget.Teleport(_transformToTeleport);

            Assert.AreEqual(_originPosition, _transformToTeleport.position);
        }

        [Test]
        public void GivenObstacleBetweenTwoPoints_WhenUpdatingTeleportationTarget_ThenTargetIsVisible()
        {
            _teleportationTarget.UpdateTeleportationTarget(Vector3.back, Vector3.down);

            Assert.IsTrue(_teleportationTarget.Enabled);
        }

        [Test]
        public void GivenObstacleBetweenTwoPoints_WhenUpdatingTeleportationTarget_ThenTeleportationTargetPositionIsUpdated()
        {
            var position = Vector3.back;
            
            _teleportationTarget.UpdateTeleportationTarget(position, Vector3.down);

            Assert.AreEqual(position, _transform.position);
        }

        [Test]
        public void
            GivenTeleportationTargetAlreadyEnabled_WhenUpdatingTeleportationTarget_ThenTeleportationTargetPositionIsNotUpdated()
        {
            _teleportationTarget.Show();
            var position = Vector3.back;

            _teleportationTarget.UpdateTeleportationTarget(position, Vector3.down);

            Assert.AreNotEqual(position, _transform.position);
        }

        public class TestableRaycast : IRaycast
        {
            private readonly bool _checkForObstacleBetweenPointsReturnValue;

            public TestableRaycast (bool checkForObstacleBetweenPointsReturnValue)
            {
                _checkForObstacleBetweenPointsReturnValue = checkForObstacleBetweenPointsReturnValue;
            }

            public bool CheckForObstacleBetweenPoints(Vector3 startPoint, Vector3 endPoint)
            {
                return _checkForObstacleBetweenPointsReturnValue;
            }
        }
    }

}