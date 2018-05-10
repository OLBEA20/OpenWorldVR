using UnityEngine;

namespace Assets.Script.Src.Animation
{
    public interface IUnityAnimator
    {

        void SetBool(string parameter, bool value);
    }

    public class UnityAnimator : IUnityAnimator
    {
        private readonly Animator _animator;

        public UnityAnimator(Animator animator)
        {
            this._animator = animator;
        }

        public void SetBool(string parameter, bool value)
        {
           _animator.SetBool(parameter, value); 
        }
    }
}
