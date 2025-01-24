using UnityEngine;

namespace GGJ.Gameplay.Interfaces
{
    /// <summary>
    /// Base extending interface of the ITriggerable class
    /// </summary>
    public class BaseTriggerArea : MonoBehaviour, ITriggerable
    {
        [SerializeField]
        protected BaseInteractable interactable;

        public BaseInteractable Interactable => interactable;
    }
}
