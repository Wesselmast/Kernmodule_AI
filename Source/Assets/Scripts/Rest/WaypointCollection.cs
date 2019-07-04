using System.Linq;
using UnityEngine;

public class WaypointCollection : MonoBehaviour {
    private Vector3[] waypoints = default;

    private void Awake() {
        waypoints = GetComponentsInChildren<Transform>().
                    Where(go => go.gameObject != gameObject).
                    Select(wp => wp.position).ToArray();
    }

    public Vector3[] GetWaypoints() {
        return waypoints;
    }
}
