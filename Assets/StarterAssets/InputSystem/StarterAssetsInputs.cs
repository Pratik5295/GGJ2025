using NUnit.Framework.Interfaces;
using System;
using UnityEngine;
#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

namespace StarterAssets
{
	public class StarterAssetsInputs : MonoBehaviour
	{
		[Header("Character Input Values")]
		public Vector2 move;
		public Vector2 look;
		public bool jump;
		public bool sprint;
		public bool interact;
		public bool next;   //Button use for UI interactions? Currently using interact
		public bool pauseKey;

		[Header("Movement Settings")]
		public bool analogMovement;

		[Header("Mouse Cursor Settings")]
		public bool cursorLocked = true;
		public bool cursorInputForLook = true;

		public Action<bool> OnInteractEvent;
		public Action<bool> OnNextEvent;
		public Action<bool> OnPausedPressedEvent;

#if ENABLE_INPUT_SYSTEM
		public void OnMove(InputValue value)
		{
			MoveInput(value.Get<Vector2>());
		}

		public void OnLook(InputValue value)
		{
			if(cursorInputForLook)
			{
				LookInput(value.Get<Vector2>());
			}
		}

		public void OnJump(InputValue value)
		{
			JumpInput(value.isPressed);
		}

		public void OnSprint(InputValue value)
		{
			SprintInput(value.isPressed);
		}

		public void OnInteract(InputValue value)
		{
			InteractInput(value.isPressed);
		}

		public void OnNext(InputValue value)
		{
			NextInput(value.isPressed);
		}

        public void OnPause(InputValue value)
        {
            PauseHandleInput(value.isPressed);
        }
#endif


        public void MoveInput(Vector2 newMoveDirection)
		{
			move = newMoveDirection;
		} 

		public void LookInput(Vector2 newLookDirection)
		{
			look = newLookDirection;
		}

		public void JumpInput(bool newJumpState)
		{
			jump = newJumpState;
		}

		public void SprintInput(bool newSprintState)
		{
			sprint = newSprintState;
		}

		public void InteractInput(bool newInteractState)
		{
			interact = newInteractState;

            OnInteractEvent?.Invoke(interact);	

        }

		public void NextInput(bool newNextState)
		{
			next = newNextState;
            OnNextEvent?.Invoke(next);

        }
        public void PauseHandleInput(bool newNextState)
        {
            pauseKey = newNextState;
            OnPausedPressedEvent?.Invoke(pauseKey);

        }


        private void OnApplicationFocus(bool hasFocus)
		{
			//if(ScreenManager.Instance.ActiveKey == ScreenManager.ScreenKey.GAME)
			//	SetCursorState(cursorLocked);
		}

		private void SetCursorState(bool newState)
		{
			Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None;
		}
	}
	
}