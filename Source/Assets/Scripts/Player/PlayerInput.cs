namespace Kernmodule3 {
    namespace Input {
        using System;
        using UnityEngine;

        public enum MoveMode {
            Walking, Crouching, Running, Crouchrunning
        }

        public class PlayerInput : PlayerBehaviour {
            public static event Action<float, float, MoveMode> OnMovement = delegate { };
            public static event Action<float, float> OnLookAround = delegate { };
            public static event Action OnJump = delegate { };
            public static event Action<int> OnWeaponChange = delegate { };
            public static event Action OnAttack = delegate { };
            public static event Action OnCrouching = delegate { };
            public static event Action OnStopCrouching = delegate { };
            public static Vector3 MousePosition;

            private MoveMode moveMode = MoveMode.Walking;
            private bool crouching = false;
            private float horizontalMove, verticalMove;

            private void Update() {
                horizontalMove = Input.GetAxisRaw("Horizontal");
                verticalMove = Input.GetAxisRaw("Vertical");
                float deltaX = Input.GetAxisRaw("Mouse X");
                float deltaY = Input.GetAxisRaw("Mouse Y");

                if (!(deltaX == 0.0f && deltaY == 0.0f)) {
                    OnLookAround(deltaX, deltaY);
                }

                if (collisionState.IsGrounded) {
                    if (Input.GetKey(KeyCode.LeftShift)) moveMode = MoveMode.Running;
                    if (Input.GetKey(KeyCode.LeftControl)) {
                        if (!crouching) {
                            OnCrouching();
                            crouching = true;
                        }
                    }
                    else if (crouching) {
                        OnStopCrouching();
                        crouching = false;
                    }
                    if (crouching) {
                        moveMode = moveMode == MoveMode.Running ? MoveMode.Crouchrunning : MoveMode.Crouching;
                    }
                }

                if (Input.GetKeyDown(KeyCode.Alpha1)) OnWeaponChange(0);
                if (Input.GetKeyDown(KeyCode.Alpha2)) OnWeaponChange(1);
                if (Input.GetKeyDown(KeyCode.Alpha3)) OnWeaponChange(2);
                if (Input.GetKeyDown(KeyCode.Space)) OnJump();
                if (Input.GetKey(KeyCode.Mouse0)) OnAttack();
                MousePosition = Input.mousePosition;
            }

            private void FixedUpdate() {
                if (!(horizontalMove == 0.0f && verticalMove == 0.0f)) {
                    OnMovement(horizontalMove, verticalMove, moveMode);
                }
                moveMode = MoveMode.Walking;
            }

        }
    }
}