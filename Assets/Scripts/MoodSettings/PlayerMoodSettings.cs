using UnityEngine;

[System.Serializable]
public class PlayerMoodSettings : IMoodSettings { 
    [SerializeField] private float walkSpeed;
    public float WalkSpeed { get { return walkSpeed; } }
    [SerializeField] private float runSpeed;
    public float RunSpeed { get { return runSpeed; } }
    [SerializeField] private float lookAroundSens;
    public float LookAroundSens { get { return lookAroundSens; } }
    [SerializeField] private float lookAroundSmooth;
    public float LookAroundSmooth { get { return lookAroundSmooth; } }
    [SerializeField] private float jumpForce;
    public float JumpForce { get { return jumpForce; } }
    [SerializeField] private float weaponSwitchTime;
    public float WeaponSwitchTime { get { return weaponSwitchTime; } }
    [SerializeField] private float maxYAngleForCamera;
    public float MaxYAngle { get { return maxYAngleForCamera; } }
}