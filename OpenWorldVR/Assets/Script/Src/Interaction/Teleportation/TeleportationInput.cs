using UnityEngine;

namespace Assets.Script.Src.Interaction.Teleportation
{
    public class TeleportationInput : ITeleporationInput
    {
        private bool _thumbClosed;
        private bool _gripped;
        private bool _indexClosed;
        private readonly float _velocity;

        private readonly Transform _origin;
        private readonly Transform _playerReferentiel;
        private readonly Transform _playerHead;
        private readonly ITeleporter _teleporter;

        public TeleportationInput(float velocity, ITeleporter teleporter, Transform origin, Transform playerReferentiel,
            Transform playerHead)
        {
            _velocity = velocity;
            _teleporter = teleporter;
            _origin = origin;
            _playerReferentiel = playerReferentiel;
            _playerHead = playerHead;
        }

        public void Update(float teleportionMultiplier)
        {
            if (_gripped && _thumbClosed && !_indexClosed)
            {
                _teleporter.DrawTeleportationArc(-_origin.right, _velocity * (1 + (teleportionMultiplier / 3)));
            }
            else
            {
                _teleporter.HideTeleportationArc();
            }
        }

        public void Teleport(object send, ClickedEventArgs enventArgs)
        {
            _teleporter.TeleportObjectWithOffset(_playerReferentiel, _playerHead.localPosition);
        }

        public void Grip(object sender, ClickedEventArgs eventArgs) => _gripped = true;

        public void UnGrip(object sender, ClickedEventArgs eventArgs) => _gripped = false;

        public void CloseThumb(object sender, ClickedEventArgs eventArgs) => _thumbClosed = true;

        public void OpenThumb(object sender, ClickedEventArgs eventArgs) => _thumbClosed = false;

        public void CloseIndex(object sender, ClickedEventArgs eventArgs) => _indexClosed = true;

        public void OpendIndex(object sender, ClickedEventArgs eventArgs) => _indexClosed = false;
    }
}