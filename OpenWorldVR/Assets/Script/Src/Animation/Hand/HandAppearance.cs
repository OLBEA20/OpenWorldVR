using UnityEngine;

namespace Assets.Script.Src.Animation.Hand
{
    public class HandAppearance {
        private readonly SkinnedMeshRenderer _skinnedMeshRenderer;

        public HandAppearance(SkinnedMeshRenderer skinnedMeshRenderer)
        {
            _skinnedMeshRenderer = skinnedMeshRenderer;
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
