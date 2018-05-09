using UnityEngine;

namespace Assets.Script.Src.Animation.Hand
{
    public class HandAnimationInput: MonoBehaviour {

        public SteamVR_TrackedController Controller;

        private UnityAnimator _animator;
        private SkinnedMeshRenderer _skinnedMeshRenderer;
        private HandAnimation _handAnimation;

        void Awake()
        {
            _animator = new UnityAnimator(GetComponent<Animator>());
            _skinnedMeshRenderer = GetComponentInChildren<SkinnedMeshRenderer>();
            _handAnimation = new HandAnimation(_animator, _skinnedMeshRenderer);
        }

        void OnEnable()
        {
            Controller.Gripped += _handAnimation.CloseThreeFingers;
            Controller.Ungripped += _handAnimation.OpenThreeFingers;

            Controller.TriggerClicked += _handAnimation.CloseIndex;
            Controller.TriggerUnclicked += _handAnimation.OpenIndex;

            Controller.PadTouched += _handAnimation.CloseThumb;
            Controller.PadUntouched += _handAnimation.OpenThumb;
        }

        void OnDisable()
        {
            Controller.Gripped -= _handAnimation.CloseThreeFingers;
            Controller.Ungripped -= _handAnimation.OpenThreeFingers;

            Controller.TriggerClicked -= _handAnimation.CloseIndex;
            Controller.TriggerUnclicked -= _handAnimation.OpenIndex;

            Controller.PadTouched += _handAnimation.CloseThumb;
            Controller.PadUntouched += _handAnimation.OpenThumb;
        }
    }
}
