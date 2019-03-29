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
    [SerializeField][Range(0,90)] private float maxYAngleForCamera = 80f;
    public float MaxYAngle { get { return maxYAngleForCamera; } }
}