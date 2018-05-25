using Assets.Script.Src.Interaction.Teleportation;
using UnityEngine;
using NUnit.Framework;

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

        [SetUp]
        public void BeforeEveryTest()
        {
            _teleportPosition = Vector3.back;
            _transformToTeleport = new GameObject().transform;
            _transformToTeleport.position = _originPosition;

            var bufferGameObject = new GameObject();

            _transform = bufferGameObject.transform;
            _teleportPosition = _transform.position;
            _meshRenderer = bufferGameObject.AddComponent<MeshRenderer>();
            _teleportationTarget = new TeleportationTarget(_transform, _meshRenderer, false);
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
        public void GivenTeleportationTargetIsDisabled_WhenTeleporatingObject_ThenObjectShouldNotBeTeleported()
        {
            _teleportationTarget.Show();

            _teleportationTarget.Teleport(_transformToTeleport);

            Assert.AreEqual(_originPosition, _transformToTeleport.position);
        }
    }
}
