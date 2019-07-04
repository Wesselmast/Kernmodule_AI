using System.Collections;
using UnityEngine;

namespace IMBT {
    public abstract class BTNode {
        protected bool foundPath = false;

        public abstract BTTaskStatus Tick(BlackBoard bb);

        protected IEnumerator DoPath(BlackBoard bb) {
            Vector3[] path = bb.GetValue<Vector3[]>("Path");
            GameObject agent = bb.GetValue<GameObject>("Agent");
            Vector3 currentWp = path[0];
            int index = 0;
            while (true) {
                if (Vector3.Distance(agent.transform.position, currentWp) < 0.1f) {
                    index++;
                    if (index >= path.Length) {
                        bb.SetValue("CurrentPathEnumeration", default(IEnumerator));
                        yield break;
                    }
                    currentWp = path[index];
                }
                agent.transform.position += (currentWp - agent.transform.position).normalized * bb.GetValue<float>("Speed") * Time.deltaTime;
                Quaternion lookRot = Quaternion.LookRotation((currentWp - agent.transform.position).normalized);
                agent.transform.rotation = Quaternion.Slerp(agent.transform.rotation, lookRot, Time.deltaTime * bb.Settings.TurnSpeed);
                yield return null;
            }
        }
    }
}