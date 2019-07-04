using UnityEngine;
using Kernmodule3.Input;
using System.Collections;

public class PlayerCrouch : PlayerBehaviour {
    private Vector3 startPosition = default;
    private Camera cam = default;

    protected override void Wake() {
        cam = Camera.main;
        startPosition = cam.transform.localPosition;
    }

    private void OnEnable() {
        PlayerInput.OnCrouching += Crouch;
        PlayerInput.OnStopCrouching += StopCrouch;
    }

    private void OnDisable() {
        PlayerInput.OnCrouching -= Crouch;
        PlayerInput.OnStopCrouching -= StopCrouch;
    }

    private void Crouch() {
        StopAllCoroutines();
        StartCoroutine(MoveTowards(startPosition.y + settings.CrouchEndYPosition));
    }

    private void StopCrouch() {
        StopAllCoroutines();
        StartCoroutine(MoveTowards(startPosition.y));
    }
    
    private IEnumerator MoveTowards(float towards) {
        while (!CloseTo(cam.transform.localPosition.y - towards, 0.05f)) {
            float yVel = 0;
            float newY = Mathf.SmoothDamp(cam.transform.localPosition.y, towards, ref yVel, settings.CrouchTime, Mathf.Infinity, Time.deltaTime * 5);
            cam.transform.localPosition = cam.transform.localPosition.SetY(newY);
            yield return null;
        }
        cam.transform.localPosition = cam.transform.localPosition.SetY(towards);
    }

    private bool CloseTo(float value, float howClose) {
        return value < howClose && value > -howClose;
    }
}