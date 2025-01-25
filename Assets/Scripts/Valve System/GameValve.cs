using UnityEngine;
using static GGJ.MetaConstants.EnumManager;

namespace GGJ.Gameplay
{
    public class GameValve : BaseInteractable
    {
        [SerializeField]
        private ValveState state;

        public ValveState State => state;

        public override void Interact()
        {
            base.Interact();

            Debug.Log($"{gameObject.name} is being interacted with");
        }
    }
}
