using UnityEngine;

namespace Assets.Script.Src.Interaction.Grab
{
    public class MonoGrabbable: MonoBehaviour
    {
        private IGrabbable _grabbable;

        private void Awake()
        {
            gameObject.layer = 8;
        }
        
        // Use this for initialization
        private void Start () {
            
	        _grabbable = new GenericGrabbable(new UnityRigidbody(GetComponent<Rigidbody>()));	
        }

        public IGrabbable GetGrabbable()
        {
            return _grabbable;
        }
    }
}
