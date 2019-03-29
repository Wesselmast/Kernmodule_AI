using UnityEngine;

[CreateAssetMenu(fileName = "New BlackBoard", menuName = "Custom/BlackBoard")]
public class BlackBoardSettings : ScriptableObject {
    [SerializeField] private float walkSpeed = 7f;
    public float WalkSpeed { get { return walkSpeed; } }
    [SerializeField] private float runSpeed = 7f;
    public float RunSpeed { get { return runSpeed; } }
    [SerializeField] private float turnSpeed = 5f;
    public float TurnSpeed { get { return turnSpeed; } }
    [SerializeField] private float lookAroundSpeed = 65f;
    public float LookAroundSpeed { get { return lookAroundSpeed; } }
    [SerializeField] private float health = 100f;
    public float Health { get { return health; } }
}