using UnityEngine;

[CreateAssetMenu(fileName = "New BlackBoard", menuName = "Custom/BlackBoard")]
public class BlackBoardSettings : ScriptableObject {
    [SerializeField] private float moveSpeed = 15f;
    public float MoveSpeed { get { return moveSpeed; } }
    [SerializeField] private float turnSpeed = 5f;
    public float TurnSpeed { get { return turnSpeed; } }
}