using Assets.Script.Src.Animation.Hand;
using UnityEngine;

namespace Assets.Script.Src
{
    public class HandInteraction : MonoBehaviour
    {
        public SteamVR_TrackedController Controller;
        public Transform Head;
        public float VelocityMultiplier = 1f;

        [SerializeField] private SkinnedMeshRenderer _skinnedMeshRenderer;

        private GameObject _objectInHand;
        private GameObject _objectColliding;

        private LayerMask _grabbable;

        private HandAppearance _handAppearance;

        private SteamVR_Controller.Device ControllerDevice => SteamVR_Controller.Input((int)Controller.controllerIndex);

        void OnEnable()
        {
            Controller.Gripped += Grab;
            Controller.Ungripped += ReleaseObject;
        }

        void OnDisable()
        {
            Controller.Gripped -= Grab;
            Controller.Ungripped -= ReleaseObject;
        }

        void Awake()
        {
            _handAppearance = new HandAppearance(_skinnedMeshRenderer);
            _grabbable = 256;
        }

        void Grab(object sender, ClickedEventArgs e)
        {
            if (!_objectInHand && _objectColliding)
            {
                _objectInHand = _objectColliding;
                _objectColliding = null;

                var joint = AddFixedJoint();
                joint.connectedBody = _objectInHand.GetComponent<Rigidbody>();

                _handAppearance.HideHand();
            }
        }

        void ReleaseObject(object sender, ClickedEventArgs e)
        {
            if (!_objectInHand) {return;}
            
            if (GetComponent<FixedJoint>())
            {
                RemoveLinkBetweenHandAndObjectInHand();
                AddVelocityToObjectInHand();

                _handAppearance.ShowHand();
            }
            _objectInHand = null;
        }

        void RemoveLinkBetweenHandAndObjectInHand()
        {
            GetComponent<FixedJoint>().connectedBody = null;
            Destroy(GetComponent<FixedJoint>());
        }

        void AddVelocityToObjectInHand() 
        {
            _objectInHand.GetComponent<Rigidbody>().velocity = VelocityMultiplier * (Head.rotation * ControllerDevice.velocity);
            _objectInHand.GetComponent<Rigidbody>().angularVelocity = VelocityMultiplier * (Head.rotation * ControllerDevice.angularVelocity);
        }

        private FixedJoint AddFixedJoint()
        {
            FixedJoint fixedJoint = gameObject.AddComponent<FixedJoint>();
            fixedJoint.breakForce = 20000;
            fixedJoint.breakTorque = 20000;
            return fixedJoint;
        }

        void OnTriggerEnter(Collider other)
        {
            if (((1 << other.gameObject.layer) & _grabbable) != 0)
            {
                ObjectColliding = other.gameObject;
            }

        }

        void OnTriggerStay(Collider other)
        {
            if (((1 << other.gameObject.layer) & _grabbable) != 0)
            {
                ObjectColliding = other.gameObject;
            }
        }

        void OnTriggerExit(Collider other)
        {
            if (((1 << other.gameObject.layer) & _grabbable) != 0)
            {
                if (other.gameObject.Equals(_objectColliding))
                {
                    ObjectColliding = null;
                }
            }
        }

        private void OnJointBreak(float breakForce)
        {
            _handAppearance.ShowHand();
        }

        public GameObject ObjectColliding
        {

            get
            {
                return _objectColliding;
            }

            set
            {
                if (value == null) {
                    _objectColliding = null;
                    return;
                }

                if (value.GetComponent<Rigidbody>())
                {
                    _objectColliding = value;
                }
            }
        }
    }
}
