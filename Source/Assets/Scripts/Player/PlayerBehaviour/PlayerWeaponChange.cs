using UnityEngine;
using IsolatedMind.Input;
using System.Collections;

public class PlayerWeaponChange : PlayerBehaviour {
    public static Weapon SelectedWeapon;

    private Player player;
    private Weapon[] weapons;

    protected override void Wake() {
        PlayerInput.OnWeaponChange += ChangeWeapons;
        weapons = GetComponentsInChildren<Weapon>(true);
        player = GetComponent<Player>();
        SelectedWeapon = weapons[0];
    }

    private void ChangeWeapons(int index) {
        foreach (var w in weapons) {
            w.gameObject.SetActive(false);
        }
        StartCoroutine(SwitchToDifferent(index));
    }

    private IEnumerator SwitchToDifferent(int index) {
        yield return new WaitForSeconds(settings.WeaponSwitchTime);
        SelectedWeapon = weapons[index];
        weapons[index].gameObject.SetActive(true);
    }
}