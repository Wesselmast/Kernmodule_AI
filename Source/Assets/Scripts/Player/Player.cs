using UnityEngine;

public class Player : MonoBehaviour {
    [SerializeField] private PlayerSettings settings;
    public PlayerSettings Settings {
        get { return settings; }
        set { settings = value; }
    }
}