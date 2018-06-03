using Assets.Script.Src.LineRenderer;
using Assets.Script.Src.Physics;
using Assets.Script.Src.Utilities;
using UnityEngine;
using Valve.VR;

namespace Assets.Script.Src.Interaction.Teleportation
{
    public class TeleportationInputConfiguration : MonoBehaviour
    {
        public SteamVR_TrackedController Controller;
        public SteamVR_Controller.Device Device;
        public Transform Head;
        public Transform Referentiel;
        public Transform IndexTip;
        public GameObject TeleportCircle;
        public Material LinesMaterial;

        public float Steps = 0.01f;
        public float Velocity = 150;

        private bool _gripped;
        private bool _thumbClosed;
        private bool _indexClosed = false;

        private ITeleporationInput _teleporationInput;
        private ITeleporter _teleporter;
        private LineRendererFactory _lineRendererFactory;
        private ILineRenderer[] _lineRenderers;

        public void Start()
        {
            Device = SteamVR_Controller.Input((int) Controller.controllerIndex);
        }

        public void Update()
        {
            var touchedPad = Device.GetAxis();
            _teleporationInput.Update(touchedPad.y);
        }

        public void OnEnable()
        {
            Initialization();
            LinkControllerEvent();
        }

        public void OnDisable()
        {
            UnLinkControllerEvent(); 
        }

        private void Initialization()
        {
            _lineRendererFactory = new LineRendererFactory();
            _lineRenderers =
                _lineRendererFactory.CreateUnityLineRenderers(1000, gameObject.transform, LinesMaterial, 0.01f);
            _teleporter =
                new Teleporter(
                    new TeleportationTarget(TeleportCircle.transform, TeleportCircle.GetComponent<MeshRenderer>(),
                        new UnityRaycast(), false), new Arc(Steps, IndexTip), _lineRenderers);
            _teleporationInput = new TeleportationInput(Velocity, _teleporter, IndexTip, Referentiel, Head);
        }

        private void LinkControllerEvent()
        {
            Controller.Gripped += _teleporationInput.Grip;
            Controller.Ungripped += _teleporationInput.UnGrip;

            Controller.PadTouched += _teleporationInput.CloseThumb;
            Controller.PadUntouched += _teleporationInput.OpenThumb;

            Controller.TriggerClicked += _teleporationInput.CloseIndex;
            Controller.TriggerUnclicked += _teleporationInput.OpendIndex;

            Controller.PadClicked += _teleporationInput.Teleport;
        }

        private void UnLinkControllerEvent()
        {
            Controller.Gripped -= _teleporationInput.Grip;
            Controller.Ungripped -= _teleporationInput.UnGrip;

            Controller.PadTouched -= _teleporationInput.CloseThumb;
            Controller.PadUntouched -= _teleporationInput.OpenThumb;

            Controller.TriggerClicked -= _teleporationInput.CloseIndex;
            Controller.TriggerUnclicked -= _teleporationInput.OpendIndex;

            Controller.PadClicked -= _teleporationInput.Teleport;
        }
    }
}