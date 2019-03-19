using UnityEngine;

[CreateAssetMenu(fileName = "New BlackBoard", menuName = "Custom/BlackBoard")]
public class BlackBoardSettings : ScriptableObject {
    [SerializeField] private float moveSpeed = 7f;
    public float MoveSpeed { get { return moveSpeed; } }
    [SerializeField] private float runSpeed = 7f;
    public float RunSpeed { get { return runSpeed; } }
    [SerializeField] private float turnSpeed = 5f;
    public float TurnSpeed { get { return turnSpeed; } }
    [SerializeField] private float lookAroundSpeed = 65f;
    public float LookAroundSpeed { get { return lookAroundSpeed; } }
}