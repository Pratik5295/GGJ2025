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

        [HideInInspector]
        public PlayerInteractionSystem InteractionSystem => interactionSystem;

        [SerializeField]
        private PlayerCollisionHandler collisionSystem;

        #endregion

        #region Player Input Handling
        [SerializeField]
        private StarterAssetsInputs input;

        public OxygenStation CurrentOxygenStation;

        private void OnInteractEventHandle(bool _interacted)
        {
            if(_interacted)
            {
                if (ScreenManager.Instance.ActiveKey == ScreenManager.ScreenKey.GAME)
                {
                    //Player has initiated interaction, check the results
                    interactionSystem.HandlePlayerInteraction();
                }
                else if(ScreenManager.Instance.ActiveKey == ScreenManager.ScreenKey.INFO)
                {
                    //Showing info text, now hide it
                    ScreenManager.Instance.ShowScreen(ScreenManager.ScreenKey.GAME);
                }
            }
            else
            {
                //Button let go
                interactionSystem.ResetInteraction();
            }
        }

        private void OnPauseEventHandler(bool _pause)
        {
            if (_pause)
            {
                ScreenManager.Instance.PauseGame();
                input.pauseKey = false;
            }
        }


        #endregion


        #region UNITY METHODS

        private void Start()
        {
            if(input != null)
            {
                input.OnInteractEvent += OnInteractEventHandle;

                input.OnPausedPressedEvent += OnPauseEventHandler;
            }
        }

        private void OnDestroy()
        {
            if (input != null)
            {
                input.OnInteractEvent -= OnInteractEventHandle;

                input.OnPausedPressedEvent -= OnPauseEventHandler;
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

        public BaseInteractable GetCurrentItem()
        {
            if (interactionSystem == null) return null;

            var currentItem = interactionSystem.CurrentInteractableObject;

            if (currentItem == null) return null;

            if (currentItem.TryGetComponent<BasePickable>(out var heldItem))
            {
                return heldItem;
            }

            return null;
        }

        public void ForceCarryItem(BasePickable _pick)
        {
            interactionSystem.ForcePlayerPick(_pick);
        }
    }
}
