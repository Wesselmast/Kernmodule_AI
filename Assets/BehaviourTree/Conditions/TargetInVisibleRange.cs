using IMBT;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Spot {
    New,
    Old
}


public class TargetInVisibleRange : BTNodeBase {
    public override TaskStatus Tick(BlackBoard bb) {
        Transform target = bb.fov.GetSeeableTarget(bb.target);
        if (target != null) {
                bb.NewSpot = target.position;
                return TaskStatus.Success;
            }
        return TaskStatus.Failed;
    }
}

public class TargetInShootableRange : BTNodeBase {
    public override TaskStatus Tick(BlackBoard bb) {
        if (bb.fov.GetShootableTarget(bb.target) != null) return TaskStatus.Success;
        return TaskStatus.Failed;
    }
}

public class BTTimer : BTNodeBase {
    private readonly float amtOfSeconds;
    private readonly bool reset = true;
    private float elapsed = 0;

    public BTTimer(float amtOfSeconds) {
        this.amtOfSeconds = amtOfSeconds;
    }

    public BTTimer(float amtOfSeconds, bool reset) {
        this.reset = reset;
        this.amtOfSeconds = amtOfSeconds;
    }

    public override TaskStatus Tick(BlackBoard bb) {
        if(elapsed > amtOfSeconds) {
            if (reset) elapsed = 0;
            return TaskStatus.Success;
        }
        else {
            elapsed += Time.deltaTime;
            return TaskStatus.Failed;
        }
    }
}

//public class MoveToSpot : BTNodeBase {
//    private Vector3[] path;
//    private int index;
//    private MonoBehaviour monoBehaviour;

//    public MoveToSpot(MonoBehaviour monoBehaviour) {
//        this.monoBehaviour = monoBehaviour;
//    }

//    public override TaskStatus Tick(BlackBoard bb) {
//        PathRequestManager.RequestPath(new PathRequest(bb.agent.transform.position, bb.target.position, OnPathFound));
//    }

//    public void OnPathFound(Vector3[] newPath, bool success) {
//        if (success) {
//            path = newPath;
//            monoBehaviour.StopCoroutine(FollowPath());
//            monoBehaviour.StartCoroutine(FollowPath());
//        }
//    }

//    private IEnumerator FollowPath() {
//        Vector3 currentWp = path[0];
//        while (true) {
//            if (transform.position == currentWp) {
//                index++;
//                if (index >= path.Length) yield break;
//                currentWp = path[index];
//            }
//            transform.position = Vector3.MoveTowards(transform.position, currentWp, speed);
//            yield return null;
//        }
//    }
//}