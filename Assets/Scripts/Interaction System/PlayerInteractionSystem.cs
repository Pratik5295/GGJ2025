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
            if (detectableObject == null && currentInteractableObject == null) return;

            if (currentInteractableObject != null)
            {
                //Already carrying an object, drop it
                currentInteractableObject.GetComponent<BasePickable>().Drop();

                var heldItemGO = currentInteractableObject.gameObject;
                heldItemGO.transform.SetParent(null);

                currentInteractableObject = null;

            }
            else
            {
                //Pick item
                if(detectableObject.TryGetComponent<BasePickable>(out var pickable))
                {
                    currentInteractableObject = pickable;

                    var heldItemGO = currentInteractableObject.gameObject;
                    heldItemGO.transform.SetParent(heldObjectTransform);


                    //Notify item that it is picked
                    pickable.Pick();
                }
                else
                {
                    Debug.LogWarning("This item cannot be picked, but only interacted with");
                }
            }
        }

        
    }
}
