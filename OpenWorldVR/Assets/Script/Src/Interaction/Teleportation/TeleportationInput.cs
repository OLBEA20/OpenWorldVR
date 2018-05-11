using UnityEngine;

namespace Assets.Script.Src.Interaction.Teleportation
{
    public class TeleportationInput : MonoBehaviour {
        public SteamVR_TrackedController Controller;
        public Transform Head;
        public Transform IndexTip;

        private bool _gripped;
        private bool _thumbClosed;

        private Teleporter _teleporter;
        private LineRenderer _lineRenderer;

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
            _gripped = false;
            _thumbClosed = false;
            _lineRenderer = gameObject.AddComponent<LineRenderer>();
            _teleporter = new Teleporter(IndexTip, _lineRenderer);
        }
	
        public void Update () {
            if (_gripped && _thumbClosed)
            {
                _teleporter.UpdateTeleportationLine(-IndexTip.right, 100);
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
            _teleporter.Teleport(Head, IndexTip.forward, 100);
        }
    }
}
