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
        private BaseInteractable currentInteractableObject;

        public bool HasInteractableObject => currentInteractableObject != null;

        public void DetectInteractableObject(BaseInteractable interactableObject)
        {
            currentInteractableObject = interactableObject;
        }
    }
}
