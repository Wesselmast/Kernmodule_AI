using UnityEngine;
using IMBT;

public class MoveToSpot : BTNodeBase {

    private const float approachRange = 1f;
    private Spot spot;
    private bool onlyOnce = true;
    private bool done = false;

    public MoveToSpot(Spot spot) {
        this.spot = spot;
    }

    public MoveToSpot(Spot spot, bool onlyOnce) {
        this.spot = spot;
        this.onlyOnce = onlyOnce;
    }

    public override TaskStatus Tick(BlackBoard bb) {

        Vector3 spotToMove;
        if (onlyOnce && !done) {
            bb.OldSpot = bb.agent.transform.position;
            if (spot == Spot.New) spotToMove = bb.NewSpot;
            else spotToMove = bb.OldSpot;
            done = true;
        }
        else {
            if (spot == Spot.New) spotToMove = bb.NewSpot;
            else spotToMove = bb.OldSpot;
        }

        if (spotToMove == Vector3.zero) {
            return TaskStatus.Failed;
        }

        float dist = Vector3.Distance(bb.agent.transform.position, spotToMove);
        if (dist < approachRange) {
            done = false;
            return TaskStatus.Success;
        }
        else {
            bb.agent.transform.position += (spotToMove - bb.agent.transform.position).normalized * bb.moveSpeed * Time.deltaTime;
            return TaskStatus.Running;
        }
    }
}
