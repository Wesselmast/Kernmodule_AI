using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Grid))]
public class PathFinding : MonoBehaviour {
    private Grid grid;

    private void Awake() {
        grid = GetComponent<Grid>();
    }
   
    public void FindPath(PathRequest request, Action<PathResult> callback) {
        Vector3[] waypoints = new Vector3[0];
        bool success = false;

        Node startNode = grid.GetNode(request.Start);
        Node endNode = grid.GetNode(request.End);

        if (startNode.Walkable && endNode.Walkable) {
            Heap<Node> open = new Heap<Node>(grid.MaxSize);
            HashSet<Node> closed = new HashSet<Node>();
            open.Add(startNode);
            while (open.Count > 0) {
                Node currentNode = open.RemoveFirst();
                closed.Add(currentNode);

                if (currentNode == endNode) {
                    success = true;
                    break;
                }

                foreach (Node neigh in grid.GetNeighbours(currentNode)) {
                    if (!neigh.Walkable || closed.Contains(neigh)) continue;

                    int newMovementCost = currentNode.GCost + GetDistance(currentNode, neigh);
                    if (newMovementCost < neigh.GCost || !open.Contains(neigh)) {
                        neigh.GCost = newMovementCost;
                        neigh.HCost = GetDistance(neigh, endNode);
                        neigh.Parent = currentNode;
                        if (!open.Contains(neigh)) {
                            open.Add(neigh);
                        }
                        else {
                            open.UpdateItem(neigh);
                        }
                    }
                }
            }
        }
        if(success) {
            waypoints = Trace(startNode, endNode);
            success = waypoints.Length > 0;
        }
        callback(new PathResult(waypoints, success, request.Callback));
    }

    private Vector3[] Trace(Node start, Node end) {
        List<Node> path = new List<Node>();
        Node currentNode = end;
        while(currentNode != start) {
            path.Add(currentNode);
            currentNode = currentNode.Parent;
        }
        Vector3[] waypoints = Simplify(path);
        Array.Reverse(waypoints);
        return waypoints;
    }

    private Vector3[] Simplify(List<Node> path) {
        List<Vector3> waypoints = new List<Vector3>();
        Vector2 dirOld = Vector2.zero;
        for (int i = 1; i < path.Count; i++) {
            Vector2 dirNew = new Vector2(path[i - 1].GridX - path[i].GridX, path[i - 1].GridZ - path[i].GridZ);
            if(dirNew != dirOld) {
                waypoints.Add(path[i].WorldPos);
            }
            dirOld = dirNew;
        }
        return waypoints.ToArray();
    }

    private int GetDistance(Node a, Node b) {
        int dstX = Mathf.Abs(a.GridX - b.GridX);
        int dstZ = Mathf.Abs(a.GridZ - b.GridZ);
        if(dstX > dstZ) {
            return 14 * dstZ + 10 * (dstX - dstZ);
        }
        else {
            return 14 * dstX + 10 * (dstZ - dstX);
        }
    }
}
