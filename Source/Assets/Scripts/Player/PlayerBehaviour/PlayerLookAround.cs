using IsolatedMind.Input;
using UnityEngine;

public class PlayerLookAround : PlayerBehaviour {
    private Camera cam;

    private Vector2 lookDelta;
    private Vector2 lookRotation;
    private Vector2 smoothedLookDelta;

    protected override void Wake() {
        Cursor.lockState = CursorLockMode.Locked;
        cam = Camera.main;
    }

    private void OnEnable() {
        PlayerInput.OnLookAround += HandleLookAround;
    }

    private void OnDisable() {
        PlayerInput.OnLookAround -= HandleLookAround;
    }

    protected override void Tick() {
        lookDelta = Vector2.Scale(lookDelta, new Vector2(settings.LookAroundSens, settings.LookAroundSens));
        smoothedLookDelta.x = Mathf.Lerp(smoothedLookDelta.x, lookDelta.x, 1f / settings.LookAroundSmooth);
        smoothedLookDelta.y = Mathf.Lerp(smoothedLookDelta.y, lookDelta.y, 1f / settings.LookAroundSmooth);
        lookRotation += smoothedLookDelta;

        lookRotation.y = Mathf.Clamp(lookRotation.y, -settings.MaxYAngle, settings.MaxYAngle);

        cam.transform.localRotation = Quaternion.AngleAxis(-lookRotation.y, Vector3.right);
        transform.localRotation = Quaternion.AngleAxis(lookRotation.x, transform.up);
    }

    private void HandleLookAround(float x, float y) {
        lookDelta.x = x;
        lookDelta.y = y;
    }
}