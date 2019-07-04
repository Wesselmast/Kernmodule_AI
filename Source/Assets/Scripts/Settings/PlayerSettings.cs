using UnityEngine;

[CreateAssetMenu(menuName = "Stats/Player", fileName = "New Player")]
public class PlayerSettings : ScriptableObject {
    [SerializeField] private float walkSpeed = 8f;
    public float WalkSpeed { get { return walkSpeed; } }
    [SerializeField] private float runSpeed = 15f;
    public float RunSpeed { get { return runSpeed; } }
    [SerializeField] private float lookAroundSens = 10f;
    public float LookAroundSens { get { return lookAroundSens; } }
    [SerializeField] private float lookAroundSmooth = 2f;
    public float LookAroundSmooth { get { return lookAroundSmooth; } }
    [SerializeField] private float jumpForce = 7f;
    public float JumpForce { get { return jumpForce; } }
    [SerializeField] private float weaponSwitchTime = 1f;
    public float WeaponSwitchTime { get { return weaponSwitchTime; } }
    [SerializeField] private float health = 0;
    public float Health { get { return health; } }
    [SerializeField][Range(0,90)] private float maxYAngleForCamera = 80f;
    public float MaxYAngle { get { return maxYAngleForCamera; } }

    [Header("Crouching")]
    [SerializeField] private float crouchSpeed = 1.2f;
    public float CrouchSpeed => crouchSpeed;
    [SerializeField] private float crouchrunSpeed = 3.0f;
    public float CrouchrunSpeed => crouchrunSpeed;
    [SerializeField] private float crouchTime = 0.3f;
    public float CrouchTime => crouchTime;
    [SerializeField] private float crouchEndYPosition = -0.5f;
    public float CrouchEndYPosition => crouchEndYPosition;
}