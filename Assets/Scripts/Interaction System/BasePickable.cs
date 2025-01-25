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
        private Transform parent = null;

        [SerializeField]
        private Vector3 offset;

        [SerializeField]
        private PickState state;

        public PickState State => state;

        private void Start()
        {
            colliderComponent = GetComponent<Collider>();
        }

        private void Update()
        {
            if(state == PickState.HELD)
            {
                //transform.position = parent.position + offset;
            }
        }

        public void Pick(Transform _parent, Vector3 _offset)
        {
            state = PickState.HELD;
            // gameObject.SetActive(false);
            colliderComponent.enabled = false;

            parent = _parent;
            offset = _offset;
        }

        public void Drop()
        {
            state = PickState.DROP;
            // gameObject.SetActive(true);

            colliderComponent.enabled = true;
            parent = null;

        }
    }
}
