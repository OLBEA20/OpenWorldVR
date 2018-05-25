﻿using UnityEngine;

namespace Assets.Script.Src.Interaction.Teleportation
{
    public class TeleportationInput : MonoBehaviour {
        public SteamVR_TrackedController Controller;
        public Transform Head;
        public Transform IndexTip;
        public GameObject TeleportCircle;
        public Material linesMaterial;

        private bool _gripped;
        private bool _thumbClosed;

        private Teleporter _teleporter;
        private LineRendererFactory _lineRendererFactory;
        private LineRenderer[] _lineRenderers;

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
            _lineRendererFactory = new LineRendererFactory();
            InitializeLineRenderers();
            _teleporter = new Teleporter(IndexTip, new TeleportationTarget(TeleportCircle.transform, TeleportCircle.GetComponent<MeshRenderer>(), false) , _lineRenderers);
        }
        
        private void InitializeLineRenderers()
        {
            _lineRenderers = _lineRendererFactory.CreateLineRenderers(1500, linesMaterial, 0.01f, 0.01f);
            foreach(var lineRenderer in _lineRenderers)
            {
                lineRenderer.gameObject.transform.SetParent(gameObject.transform);
            }
        }
	
        public void Update () {
            if (_gripped && _thumbClosed)
            {
                _teleporter.UpdateTeleportationArc(-IndexTip.right);
            }
            else {
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
            _teleporter.Teleport(Head);
        }
    }
}
