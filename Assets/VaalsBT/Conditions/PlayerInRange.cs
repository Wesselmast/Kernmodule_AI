using UnityEngine;
using VaalsBT;

public class PlayerInRange : BTNodeBase {
    private float range;
    public PlayerInRange(float range) {
        this.range = range;
    }

    public override TaskStatus Tick(BlackBoard bb) {
        Vector3 playerPos = bb.target.position;

        if (Vector3.Distance(bb.agent.transform.position, playerPos) < range) {
            return TaskStatus.Success;
        }
        return TaskStatus.Failed;
    }
}
