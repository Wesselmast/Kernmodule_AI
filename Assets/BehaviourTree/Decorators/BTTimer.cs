using UnityEngine;

namespace IMBT {
    public class BTTimer : BTNode {
        private readonly float amtOfSeconds;
        private readonly bool reset = true;
        private float elapsed = 0;
        private bool done = false;

        public BTTimer(float amtOfSeconds) {
            this.amtOfSeconds = amtOfSeconds;
        }

        public BTTimer(float amtOfSeconds, bool reset) {
            this.reset = reset;
            this.amtOfSeconds = amtOfSeconds;
        }

        public override BTTaskStatus Tick(BlackBoard bb) {
            if ((reset || elapsed < amtOfSeconds) && !done) {
                elapsed = 0;
                done = true;
            }
            if (elapsed > amtOfSeconds) {
                if (reset) elapsed = 0;
                done = false;
                return BTTaskStatus.Success;
            }
            else {
                elapsed += Time.deltaTime;
                return BTTaskStatus.Failed;
            }
        }
    }
}