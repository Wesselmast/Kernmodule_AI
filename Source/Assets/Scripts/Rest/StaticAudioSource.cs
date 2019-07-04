using UnityEngine;

public class StaticAudioSource : MonoBehaviour {
    private void Awake() {
        DontDestroyOnLoad(this);
    }
}
