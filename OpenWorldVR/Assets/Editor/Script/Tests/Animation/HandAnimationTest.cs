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
        public void when_closing_hand_then_hand_is_closed()
        {
            _handAnimation.CloseHand();

            _animator.Received().SetBool("close", true);
        }

        [Test]
        public void when_opening_hand_then_hand_is_opened()
        {
            _handAnimation.OpenHand();

            _animator.Received().SetBool("close", false);
        }

        [Test]
        public void when_closing_three_fingers_then_three_fingers_should_be_closed()
        {
            _handAnimation.CloseThreeFingers(_anObject, _anEventArgs);

            _animator.Received().SetBool("three_fingers_close", true);
        }

        [Test]
        public void when_opening_theee_fingers_then_three_fingers_should_be_closed()
        {
            _handAnimation.OpenThreeFingers(_anObject, _anEventArgs);

            _animator.Received().SetBool("three_fingers_close", false);
        }

        [Test]
        public void when_closing_index_then_index_is_closed()
        {
            _handAnimation.CloseIndex(_anObject, _anEventArgs);

            _animator.Received().SetBool("close_index", true);
        }
        [Test]
        public void when_opening_index_then_index_is_opened()
        {
            _handAnimation.OpenIndex(_anObject, _anEventArgs);

            _animator.Received().SetBool("close_index", false);
        }

        [Test]
        public void when_closing_thumb_then_thumb_should_be_closed()
        {
            _handAnimation.CloseThumb(_anObject, _anEventArgs);

            _animator.Received().SetBool("close_thumb", true);
        }
        [Test]
        public void when_opening_thumb_then_thumb_is_opened()
        {
            _handAnimation.OpenThumb(_anObject, _anEventArgs);

            _animator.Received().SetBool("close_thumb", false);
        }       
    }
}
