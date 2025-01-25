using GGJ.Gameplay.Interfaces;
using GGJ.Gameplay.System;
using StarterAssets;
using UnityEngine;

namespace GGJ.Gameplay.Player
{
    /// <summary>
    /// Singleton to handle the player manager in the game.
    /// This class will hold references for other player systems in the game,
    /// for example collision detection system, Interaction system, etc
    /// </summary>

    [DefaultExecutionOrder(0)]
    public class PlayerManager : MonoBehaviour
    {
        #region Singleton Section

        public static PlayerManager Instance = null;

        private void Awake()
        {
            if(Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }
        #endregion

        #region Player System References Section

        [Header("System References")]
        [SerializeField]
        private PlayerInteractionSystem interactionSystem;

        [SerializeField]
        private PlayerCollisionHandler collisionSystem;

        #endregion

        #region Player Input Handling
        [SerializeField]
        private StarterAssetsInputs input;

        private void OnInteractEventHandle(bool _interacted)
        {
            if(_interacted)
            {
                //Player has initiated interaction, check the results
                interactionSystem.HandlePlayerInteraction();
            }
            else
            {
                interactionSystem.ResetInteraction();
            }
        }


        #endregion


        #region UNITY METHODS

        private void Start()
        {
            if(input != null)
            {
                input.OnInteractEvent += OnInteractEventHandle;
            }
        }

        private void OnDestroy()
        {
            if (input != null)
            {
                input.OnInteractEvent -= OnInteractEventHandle;
            }
        }

        #endregion

        public void SetCollidedTrigger(BaseTriggerArea _triggerArea)
        {
            interactionSystem.DetectInteractableObject(_triggerArea.Interactable);
        }

        public void ResetCollidedTrigger()
        {
            interactionSystem.DetectInteractableObject(null);
        }
    }
}
