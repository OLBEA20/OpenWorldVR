using Assets.Script.Src;
using Assets.Script.Src.Animation.Hand;
using NSubstitute;
using NUnit.Framework;
using UnityEngine;

namespace Assets.Editor.Script.Tests.Animation
{
    public class HandAnimationTest
    {
        private object _anObject;
        private ClickedEventArgs _anEventArgs;

        private IUnityAnimator _animator;
        private SkinnedMeshRenderer _skinnedMeshRenderer;
        private HandAnimation _handAnimation;

        [SetUp]
        public void BeforeEveryTest()
        {
            _animator = Substitute.For<IUnityAnimator>();
            _skinnedMeshRenderer = Substitute.For<SkinnedMeshRenderer>();

            _handAnimation = new HandAnimation(_animator, _skinnedMeshRenderer);
            _anEventArgs = new ClickedEventArgs(); 
            _anObject = new object();
        }

        [Test]
        public void WhenClosingHand_ThenHandIsClosed()
        {
            _handAnimation.CloseHand();

            _animator.Received().SetBool("close", true);
        }

        [Test]
        public void WhenOpeningHand_ThenHandIsOpened()
        {
            _handAnimation.OpenHand();

            _animator.Received().SetBool("close", false);
        }

        [Test]
        public void WhenClosingThreeFingers_ThenThreeFingersShouldBeClosed()
        {
            _handAnimation.CloseThreeFingers(_anObject, _anEventArgs);

            _animator.Received().SetBool("three_fingers_close", true);
        }

        [Test]
        public void WhenOpeningThreeFingers_ThenThreeFingersShouldBeClosed()
        {
            _handAnimation.OpenThreeFingers(_anObject, _anEventArgs);

            _animator.Received().SetBool("three_fingers_close", false);
        }

        [Test]
        public void WhenClosingIndex_ThenIndexIsClosed()
        {
            _handAnimation.CloseIndex(_anObject, _anEventArgs);

            _animator.Received().SetBool("close_index", true);
        }
        [Test]
        public void WhenOpeningIndex_ThenIndexIsOpened()
        {
            _handAnimation.OpenIndex(_anObject, _anEventArgs);

            _animator.Received().SetBool("close_index", false);
        }

        [Test]
        public void WhenClosingThumb_ThenThumbShouldBeClosed()
        {
            _handAnimation.CloseThumb(_anObject, _anEventArgs);

            _animator.Received().SetBool("close_thumb", true);
        }
        [Test]
        public void WhenOpeningThumb_ThenThumbIsOpened()
        {
            _handAnimation.OpenThumb(_anObject, _anEventArgs);

            _animator.Received().SetBool("close_thumb", false);
        }       
    }
}
