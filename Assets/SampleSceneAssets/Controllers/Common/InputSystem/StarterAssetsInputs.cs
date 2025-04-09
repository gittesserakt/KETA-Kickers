using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

namespace StarterAssets
{
	public class StarterAssetsInputs : MonoBehaviour
	{
		[Header("Character Input Values")]
		public Vector2 move;
		public bool dash;
		public bool sprint;
		public bool attack;

		[Header("Movement Settings")]
		public bool analogMovement;

		[Header("Mouse Cursor")]
		public bool cursorLocked = true;
		public bool cursorInputForLook = true;
		private Vector2 screenCenter = new Vector2(Screen.width / 2f, Screen.height / 2f);
        private Vector2 mousePosition = Vector2.zero;
        public float angle = 0f;

		public void OnMove(InputValue value)
		{
			MoveInput(value.Get<Vector2>());
		}

		public void OnLook(InputValue value)
		{
			if(cursorInputForLook)
			{
				mousePosition = new Vector2(Input.mousePosition.x, Screen.height - Input.mousePosition.y);

				Vector2 direction = mousePosition - screenCenter;
				angle = Mathf.Atan2(direction.x, -direction.y) * Mathf.Rad2Deg;
				angle = (angle + 360f) % 360f;
			}
		}

		void OnGUI()
		{
			DrawLine(screenCenter, mousePosition, Color.red, 2f);
			GUI.Label(new Rect(10, 10, 200, 30), $"Angle: {angle:F2}°");
		}

		public void OnDash(InputValue value)
		{
			DashInput(value.isPressed);
		}

		public void OnSprint(InputValue value)
		{
			SprintInput(value.isPressed);
		}

		public void OnAttack(InputValue value)
		{
			AttackInput(value.isPressed);
		}

		public void MoveInput(Vector2 newMoveDirection)
		{
			move = newMoveDirection;
		}

		public void DashInput(bool newDashState)
		{
			dash = newDashState;
		}

		public void AttackInput(bool newAttackState)
		{
			attack = newAttackState;
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

        // Draw a simple line using GUI
        void DrawLine(Vector2 start, Vector2 end, Color color, float width)
        {
            Matrix4x4 matrix = GUI.matrix;
            Color originalColor = GUI.color;

            // Set color and rotation
            GUI.color = color;
            float angle = Vector3.Angle(end - start, Vector2.right);
            if (start.y > end.y) angle = -angle;
            Vector2 pivot = start;
            GUIUtility.RotateAroundPivot(angle, pivot);

            // Draw line
            GUI.DrawTexture(new Rect(start.x, start.y, (end - start).magnitude, width), Texture2D.whiteTexture);

            // Reset
            GUI.matrix = matrix;
            GUI.color = originalColor;
        }
	}
	
}
