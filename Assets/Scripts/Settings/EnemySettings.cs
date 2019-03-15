using UnityEngine;

[CreateAssetMenu(menuName = "Stats/Enemy", fileName = "New Enemy")]
public class EnemySettings : ScriptableObject, ISettings {
    [Header("Normal")]
    [SerializeField] private EnemyMoodSettings normal;
    [Header("Manic")]
    [SerializeField] private EnemyMoodSettings manic;
    [Header("Psychotic")]
    [SerializeField] private EnemyMoodSettings psychotic;
    [Header("Depressed")]
    [SerializeField] private EnemyMoodSettings depressed;

    public IMoodSettings GetMoodSettings(MindState mood) {
        switch (mood) {
            case MindState.MANIC: return manic;
            case MindState.DEPRESSION: return depressed;
            case MindState.PSYCHOTIC: return psychotic;
            default: return normal;
        }
    }
}