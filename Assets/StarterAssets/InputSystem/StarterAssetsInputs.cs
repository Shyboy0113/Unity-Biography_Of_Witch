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

		public UIManager uiManager;

		[Header("Movement Settings")]
		public bool analogMovement;

		[Header("Mouse Cursor Settings")]
		public bool cursorLocked = true;
		public bool cursorInputForLook = true;

#if ENABLE_INPUT_SYSTEM
		public void OnMove(InputValue value)
		{
			if(UIManager.Instance.isMenu is false) MoveInput(value.Get<Vector2>());
		}

		public void OnLook(InputValue value)
		{
			if(cursorInputForLook)
			{
				if (UIManager.Instance.isMenu is false) LookInput(value.Get<Vector2>());
			}
		}

		public void OnJump(InputValue value)
		{
			if (UIManager.Instance.isMenu is false) JumpInput(value.isPressed);
		}

		public void OnSprint(InputValue value)
		{
			if (UIManager.Instance.isMenu is false) SprintInput(value.isPressed);
		}

		public void OnMenu(InputValue value)
		{
			UIManager.Instance.isMenu = !UIManager.Instance.isMenu;
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
			// 움직임 입력을 정지합니다.
			move = Vector2.zero;
			look = Vector2.zero;
			jump = false;
			sprint = false;

			// 마우스 커서 상태를 토글합니다.
			cursorLocked = !cursorLocked;
			SetCursorState(cursorLocked);

			if (uiManager is null)
            {
				uiManager = GameObject.Find("UIManager").GetComponent<UIManager>();
            }

			uiManager.ToggleMenuUI(UIManager.Instance.isMenu);

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