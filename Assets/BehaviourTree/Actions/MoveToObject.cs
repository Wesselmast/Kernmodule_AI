using UnityEngine;
using IMBT;

public class InspectTransform : BTNodeBase {

    private const float approachRange = 1f;
    private Transform target;
    private bool done = false;
    private Vector3 spotToMove = Vector3.zero;

    public InspectTransform(Transform target) {
        this.target = target;
    }

    public override TaskStatus Tick(BlackBoard bb) {
        if (!done) {
            spotToMove = target.position;
            bb.Inspected = false;
            done = true;
        }

        if (target == null) {
            return TaskStatus.Failed;
        }

        float dist = Vector3.Distance(bb.agent.transform.position, spotToMove);
        if (dist < approachRange) {
            bb.Inspected = true;
            done = false;
            return TaskStatus.Success;
        }
        else {
            bb.agent.transform.position += (spotToMove - bb.agent.transform.position).normalized * bb.moveSpeed * Time.deltaTime;
            return TaskStatus.Running;
        }
    }
}

public class CheckIfHasInspected : BTNodeBase {
    public override TaskStatus Tick(BlackBoard bb) {
        if (bb.Inspected) return TaskStatus.Success;
        return TaskStatus.Failed;
    }
}


public class MoveBackToOldSpot : BTNodeBase {
    private const float approachRange = 1f;

    public override TaskStatus Tick(BlackBoard bb) {
        Vector3 spotToMove = bb.OldSpot;

        if (bb.OldSpot == null) {
            return TaskStatus.Failed;
        }

        float dist = Vector3.Distance(bb.agent.transform.position, spotToMove);
        if (dist < approachRange) {
            bb.OldSpotSaved = false;
            bb.Inspected = false;
            return TaskStatus.Success;
        }
        else {
            bb.agent.transform.position += (spotToMove - bb.agent.transform.position).normalized * bb.moveSpeed * Time.deltaTime;
            return TaskStatus.Running;
        }
    }
}

public class SaveOldSpot : BTNodeBase {
    public override TaskStatus Tick(BlackBoard bb) {
        if (!bb.OldSpotSaved) {
            bb.OldSpot = bb.agent.transform.position;
            bb.OldSpotSaved = true;
        }
        return TaskStatus.Success;
    }
}