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

		public bool isMenu = false;

		public UIManager uiManager;

		[Header("Movement Settings")]
		public bool analogMovement;

		[Header("Mouse Cursor Settings")]
		public bool cursorLocked = true;
		public bool cursorInputForLook = true;

#if ENABLE_INPUT_SYSTEM
		public void OnMove(InputValue value)
		{
			if(isMenu is false) MoveInput(value.Get<Vector2>());
		}

		public void OnLook(InputValue value)
		{
			if(cursorInputForLook)
			{
				if (isMenu is false) LookInput(value.Get<Vector2>());
			}
		}

		public void OnJump(InputValue value)
		{
			if (isMenu is false) JumpInput(value.isPressed);
		}

		public void OnSprint(InputValue value)
		{
			if (isMenu is false) SprintInput(value.isPressed);
		}

		public void OnMenu(InputValue value)
		{
			isMenu = !isMenu;
			MenuInput(value.isPressed);			
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
		public void MenuInput(bool newMenuState)
		{
			// ������ �Է��� �����մϴ�.
			move = Vector2.zero;
			look = Vector2.zero;
			jump = false;
			sprint = false;

			// ���콺 Ŀ�� ���¸� ����մϴ�.
			cursorLocked = !cursorLocked;
			SetCursorState(cursorLocked);

			uiManager.ToggleMenuUI(isMenu);

		}

		private void OnApplicationFocus(bool hasFocus)
		{
			SetCursorState(cursorLocked);
		}

		private void SetCursorState(bool newState)
		{
			Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None;
			Cursor.visible = !newState;
		}


	}
	
}