using UnityEngine;

public class Player : MonoBehaviour {
    [SerializeField] private PlayerSettings globalSettings;
    public PlayerSettings GlobalSettings {
        get { return globalSettings; }
        set { globalSettings = value; }
    }

    public PlayerMoodSettings Settings {
        get {
            return (PlayerMoodSettings)globalSettings.GetMoodSettings(MindStateManager.State);
        }
    }
}