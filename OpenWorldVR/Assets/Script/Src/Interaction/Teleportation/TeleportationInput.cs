using Assets.Script.Src.LineRenderer;
using Assets.Script.Src.Physics;
using Assets.Script.Src.Utilities;
using UnityEngine;
using Valve.VR;

namespace Assets.Script.Src.Interaction.Teleportation
{
    public class TeleportationInput : MonoBehaviour
    {
        public SteamVR_TrackedController Controller;
        public SteamVR_Controller.Device Device;
        public Transform Head;
        public Transform Referentiel;
        public Transform IndexTip;
        public GameObject TeleportCircle;
        public Material LinesMaterial;

        private bool _gripped;
        private bool _thumbClosed;

        private readonly float _steps = 0.01f;
        private readonly float _velocity = 150;
        private Teleporter _teleporter;
        private LineRendererFactory _lineRendererFactory;
        private ILineRenderer[] _lineRenderers;

        public void OnEnable()
        {
            Controller.Gripped += Grip;
            Controller.Ungripped += UnGrip;

            Controller.PadTouched += CloseThumb;
            Controller.PadUntouched += OpenThumb;

            Controller.PadClicked += Teleport;
        }

        public void OnDisable()
        {
            Controller.Gripped -= Grip;
            Controller.Ungripped -= UnGrip;

            Controller.PadTouched -= CloseThumb;
            Controller.PadUntouched -= OpenThumb;

            Controller.PadClicked -= Teleport;
        }

        public void Start()
        {
            Device = SteamVR_Controller.Input((int) Controller.controllerIndex);
            _gripped = false;
            _thumbClosed = false;
            _lineRendererFactory = new LineRendererFactory();
            InitializeLineRenderers();
            _teleporter =
                new Teleporter(
                    new TeleportationTarget(TeleportCircle.transform, TeleportCircle.GetComponent<MeshRenderer>(),
                        new UnityRaycast(), false), new Arc(_steps, IndexTip), _lineRenderers);
        }

        private void InitializeLineRenderers()
        {
            _lineRenderers =
                _lineRendererFactory.CreateUnityLineRenderers(1000, gameObject.transform, LinesMaterial, 0.01f);
        }

        public void Update()
        {
            if (_gripped && _thumbClosed)
            {
                var touchedPad = Device.GetAxis(EVRButtonId.k_EButton_SteamVR_Touchpad);
                _teleporter.UpdateTeleportationArc(-IndexTip.right, _velocity * (1 + (touchedPad.y / 3)));
            }
            else
            {
                _teleporter.HideTeleportationArc();
            }
        }

        public void Grip(object sender, ClickedEventArgs eventArgs)
        {
            _gripped = true;
        }

        public void UnGrip(object sender, ClickedEventArgs eventArgs)
        {
            _gripped = false;
        }

        public void CloseThumb(object sender, ClickedEventArgs eventArgs)
        {
            _thumbClosed = true;
        }

        public void OpenThumb(object sender, ClickedEventArgs eventArgs)
        {
            _thumbClosed = false;
        }

        public void Teleport(object sender, ClickedEventArgs eventArgs)
        {
            _teleporter.Teleport(Referentiel, Head.localPosition);
        }
    }
}