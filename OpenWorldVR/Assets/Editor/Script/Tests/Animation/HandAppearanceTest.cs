using Assets.Script.Src.Animation.Hand;
using NUnit.Framework;
using UnityEngine;

namespace Assets.Editor.Script.Tests.Animation
{
    public class HandAppearanceTest
    {
        private HandAppearance _handAppearance;
        private SkinnedMeshRenderer _skinnedMeshRenderer;

        [SetUp]
        public void BeforeEveryTest()
        {
            var hand = new GameObject();
            _skinnedMeshRenderer = hand.AddComponent<SkinnedMeshRenderer>();
            _handAppearance = new HandAppearance(_skinnedMeshRenderer);
        }

        [Test]
        public void WhenHidingHand_ThenMeshRendererShouldBeDisabled()
        {

            _handAppearance.HideHand();

            Assert.False(_skinnedMeshRenderer.enabled);
        }

        [Test]
        public void WhenShowingHand_ThenMeshRendererShouldBeEnabled()
        {
            _handAppearance.ShowHand();
            
            Assert.True(_skinnedMeshRenderer.enabled);
        }

    }
}