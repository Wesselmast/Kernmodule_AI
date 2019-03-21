﻿using UnityEngine;
using System.Collections;

namespace IMBT {
    public class MoveBackToOldSpot : BTNode {
        private const float approachRange = .5f;
        private bool doneCalculation = false;
        private readonly MonoBehaviour monoBehaviour;

        public MoveBackToOldSpot(MonoBehaviour monoBehaviour) {
            this.monoBehaviour = monoBehaviour;
        }

        public override BTTaskStatus Tick(BlackBoard bb) {
            if (!bb.IsMovingBack) {
                doneCalculation = false;
                bb.IsPatrolling = false;
                bb.IsMovingBack = true;
                PathRequestManager.RequestPath(new PathRequest(bb.Agent.transform.position, bb.OldSpot,
                    (Vector3[] newPath, bool success) => {
                        if (success) {
                            bb.Path = newPath;
                            monoBehaviour.StopAllCoroutines();
                            monoBehaviour.StartCoroutine(DoPath(bb));
                        }
                    }));
            }
            if (doneCalculation) {
                bb.OldSpotSaved = false;
                bb.IsInspecting = false;
                if(bb.State == BTState.GroupInspect) bb.WasInGroupInspect = true;
                return BTTaskStatus.Success;
            }
            return BTTaskStatus.Running;
        }

        private IEnumerator DoPath(BlackBoard bb) {
            Vector3 currentWp = bb.Path[0];
            int index = 0;
            while (true) {
                if (Vector3.Distance(bb.Agent.transform.position, currentWp) < approachRange) {
                    index++;
                    if (index >= bb.Path.Length) {
                        doneCalculation = true;
                        yield break;
                    }
                    currentWp = bb.Path[index];
                }
                bb.Agent.transform.position += (currentWp - bb.Agent.transform.position).normalized * bb.Speed * Time.deltaTime;
                Quaternion lookRot = Quaternion.LookRotation((currentWp - bb.Agent.transform.position).normalized);
                bb.Agent.transform.rotation = Quaternion.Slerp(bb.Agent.transform.rotation, lookRot, Time.deltaTime * bb.Settings.TurnSpeed);
                yield return null;
            }
        }
    }
}