using Assets.Script.Src.Interaction.Teleportation;
using NSubstitute;
using NUnit.Framework;
using UnityEngine;

namespace Assets.Editor.Script.Tests.Interaction.Teleportation
{
    public class TeleportationInputTest
    {
        private const float AnyVelocity = 100f;
        private readonly Transform _origin = new GameObject().transform;
        private readonly Transform _referential = new GameObject().transform; 
        private readonly Transform _head = new GameObject().transform;

        private readonly ITeleporter _teleporter = Substitute.For<ITeleporter>();

        private TeleportationInput _teleportationInput;

        [SetUp]
        public void BeforeEveryTest()
        {
            _teleportationInput = new TeleportationInput(AnyVelocity, _teleporter, _origin, _referential, _head);
        }

        [Test]
        public void GivenThumbIsClosedIndexIsOpenedAndHandIsGripped_WhenUpdating_ThenTeleportationArcIsDrawned()
        {
            _teleportationInput.CloseThumb(new object(), new ClickedEventArgs());
            _teleportationInput.OpendIndex(new object(), new ClickedEventArgs());
            _teleportationInput.Grip(new object(), new ClickedEventArgs());

            _teleportationInput.Update(0f);

            _teleporter.Received().DrawTeleportationArc(-_origin.right, AnyVelocity);
        }

        [Test]
        public void GivenThumbIsOpened_WhenUpdating_ThenTeleportationArcIsHiden()
        {
            _teleportationInput.OpenThumb(new object(), new ClickedEventArgs());
            _teleportationInput.OpendIndex(new object(), new ClickedEventArgs());
            _teleportationInput.Grip(new object(), new ClickedEventArgs());

            _teleportationInput.Update(0f);
            
            _teleporter.Received().HideTeleportationArc();
        }

        [Test]
        public void GivenIndexIsClosed_WhenUpdating_ThenTeleportationArcIsHiden()
        {
            _teleportationInput.CloseThumb(new object(), new ClickedEventArgs());
            _teleportationInput.CloseIndex(new object(), new ClickedEventArgs());
            _teleportationInput.Grip(new object(), new ClickedEventArgs());

            _teleportationInput.Update(0f);
            
            _teleporter.Received().HideTeleportationArc();
        }

        [Test]
        public void GivenHandIsUngripped_WhenUpdating_ThenTeleportationArcIsHiden()
        {
            _teleportationInput.CloseThumb(new object(), new ClickedEventArgs());
            _teleportationInput.OpendIndex(new object(), new ClickedEventArgs());
            _teleportationInput.UnGrip(new object(), new ClickedEventArgs());

            _teleportationInput.Update(0f);
            
            _teleporter.Received().HideTeleportationArc();
        }

        [Test]
        public void WhenTeleporting_ThenPlayerReferentielIsTeleportedWithHeadLocalPositionAsOffset()
        {
            _teleportationInput.Teleport(new object(), new ClickedEventArgs());

            _teleporter.Received().TeleportObjectWithOffset(_referential, _head.localPosition);
        }
    }
}