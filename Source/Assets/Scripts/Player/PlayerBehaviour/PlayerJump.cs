using IsolatedMind.Input;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : PlayerBehaviour {
    private void OnEnable() {
        PlayerInput.OnJump += HandleJump;
    }

    private void OnDisable() {
        PlayerInput.OnJump -= HandleJump;
    }

    private void HandleJump() {
        if (!collisionState.IsGrounded) return;
        rb.AddForce(new Vector3(0, settings.JumpForce, 0), ForceMode.Impulse);
    }
}