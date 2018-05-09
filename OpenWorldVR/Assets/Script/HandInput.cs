using Assets.Script.Src;
using UnityEngine;

namespace Assets.Script
{
    public class HandInput : MonoBehaviour {

        public SteamVR_TrackedController controller;

        private UnityAnimator animator;
        private SkinnedMeshRenderer skinnedMeshRenderer;
        private HandAnimation handAnimation;

        void Awake()
        {
            animator = new UnityAnimator(GetComponent<Animator>());
            skinnedMeshRenderer = GetComponentInChildren<SkinnedMeshRenderer>();
            handAnimation = new HandAnimation(animator, skinnedMeshRenderer);
        }

        void OnEnable()
        {
            controller.Gripped += handAnimation.CloseThreeFingers;
            controller.Ungripped += handAnimation.OpenThreeFingers;

            controller.TriggerClicked += handAnimation.CloseIndex;
            controller.TriggerUnclicked += handAnimation.OpenIndex;

            controller.PadTouched += handAnimation.CloseThumb;
            controller.PadUntouched += handAnimation.OpenThumb;
        }

        void OnDisable()
        {
            controller.Gripped -= handAnimation.CloseThreeFingers;
            controller.Ungripped -= handAnimation.OpenThreeFingers;

            controller.TriggerClicked -= handAnimation.CloseIndex;
            controller.TriggerUnclicked -= handAnimation.OpenIndex;

            controller.PadTouched += handAnimation.CloseThumb;
            controller.PadUntouched += handAnimation.OpenThumb;
        }
    }
}
