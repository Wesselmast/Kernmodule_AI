using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VaalsBT;

public class MoveToObject : BTNodeBase {

    private float approachRange = 1f;
    private GameObject target;
    public MoveToObject(GameObject target) {
        this.target = target;
    }

    public override TaskStatus Tick(BlackBoard bb) {
        if (target == null) {
            return TaskStatus.Failed;
        }

        float dist = Vector3.Distance(bb.agent.transform.position, target.transform.position);
        if (dist < approachRange) {
            return TaskStatus.Success;
        }
        else {
            bb.agent.transform.position += (target.transform.position - bb.agent.transform.position).normalized * bb.moveSpeed * Time.deltaTime;
            return TaskStatus.Running;
        }
    }

}
