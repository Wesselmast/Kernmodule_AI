using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Weapon))]
public class WeaponRangeEditor : Editor {
    private void OnSceneGUI() {
        Weapon weapon = (Weapon)target;
        Handles.color = Color.red;
        Handles.DrawWireArc(weapon.transform.position, Vector3.up, Vector3.forward, 360, weapon.Settings.MeleeRange);
    }
}