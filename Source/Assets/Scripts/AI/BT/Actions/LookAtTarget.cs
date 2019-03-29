using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IMBT {
    public class LookAtTarget : BTNode {
        public override BTTaskStatus Tick(BlackBoard bb) {
            bb.GetValue<GameObject>("Agent").transform.LookAt(bb.GetValue<Transform>("Target"));
            return BTTaskStatus.Success;
        }
    }
}