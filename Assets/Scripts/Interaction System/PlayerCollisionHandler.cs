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
                if (triggerObj.TryGetComponent<OxygenStation>(out var oxygenStation))
                {
                    PlayerManager.Instance.CurrentOxygenStation = oxygenStation;
                }
                else
                {

                    PlayerManager.Instance.SetCollidedTrigger(triggerObj);
                }

                if(PlayerManager.Instance.InteractionSystem.CurrentInteractableObject == null)
                {
                    ScreenManager.Instance.ShowInstructionText();
                }
            }
            else
            {
                Debug.LogWarning("Trigger area is unhandled");
            }
        }

        private void OnTriggerStay(Collider other)
        {
            //Check if we are in interactable trigger area
            if (other.gameObject.TryGetComponent<BaseTriggerArea>(out var triggerObj))
            {
                if (triggerObj.TryGetComponent<OxygenStation>(out var oxygenStation))
                {
                    PlayerManager.Instance.CurrentOxygenStation = oxygenStation;
                }

                if (PlayerManager.Instance.InteractionSystem.CurrentInteractableObject == null)
                {
                    ScreenManager.Instance.ShowInstructionText();
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            //Check if we are in interactable trigger area
            if (other.gameObject.TryGetComponent<BaseTriggerArea>(out var triggerObj))
            {
                if (triggerObj.TryGetComponent<OxygenStation>(out var oxygenStation))
                {
                    PlayerManager.Instance.CurrentOxygenStation = null;
                }

                PlayerManager.Instance.ResetCollidedTrigger();
                ScreenManager.Instance.HideInstructionText();

            }
        }
    }
}
