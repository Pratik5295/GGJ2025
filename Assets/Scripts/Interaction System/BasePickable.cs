using UnityEngine;
using GGJ.Gameplay.Interfaces;
using static GGJ.MetaConstants.EnumManager;

namespace GGJ.Gameplay
{
    /// <summary>
    /// Base pickable extends base interactable and pickable
    /// </summary>
    public class BasePickable : BaseInteractable,IPickable
    {
        [SerializeField]
        private Collider colliderComponent;

        [SerializeField]
        private Rigidbody rb;


        [SerializeField]
        private PickState pickState;

        public PickState PickState => pickState;

        [SerializeField]
        protected CarryType Type;

        private void Start()
        {
            colliderComponent = GetComponent<Collider>();
            rb = GetComponent<Rigidbody>();
        }

        public void Pick()
        {
            pickState = PickState.HELD;
            colliderComponent.enabled = false;
            rb.isKinematic = true;
        }

        public void Drop()
        {
            pickState = PickState.DROP;

            colliderComponent.enabled = true;

            rb.isKinematic = false;

        }
    }
}
