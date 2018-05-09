using UnityEngine;

namespace Assets.Script.Src.Animation.Hand
{
    public class HandAnimation {

        private readonly IUnityAnimator _animator;
        private readonly SkinnedMeshRenderer _skinnedMeshRenderer;

        public HandAnimation(IUnityAnimator animator, SkinnedMeshRenderer skinnedMeshRenderer) {
            this._animator = animator;
            this._skinnedMeshRenderer = skinnedMeshRenderer;
        }

        public void CloseHand()
        {
            _animator.SetBool("close", true);
        }

        public void OpenHand()
        {
            _animator.SetBool("close", false);
        }

        public void CloseThreeFingers(object sender, ClickedEventArgs e)
        {
            _animator.SetBool("three_fingers_close", true);
        }

        public void OpenThreeFingers(object sender, ClickedEventArgs e)
        {
            _animator.SetBool("three_fingers_close", false);
        }

        public void CloseIndex(object sender, ClickedEventArgs e)
        {
            _animator.SetBool("close_index", true);
        }

        public void OpenIndex(object sender, ClickedEventArgs e)
        {
            _animator.SetBool("close_index", false);
        }

        public void CloseThumb(object sender, ClickedEventArgs e)
        {
            _animator.SetBool("close_thumb", true);
        }

        public void OpenThumb(object sender, ClickedEventArgs e)
        {
            _animator.SetBool("close_thumb", false);
        }

        public void HideHand()
        {
            _skinnedMeshRenderer.enabled = false;
        }

        public void ShowHand()
        {
            _skinnedMeshRenderer.enabled = true;
        }
    }
}
