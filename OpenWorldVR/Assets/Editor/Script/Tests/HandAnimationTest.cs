using Assets.Script.Src;
using NSubstitute;
using NUnit.Framework;
using UnityEngine;

namespace Assets.Editor.Script.Tests
{
    public class HandAnimationTest
    {

        [Test]
        public void when_closing_hand_then_hand_is_closed()
        {
            var animator = Substitute.For<IUnityAnimator>();
            var skinnedMeshRenderer = Substitute.For<SkinnedMeshRenderer>();
            var handAnimation = new HandAnimation(animator, skinnedMeshRenderer);

            handAnimation.CloseHand();

            animator.Received().SetBool("close", true);
        }
    }
}
