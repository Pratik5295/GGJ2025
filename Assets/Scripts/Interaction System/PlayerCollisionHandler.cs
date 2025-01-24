using GGJ.Gameplay.Interfaces;
using GGJ.Gameplay.Player;
using UnityEngine;

namespace GGJ.Gameplay.System
{
    /// <summary>
    /// Class Handles player collisions
    /// </summary>
    /// 

    [DefaultExecutionOrder(1)]
    public class PlayerCollisionHandler : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            //Check if we are in interactable trigger area
            if(other.gameObject.TryGetComponent<BaseTriggerArea>(out var triggerObj))
            {
                PlayerManager.Instance.SetCollidedTrigger(triggerObj);
            }
            else
            {
                Debug.LogWarning("Trigger area is unhandled");
            }
        }

        private void OnTriggerExit(Collider other)
        {
            //Check if we are in interactable trigger area
            if (other.gameObject.TryGetComponent<BaseTriggerArea>(out var triggerObj))
            {
                PlayerManager.Instance.ResetCollidedTrigger();
            }
        }
    }
}
