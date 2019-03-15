using IsolatedMind.Input;
using UnityEngine;

[RequireComponent(typeof(PlayerWeaponChange))]
public class PlayerAttack : PlayerBehaviour {
    private Camera cam;

    protected override void Wake() {
        cam = Camera.main;
    }

    private void OnEnable() {
        PlayerInput.OnShoot += Attack;
    }

    private void OnDisable() {
        PlayerInput.OnShoot -= Attack;
    }

    private void Attack() {
        PlayerWeaponChange.SelectedWeapon.Attack(cam.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0)));
    }
}