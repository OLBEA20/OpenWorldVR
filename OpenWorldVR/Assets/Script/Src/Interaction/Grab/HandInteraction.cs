using Assets.Script.Src.Animation.Hand;
using Assets.Script.Src.Input;
using UnityEngine;

namespace Assets.Script.Src.Interaction.Grab
{
    public class HandInteraction : MonoBehaviour
    {
        public SteamVR_TrackedController Controller;
        public Transform Head;
        public float PlayerStrength = 150;

        public SkinnedMeshRenderer SkinnedMeshRenderer;

        private FixedJoint _fixedJoint;

        private HandAppearance _handAppearance;
        private GrabbingMechanism _grabbingMechanism;

        public void Grab(object sender, ClickedEventArgs e)
        {
            _grabbingMechanism.Grab();
            if (_grabbingMechanism.IsHandFull)
            {
                _handAppearance.HideHand();
            }
        }

        public void ReleaseObject(object sender, ClickedEventArgs e)
        {
            _grabbingMechanism.ReleaseObject();
            _handAppearance.ShowHand();
        }

        public void Start()
        {
            InitializeFixedJoint();
            _handAppearance = new HandAppearance(SkinnedMeshRenderer);
            _grabbingMechanism =
                new GrabbingMechanism(Head, _fixedJoint, 256, new SteamVrController(Controller), PlayerStrength);
        }

        public void OnTriggerEnter(Collider other)
        {
            _grabbingMechanism.SetObjectToGrab(other.gameObject);
        }

        public void OnTriggerStay(Collider other)
        {
            _grabbingMechanism.SetObjectToGrab(other.gameObject);
        }

        public void OnTriggerExit(Collider other)
        {
            _grabbingMechanism.RemoveObjectToGrab(other.gameObject);
        }

        private void OnJointBreak(float breakForce)
        {
            _handAppearance.ShowHand();
            InitializeFixedJoint();
            _grabbingMechanism.FixedJoint = _fixedJoint;
        }

        private void InitializeFixedJoint()
        {
            _fixedJoint = gameObject.AddComponent<FixedJoint>();
            _fixedJoint.breakForce = 20000;
            _fixedJoint.breakTorque = 20000;
        }

        private void OnEnable()
        {
            Controller.Gripped += Grab;
            Controller.Ungripped += ReleaseObject;
        }

        private void OnDisable()
        {
            Controller.Gripped -= Grab;
            Controller.Ungripped -= ReleaseObject;
        }
    }
}