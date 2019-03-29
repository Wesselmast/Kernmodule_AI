using UnityEngine;

namespace IMBT {
    public class DoAPray : BTNode {
        private readonly float amtOfSeconds;
        private float elapsed = 0;
        private bool done = false;

        public DoAPray(float amtOfSeconds) {
            this.amtOfSeconds = amtOfSeconds;
        }

        public override BTTaskStatus Tick(BlackBoard bb) {
            if ((elapsed < amtOfSeconds) && !done) {
                elapsed = 0;
                done = true;
            }
            if (elapsed > amtOfSeconds) {
                done = false;
                return BTTaskStatus.Success;
            }
            else {
                elapsed += Time.deltaTime;
                return BTTaskStatus.Running;
            }
        }
    }
}