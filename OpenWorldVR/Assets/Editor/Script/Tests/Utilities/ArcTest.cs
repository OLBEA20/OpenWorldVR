using Assets.Script.Src.Utilities;
using NUnit.Framework;
using UnityEngine;

namespace Assets.Editor.Script.Tests.Utilities
{
    public class ArcTest {
        private Arc _arcUnitaryStepSize;
        private Arc _arcHalfUnitStepSize;
        private Vector3 _initialTangent;
        private Transform _origin;

        [SetUp]
        public void BeforeEveryTest()
        {
            _origin = new GameObject().transform;
            _origin.position = Vector3.up;
            _initialTangent = new Vector3(1, 1, 0);
            _arcUnitaryStepSize = new Arc(1, _origin);
            _arcHalfUnitStepSize = new Arc(0.5f, _origin);
        }

        [Test]
        public void WhenRetrievingCoordinatesAtTimeZero_ThenCoordinatesShouldBeOrigin()
        {
            var coordinates = _arcUnitaryStepSize.CalculateCoordinateAtTime(Vector3.up, 1, 0);

            Assert.AreEqual(_origin.position, coordinates);
        }

        [Test]
        public void GivenInitialTangent_WhenCalculatingCoordinatesAtOneUnitTimeLater_ThenXZCoordinatesAreIncrementedAccoordingToInitialTangent()
        {
            var coordinates = _arcUnitaryStepSize.CalculateCoordinateAtTime(_initialTangent, 1, 1);

            var expectedX = _origin.position.x + _initialTangent.x;
            var expectedZ = _origin.position.z + _initialTangent.z;
            Assert.AreEqual(expectedX, coordinates.x, "X");
            Assert.AreEqual(expectedZ, coordinates.z, "Z");
        }

        [Test]
        public void GivenStepsSize_WhenCalculatingCoordinatesAtOneUnitTimeLater_ThenXZCoordinatesAreIncrementedByInitialTangentTimesStepsSize()
        {
            var coordinates = _arcHalfUnitStepSize.CalculateCoordinateAtTime(_initialTangent, 1, 1);

            var expectedX = _origin.position.x + 0.5f * _initialTangent.x;
            var expectedZ = _origin.position.z + 0.5f * _initialTangent.z;
            Assert.AreEqual(expectedX, coordinates.x, "X");
            Assert.AreEqual(expectedZ, coordinates.z, "Z");
        }

        [Test]
        public void GivenVelocity_WhenCalculatingCoordinates_ThenYCoordinateIsTimeDividedByVelocitySquared()
        {
            var velocity = 5f;

            var coordinates = _arcHalfUnitStepSize.CalculateCoordinateAtTime(_initialTangent, velocity, 2);

            var expectedY = (_origin.position.y + _initialTangent.y) - Mathf.Pow(2f / velocity, 2);
            Assert.AreEqual(expectedY, coordinates.y);
        }
    }
}
