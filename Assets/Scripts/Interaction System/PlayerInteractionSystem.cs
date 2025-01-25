using GGJ.Gameplay.Interfaces;
using UnityEngine;

namespace GGJ.Gameplay.System
{
    /// <summary>
    /// Interaction System player handler.
    /// This class will have reference to current interactable object detected
    /// </summary>
    /// 

    [DefaultExecutionOrder(1)]
    public class PlayerInteractionSystem : MonoBehaviour
    {
        [SerializeField]
        private BaseInteractable detectableObject;

        //Held Object
        [SerializeField]
        private BaseInteractable currentInteractableObject;

        [SerializeField]
        private Transform heldObjectTransform;

        public bool HasDetectableObject => detectableObject != null;

        public void DetectInteractableObject(BaseInteractable interactableObject)
        {
            detectableObject = interactableObject;
        }

        public void HandlePlayerInteraction()
        {
            if (detectableObject == null) return;

            if (currentInteractableObject != null)
            {
                //Already carrying an object, drop it
                currentInteractableObject.GetComponent<BasePickable>().Drop();
                currentInteractableObject = null;

            }
            else
            {
                //Pick item
                if(detectableObject.TryGetComponent<BasePickable>(out var pickable))
                {
                    currentInteractableObject = pickable;
                    pickable.Pick();
                }
            }
        }

        
    }
}
