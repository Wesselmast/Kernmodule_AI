using UnityEngine;

public class EnemyFOV : MonoBehaviour {
    [SerializeField] private float communicationRadius = 15;
    public float CommunicationRadius { get { return communicationRadius; } }
    [SerializeField] private float viewRadius = 10;
    public float ViewRadius { get { return viewRadius; } }
    [SerializeField] private float shootRadius = 5;
    public float ShootRadius { get { return shootRadius; } }
    [SerializeField][Range(0, 360)] private float viewAngle = 90;
    public float ViewAngle { get { return viewAngle; } }

    public Vector3 DirFromAngle(float angle) {
        float calcAngle = (angle + transform.eulerAngles.y) * Mathf.Deg2Rad;
        return new Vector3(Mathf.Sin(calcAngle), 0, Mathf.Cos(calcAngle));
    }

    public Transform GetSeeableTarget(Transform target) {
        return FoundTarget(viewRadius, target) ? target : null;
    }

    public Transform GetShootableTarget(Transform target) {
        return FoundTarget(shootRadius, target) ? target : null;
    }

    private bool FoundTarget(float radius, Transform target) {
        float dist = Vector3.Distance(transform.position, target.position);
        if (dist <= radius) {
            Vector3 dirToTarget = (target.position - transform.position).normalized;
            if (Vector3.Angle(transform.forward, dirToTarget) < viewAngle / 2) {
                if (!Physics.Raycast(transform.position, dirToTarget, dist, LayerMask.GetMask("Obstacle"))) { 
                    return true;
                }
            }
        }
        return false;
    }
}