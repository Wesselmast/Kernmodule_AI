using UnityEngine;

[CreateAssetMenu(menuName = "Stats/Weapon", fileName = "New Weapon")]
public class WeaponSettings : ScriptableObject, ISettings {
    [Header("Normal")]
    [SerializeField] private WeaponMoodSettings normal;
    [Header("Manic")]
    [SerializeField] private WeaponMoodSettings manic;
    [Header("Psychotic")]
    [SerializeField] private WeaponMoodSettings psychotic;
    [Header("Depressed")]
    [SerializeField] private WeaponMoodSettings depressed;

    public IMoodSettings GetMoodSettings(MindState mood) {
        switch (mood) {
            case MindState.MANIC: return manic;
            case MindState.DEPRESSION: return depressed;
            case MindState.PSYCHOTIC: return psychotic;
            default: return normal;
        }
    }
}