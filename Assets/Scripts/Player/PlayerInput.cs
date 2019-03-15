namespace IsolatedMind {
    namespace Input {
        using System;
        using UnityEngine;

        public class PlayerInput : MonoBehaviour {
            public static event Action<float, float> OnMovement = delegate { };
            public static event Action<bool> OnRunning = delegate { };
            public static event Action<float, float> OnLookAround = delegate { };
            public static event Action OnJump = delegate { };
            public static event Action<MindState> OnStateChange = delegate { };
            public static event Action<int> OnWeaponChange = delegate { };
            public static event Action OnShoot = delegate { };
            public static Vector3 MousePosition;

            private void Update() {
                HandleLookAround();
                HandleStateChange();
                HandleMovement();
                HandleWeaponChange();
                if (Input.GetKeyDown(KeyCode.Space)) OnJump();
                if (Input.GetKey(KeyCode.Mouse0)) OnShoot();
                MousePosition = Input.mousePosition;
            }

            private void HandleMovement() {
                float horizontalMove = Input.GetAxis("Horizontal");
                float verticalMove = Input.GetAxis("Vertical");
                if (horizontalMove != 0.0f || verticalMove != 0.0f) {
                    OnMovement(horizontalMove, verticalMove);
                }
                OnRunning(Input.GetKey(KeyCode.LeftShift));
            }

            private void HandleLookAround() {
                float deltaX = Input.GetAxisRaw("Mouse X");
                float deltaY = Input.GetAxisRaw("Mouse Y");
                OnLookAround(deltaX, deltaY);
            }

            private void HandleStateChange() {
                if (Input.GetKeyDown(KeyCode.Alpha4)) OnStateChange(MindState.NORMAL);
                if (Input.GetKeyDown(KeyCode.Alpha5)) OnStateChange(MindState.DEPRESSION);
                if (Input.GetKeyDown(KeyCode.Alpha6)) OnStateChange(MindState.MANIC);
                if (Input.GetKeyDown(KeyCode.Alpha7)) OnStateChange(MindState.PSYCHOTIC);
            }

            private void HandleWeaponChange() {
                if (Input.GetKeyDown(KeyCode.Alpha1)) OnWeaponChange(0);
                if (Input.GetKeyDown(KeyCode.Alpha2)) OnWeaponChange(1);
                if (Input.GetKeyDown(KeyCode.Alpha3)) OnWeaponChange(2);
            }
        }
    }
}