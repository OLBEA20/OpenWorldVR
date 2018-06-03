using Assets.Script.Src.Animation.Hand;
using UnityEngine;

namespace Assets.Script.Src.Interaction.Grab
{
    public class HandInteraction : MonoBehaviour
    {
        public SteamVR_TrackedController Controller;
        public Transform Head;
        public float PlayerStrenght = 150;

        private GameObject _objectColliding; 

        public SkinnedMeshRenderer SkinnedMeshRenderer;

        private IGrabbable _objectInHand;
        private bool _isHandFull;

        private FixedJoint _fixedJoint;

        private LayerMask _grabbable;

        private HandAppearance _handAppearance;

        private SteamVR_Controller.Device ControllerDevice =>
            SteamVR_Controller.Input((int) Controller.controllerIndex);

        public void Grab(object sender, ClickedEventArgs e)
        {
            if (_isHandFull || !_objectColliding) {return;}

            _objectInHand = _objectColliding.GetComponent<MonoGrabbable>().GetGrabbable();
            _objectColliding = null;
            _objectInHand.Grab(_fixedJoint);
            _handAppearance.HideHand();
            _isHandFull = true;
        }

        public void ReleaseObject(object sender, ClickedEventArgs e)
        {
            if (!_isHandFull) {return;}

            ThrowObjectInHand();
            _handAppearance.ShowHand();
            _isHandFull = false;
        }

        public void ThrowObjectInHand()
        {
            _objectInHand.Throw(_fixedJoint, Head.rotation * (PlayerStrenght * ControllerDevice.velocity), Head.rotation * ControllerDevice.angularVelocity);
        }

        public void OnEnable()
        {
            Controller.Gripped += Grab;
            Controller.Ungripped += ReleaseObject;
        }

        public void OnDisable()
        {
            Controller.Gripped -= Grab;
            Controller.Ungripped -= ReleaseObject;
        }

        public void Awake()
        {
            InitializeFixedJoint();
            _handAppearance = new HandAppearance(SkinnedMeshRenderer);
            _grabbable = 256;
            _isHandFull = false;
        }

        public void OnTriggerEnter(Collider other)
        {
            if (((1 << other.gameObject.layer) & _grabbable) != 0)
            {
                _objectColliding = other.gameObject;
            }
        }

        public void OnTriggerStay(Collider other)
        {
            if (((1 << other.gameObject.layer) & _grabbable) != 0)
            {
                _objectColliding = other.gameObject;
            }
        }

        public void OnTriggerExit(Collider other)
        {
            if (((1 << other.gameObject.layer) & _grabbable) == 0) {return;}

            if (other.gameObject.Equals(_objectColliding))
            {
                _objectColliding = null;
            }
        }

        private void OnJointBreak(float breakForce)
        {
            _handAppearance.ShowHand();
            InitializeFixedJoint();
        }

        private void InitializeFixedJoint()
        {
            _fixedJoint = gameObject.AddComponent<FixedJoint>();
            _fixedJoint.breakForce = 20000;
            _fixedJoint.breakTorque = 20000;
        }
    }
}