using UnityEngine;

namespace IMBT {
    public class BTLog : BTNode {
        private readonly string message;

        public BTLog(string message) {
            this.message = message;
        }

        public override BTTaskStatus Tick(BlackBoard bb) {
            Debug.Log(message);
            return BTTaskStatus.Success;
        }
    }
}