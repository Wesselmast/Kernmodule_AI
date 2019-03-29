using UnityEngine;

namespace IMBT {
    public class LookAround : BTNode {
        private float totalRotation = 360f;

        public override BTTaskStatus Tick(BlackBoard bb) {
            float rotation = Time.deltaTime * bb.Settings.LookAroundSpeed;
            if(totalRotation < 0) {
                totalRotation = 360f;
                return BTTaskStatus.Success;
            }
            totalRotation -= rotation;
            bb.GetValue<GameObject>("Agent").transform.Rotate(new Vector3(0f, rotation, 0f));
            return BTTaskStatus.Running;
        }
    }
}
