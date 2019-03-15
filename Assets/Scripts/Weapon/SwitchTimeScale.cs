using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchTimeScale : MonoBehaviour {

    [SerializeField] private float newScale;

    private void Awake() {
        Time.timeScale = newScale;
    }

}
