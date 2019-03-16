using UnityEngine;

namespace IMBT {
    public class BTLog : BTNodeBase {
        private readonly string logString;

        public BTLog(string logString) {
            this.logString = logString;
        }

        public override TaskStatus Tick(BlackBoard bb) {
            Debug.Log(logString);
            return TaskStatus.Success;
        }
    }
}