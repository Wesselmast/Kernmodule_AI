using IsolatedMind.Input;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : PlayerBehaviour {
    private bool isRunning = false;

    private void OnEnable() {
        PlayerInput.OnMovement += HandleMovement;
        PlayerInput.OnRunning += HandleRunning;
    }

    private void OnDisable() {
        PlayerInput.OnMovement -= HandleMovement;
        PlayerInput.OnRunning -= HandleRunning;
    }

    private void HandleMovement(float horizontal, float vertical) {
        float deltaSpeed = isRunning ? settings.RunSpeed * Time.deltaTime : settings.WalkSpeed * Time.deltaTime;
        transform.Translate(new Vector3(horizontal, 0.0f, vertical) * deltaSpeed);
    }

    private void HandleRunning(bool value) {
        isRunning = value;
    }
}