using UnityEngine;
using GGJ.Gameplay.Interfaces;

namespace GGJ.Gameplay
{
    public enum PickState
    {
        DROP = 0,
        HELD = 1
    }

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
        private PickState state;

        public PickState State => state;

        private void Start()
        {
            colliderComponent = GetComponent<Collider>();
            rb = GetComponent<Rigidbody>();
        }

        public void Pick()
        {
            state = PickState.HELD;
            colliderComponent.enabled = false;
            rb.isKinematic = true;
        }

        public void Drop()
        {
            state = PickState.DROP;

            colliderComponent.enabled = true;

            rb.isKinematic = false;

        }
    }
}
