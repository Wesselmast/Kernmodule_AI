﻿
using UnityEngine;
using IMBT;

public class ComplexEnemy : MonoBehaviour {
    [SerializeField] private BlackBoard blackBoard;
    private BTSelector BT;

    void Start() {
        BT =
            new BTSelector(
                new BTSequence(
                    new TargetInShootableRange(),
                    new BTLog("PEW!")
                    ),
                new BTSequence(
                    new TargetInVisibleRange(),
                    new BTSequence(
                        new BTTimer(.5f, false),
                        new BTLog("Inspect"),
                        new BTSelector(
                            /*new BTSequence(
                                new BTTimer(.5f, false),
                                new BTLog("Group-Inspect"),
                                new BTSelector(
                                    new BTTimer(.5f),
                                    new BTLog("Combat")
                                    )
                                ),*/
                            new BTSelector( //inspect\
                                    new BTSequence(
                                        new CheckIfHasInspected(),
                                        new MoveBackToOldSpot(),
                                        new BTLog("Moved Back")
                                        ),
                                    new BTSequence(
                                        new SaveOldSpot(),
                                        new InspectTransform(blackBoard.target),
                                        new BTLog("Look Around")
                                        )
                                    )
                                )
                            )
                        ),
                        new BTSelector( //Passive state, seems to be consistently working
                            new BTSequence(
                                new BTTimer(2f),
                                new BTLog("Talk To A Friend")
                                ),
                            new BTLog("Patrolling")
                        )
            );
    }

    private void Update() {
        BT.Tick(blackBoard);
    }

    private void OnDrawGizmos() {
        Gizmos.DrawCube(blackBoard.OldSpot, Vector3.one * 2);
    }
}
