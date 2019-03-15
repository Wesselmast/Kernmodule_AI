using UnityEngine;

[CreateAssetMenu(menuName = "Stats/Player", fileName = "New Player")]
public class PlayerSettings : ScriptableObject, ISettings {
    [Header("Normal")]
    [SerializeField] private PlayerMoodSettings normal;
    [Header("Manic")]
    [SerializeField] private PlayerMoodSettings manic;
    [Header("Psychotic")]
    [SerializeField] private PlayerMoodSettings psychotic;
    [Header("Depressed")]
    [SerializeField] private PlayerMoodSettings depressed;

    public IMoodSettings GetMoodSettings(MindState mood) {
        switch (mood) {
            case MindState.MANIC: return manic;
            case MindState.DEPRESSION: return depressed;
            case MindState.PSYCHOTIC: return psychotic;
            default: return normal;
        }
    }
}