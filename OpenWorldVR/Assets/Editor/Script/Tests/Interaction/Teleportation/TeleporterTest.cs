using Assets.Script.Src.Interaction.Teleportation;
using Assets.Script.Src.LineRenderer;
using Assets.Script.Src.Physics;
using Assets.Script.Src.Utilities;
using NSubstitute;
using NUnit.Framework;
using UnityEngine;

namespace Assets.Editor.Script.Tests.Interaction.Teleportation
{
    public class TeleporterTest
    {
        private const float AnyVelocity = 100f;

        private Teleporter _teleporter;
        private readonly Vector3 _position1 = Vector3.down;
        private readonly Vector3 _position2 = Vector3.back;
        private readonly Vector3 _anyPointingDirection = new Vector3();
        private readonly ITeleportationTarget _teleportationTarget = Substitute.For<ITeleportationTarget>(); 
        private readonly ILineRenderer _lineRenderer = Substitute.For<ILineRenderer>();
        private readonly ILineRenderer[] _lineRenderers = new ILineRenderer[1];
        private IArc _arc;

        [SetUp]
        public void BeforeEveryTest()
        {
            InitializeArc();
            _teleporter = new Teleporter(_teleportationTarget, _arc, _lineRenderers);
        }

        private void InitializeArc()
        {
            _lineRenderers[0] = _lineRenderer;
            _arc = new TestableArc(new[]{_position1, _position2}); 
        }

        [Test]
        public void WhenHidingTeleportationArc_ThenArcShouldNotBeVisible()
        {
            _teleporter.HideTeleportationArc();

            _lineRenderer.Received().HideLine();
        }

        [Test]
        public void WhenHidingTeleportationArc_ThenTeleportationTargetShouldBeHidden()
        {
            _teleporter.HideTeleportationArc();

            _teleportationTarget.Received().Hide();
        }

        [Test]
        public void WhenTeleportingOjbectWithOffset_ThenObjectShouldBeTeleportedWithOffset()
        {
            var objectToTeleport = new GameObject().transform;
            var offset = new Vector3();

            _teleporter.TeleportObjectWithOffset(objectToTeleport, offset);

            _teleportationTarget.Received().Teleport(objectToTeleport, offset);
        }

        [Test]
        public void WhenUpdatingTeleportationArc_ThenTeleportationTargetIsFistHiden()
        {
            _teleporter.DrawTeleportationArc(_anyPointingDirection, AnyVelocity);

            _teleportationTarget.Received().Hide();
        }

        [Test]
        public void WhenUpdatingTeleportationArc_ThenLineRenderersPositionsAreUpdated()
        {
            _teleporter.DrawTeleportationArc(_anyPointingDirection, AnyVelocity);

            _lineRenderer.Received().UpdatePositions(_position1, _position2);
        }

        [Test]
        public void WhenUpdatingTeleportationArc_ThenLineRenderersAreVisible()
        {
            _teleporter.DrawTeleportationArc(_anyPointingDirection, AnyVelocity);

            _lineRenderer.Received().ShowLine();
        }

        [Test]
        public void WhenUpdatingTeleportationArc_ThenTeleportionTargetIsUpdated()
        {
           _teleporter.DrawTeleportationArc(_anyPointingDirection, AnyVelocity); 

            _teleportationTarget.Received().UpdateTeleportationTarget(_position1, _position2);
        }

        private class TestableArc : IArc
        {
            private readonly Vector3[] _calculateCoordinateAtTimeReturnValues;
            private int _calculateCoordinateAtTimeReturnValuesIndex = 0;

            public TestableArc(Vector3[] calculateCoordinateAtTimeReturnValues)
            {
                _calculateCoordinateAtTimeReturnValues = calculateCoordinateAtTimeReturnValues;
            }

            public Vector3 CalculateCoordinateAtTime(Vector3 initialTangent, float velocity, float time)
            {
                
                return _calculateCoordinateAtTimeReturnValues[
                    _calculateCoordinateAtTimeReturnValuesIndex++ % _calculateCoordinateAtTimeReturnValues.Length];
            }
        }
    }
}