using UnityEngine;

public class CollisionState : MonoBehaviour {
    [SerializeField] private LayerMask layerMask = 1<<0;

    public bool IsGrounded {
        get {
            return Physics.Raycast(transform.position, Vector3.down, 1f, layerMask);
        }
    }
}