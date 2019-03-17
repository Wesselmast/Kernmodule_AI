using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour {
    [SerializeField] private Transform target;
    [SerializeField] private float speed;

    private Vector3[] path;
    private int index;

    private void Start() {
        PathRequestManager.RequestPath(new PathRequest(transform.position, target.position,OnPathFound));
    }

    public void OnPathFound(Vector3[] newPath, bool success) {
        if(success) {
            path = newPath;
            StopCoroutine(FollowPath());
            StartCoroutine(FollowPath());
        }
    }

    private IEnumerator FollowPath() {
        Vector3 currentWp = path[0];
        while(true) {
            if(transform.position == currentWp) {
                index++;
                if (index >= path.Length) yield break;
                currentWp = path[index];
            }
            transform.position = Vector3.MoveTowards(transform.position, currentWp, speed);
            yield return null;
        }
    }

    private void OnDrawGizmos() {
        if(path != null) {
            for (int i = index; i < path.Length; i++) {
                Gizmos.color = Color.cyan;
                Gizmos.DrawCube(path[i], Vector3.one);
                if(i == index) {
                    Gizmos.DrawLine(transform.position, path[i]);
                }
                else {
                    Gizmos.DrawLine(path[i - 1], path[i]);
                }
            }
        }
    }
}
