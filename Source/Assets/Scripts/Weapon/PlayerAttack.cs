using Kernmodule3.Input;
using UnityEngine;

public class PlayerAttack : PlayerBehaviour {
    private Camera cam = default;
    private Weapon weapon = default;
    private bool hasWeaponChangeScript = true;

    protected override void Wake() {
        cam = Camera.main;
        weapon = GetComponentInChildren<Weapon>();
    }

    private void OnEnable() {
        hasWeaponChangeScript = GetComponent<PlayerWeaponChange>() != null;


        if(hasWeaponChangeScript)  PlayerInput.OnAttack += Attack;
        else                       PlayerInput.OnAttack += AttackSingle;
    }

    private void OnDisable() {
        if (hasWeaponChangeScript) PlayerInput.OnAttack -= Attack;
        else                       PlayerInput.OnAttack -= AttackSingle;
    }

    private void Attack() {
        PlayerWeaponChange.SelectedWeapon.Attack(cam.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0)));
    }

    private void AttackSingle() {
        weapon.Attack(cam.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0)));
    }
}