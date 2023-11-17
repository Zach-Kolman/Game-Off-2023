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
		public Vector2 mousePos;

		public bool jump;
		public bool sprint;
		public bool interact;
		public bool pause;
		public bool leftMouse;

		[Header("Movement Settings")]
		public bool analogMovement;

		[Header("Mouse Cursor Settings")]
		public bool cursorLocked = true;
		public bool cursorInputForLook = true;

#if ENABLE_INPUT_SYSTEM
		public void OnMove(InputValue value)
		{
			MoveInput(value.Get<Vector2>());
		}

		public void OnLeftMouse(InputValue value)
        {
			LeftMouseInput(value.isPressed);
        }

		public void OnMousePos(InputValue value)
        {
			MousePosInput(value.Get<Vector2>());
        }

		public void OnInteract(InputValue value)
        {
			InteractInput(value.isPressed);
        }

		public void OnPause(InputValue value)
        {
			PauseInput(value.isPressed);
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
#endif


		public void MoveInput(Vector2 newMoveDirection)
		{
			move = newMoveDirection;
		} 

		public void MousePosInput(Vector2 newMousePos)
        {
			mousePos = newMousePos;
        }

		public void LookInput(Vector2 newLookDirection)
		{
			look = newLookDirection;
		}

		public void LeftMouseInput(bool newLeftMouseState)
        {
			leftMouse = newLeftMouseState;
        }

		public void PauseInput(bool newPauseState)
        {
			pause = newPauseState;
        }

		public void InteractInput(bool newInteractState)
        {
			interact = newInteractState;
        }

		public void JumpInput(bool newJumpState)
		{
			jump = newJumpState;
		}

		public void SprintInput(bool newSprintState)
		{
			sprint = newSprintState;
		}

		private void OnApplicationFocus(bool hasFocus)
		{
			SetCursorState(cursorLocked);
		}

		private void SetCursorState(bool newState)
		{
			Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None;
		}
	}
	
}