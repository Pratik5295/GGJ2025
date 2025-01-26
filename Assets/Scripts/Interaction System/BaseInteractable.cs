using GGJ.Gameplay.Interfaces;
using UnityEngine;

namespace GGJ.Gameplay
{
    /// <summary>
    /// Base class for interactable object implementing Interactable interface
    /// </summary>

    public class BaseInteractable : MonoBehaviour, IInteractable
    {
        [SerializeField]
        private string MessageOnInteract;

        /// <summary>
        /// Allow player to interact with the object
        /// </summary>
        public virtual void Interact()
        {
           ScreenManager.Instance.PopulateInfoText(MessageOnInteract);
           ScreenManager.Instance.ShowScreen(ScreenManager.ScreenKey.INFO);    
        }


        /// <summary>
        /// Reset the interact on this object
        /// </summary>
        public virtual void ResetInteract()
        {

        }
    }
}
