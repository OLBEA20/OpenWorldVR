using UnityEngine;

namespace Assets.Script.Src.Input
{
    public class SteamVrController : IController
    {
        private readonly SteamVR_TrackedController _controller;

        public SteamVrController(SteamVR_TrackedController controller)
        {
            _controller = controller;
        }

        private SteamVR_Controller.Device ControllerDevice =>
            SteamVR_Controller.Input((int) _controller.controllerIndex);

        public Vector3 GetVelocity()
        {
            return ControllerDevice.velocity;
        }

        public Vector3 GetAngularVelocity()
        {
            return ControllerDevice.angularVelocity;
        }
    }
}
