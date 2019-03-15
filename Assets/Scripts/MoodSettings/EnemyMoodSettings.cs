using UnityEngine;

[System.Serializable]
public struct EnemyMoodSettings : IMoodSettings {
    [SerializeField] private float health;
    public float Health { get { return health; } }
    [SerializeField] private float walkSpeed;
    public float WalkSpeed { get { return walkSpeed; } }
    [SerializeField] private float sprintSpeed;
    public float SprintSpeed { get { return sprintSpeed; } }
    [SerializeField] private float rotationSpeed;
    public float RotationSpeed { get { return rotationSpeed; } }
}