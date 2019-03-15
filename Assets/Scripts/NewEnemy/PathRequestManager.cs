using System;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;

[RequireComponent(typeof(PathFinding))]
public class PathRequestManager : MonoBehaviour {
    private PathFinding pathFinding;
    private Queue<PathResult> results = new Queue<PathResult>();

    private static PathRequestManager instance;

    private void Awake() {
        instance = this;
        pathFinding = GetComponent<PathFinding>();
    }

    private void Update() {
        if(results.Count > 0) {
            int itemsInQueue = results.Count;
            lock(results) {
                for (int i = 0; i < itemsInQueue; i++) {
                    PathResult result = results.Dequeue();
                    result.Callback(result.Path, result.Success);
                }
            }
        }
    }

    public static void RequestPath(PathRequest request) {
        ThreadStart threadStart = delegate {
            instance.pathFinding.FindPath(request, instance.FinishedProcessingPath);
        };
        threadStart.Invoke();
    }
    
    public void FinishedProcessingPath(PathResult result) {
        lock (results) {
            results.Enqueue(result);
        }
    }
}

public struct PathRequest {
    public Vector3 Start { get; private set; }
    public Vector3 End { get; private set; }
    public Action<Vector3[], bool> Callback { get; private set; }

    public PathRequest(Vector3 start, Vector3 end, Action<Vector3[], bool> callback) {
        this.Start = start;
        this.End = end;
        this.Callback = callback;
    }
}

public struct PathResult {
    public Vector3[] Path { get; private set; }
    public bool Success { get; private set; }
    public Action<Vector3[], bool> Callback { get; private set; }

    public PathResult(Vector3[] path, bool success, Action<Vector3[], bool> callback) {
        Path = path;
        Success = success;
        Callback = callback;
    }
}