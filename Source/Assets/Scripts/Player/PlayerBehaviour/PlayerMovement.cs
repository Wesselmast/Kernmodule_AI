using Kernmodule3.Input;
using UnityEngine;

public class PlayerMovement : PlayerBehaviour {
    private void OnEnable() {
        PlayerInput.OnMovement += HandleMovement;
    }

    private void OnDisable() {
        PlayerInput.OnMovement -= HandleMovement;
    }

    private void HandleMovement(float horizontal, float vertical, MoveMode moveMode) {
        float deltaSpeed = Time.deltaTime;
        switch (moveMode) {
            case MoveMode.Crouching: deltaSpeed *= settings.CrouchSpeed; break;
            case MoveMode.Crouchrunning: deltaSpeed *= settings.CrouchrunSpeed; break;
            case MoveMode.Running: deltaSpeed *= settings.RunSpeed; break;
            default: deltaSpeed *= settings.WalkSpeed; break;
        }
        transform.Translate(new Vector3(horizontal, 0.0f, vertical).normalized * deltaSpeed);
    }
}
