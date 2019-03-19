
using UnityEngine;
using IMBT;

public class ComplexEnemy : MonoBehaviour {
    [SerializeField] private BlackBoard blackBoard;
    [SerializeField] private WaypointCollection patrolPath;
    [SerializeField] private Transform target;
    private BTSelector BT;

    private void Awake() {
        blackBoard.Inspected = false;
        blackBoard.IsPatrolling = false;
        blackBoard.WasInGroupInspect = false;
        blackBoard.Agent = gameObject;
        blackBoard.Fov = GetComponent<EnemyFOV>();
        blackBoard.PatrolPath = patrolPath;
        blackBoard.Target = target;
        blackBoard.State = BTState.Patrol;
    }

    void Start() {
        BT =
            new BTSelector(
                new BTSelector(
                    new BTSequence(
                        new WasInGroupInspect(),
                        new SetCurrentState(BTState.GroupInspect),
                        new TargetInVisibleRange(),
                        new BTSequence(
                            new BTTimer(2f),
                            new SetCurrentState(BTState.Combat),
                            new BTLog("Enter Combat"),
                            new BTBreak()
                            )
                        ),
                    new BTSequence(
                        new TargetInVisibleRange(),
                        new BTSequence(
                            new BTTimer(.5f, false),
                            new SetCurrentState(BTState.Inspect),
                            new BTSequence(
                                new BTTimer(25f, false),
                                new SetCurrentState(BTState.GroupInspect),
                                new BTBreak() //dispose of this tree
                                )
                            )
                        )
                    ),
                 new BTSequence(
                     new IsInState(BTState.Combat),
                     new BTLog("COMBAT PEW PEW!")
                     ),
                 new BTSequence(
                     new IsInState(BTState.GroupInspect),
                     new BTLog("GroupInspect!"),
                     new BTSequence(
                        new BTTimer(2f),
                        new BTLog("YELL OUT")
                        )
                     ),
                 new BTSequence(
                    new IsInState(BTState.Inspect),
                    new BTLog("Inspecting"),
                    new BTSequence(
                         new SaveOldSpot(),
                         new InspectTarget(this),
                         new BTLog("Look Around"),
                         new SetCurrentState(BTState.Patrol)
                       )
                    ),
                 new BTSequence(
                    new IsInState(BTState.Patrol),
                    new BTSelector(
                        new BTSequence(
                            new CheckIfHasInspected(),
                            new LookAround(),
                            new MoveBackToOldSpot(this)
                            ),
                        new DoAPatrol(this)
                        ),
                    new BTSequence(
                        new BTTimer(2f),
                        new BTLog("Talk To A Friend")
                        )
                    )
                );
    }

    private void Update() {
        BT.Tick(blackBoard);
    }

    private void OnDrawGizmos() {
        if (blackBoard.Path != null) {
            for (int i = 0; i < blackBoard.Path.Length; i++) {
                Gizmos.color = Color.cyan;
                Gizmos.DrawCube(blackBoard.Path[i], Vector3.one);
            }
        }
    }
}
