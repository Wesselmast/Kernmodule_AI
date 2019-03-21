
using UnityEngine;
using IMBT;

public class ComplexEnemy : MonoBehaviour {
    public BlackBoard blackBoard;
    [SerializeField] private WaypointCollection patrolPath;
    [SerializeField] private Transform target;
    private BTSelector BT;

    private void Awake() {
        blackBoard.IsPatrolling = false;
        blackBoard.WasInGroupInspect = false;
        blackBoard.IsInspecting = false;
        blackBoard.IsTakingCover = false;
        blackBoard.Agent = gameObject;
        blackBoard.Fov = GetComponent<EnemyFOV>();
        blackBoard.PatrolPath = patrolPath;
        blackBoard.Target = target;
        blackBoard.State = BTState.Patrol;
        blackBoard.Speed = blackBoard.Settings.WalkSpeed;
    }

    private void Start() {
        BT =
            new BTSelector(
                new BTSequence(
                    new TargetInVisibleRange(),
                    new BTSequence(
                        new BTTimer(.5f, false),
                        new SetCurrentState(BTState.Inspect),
                        new BTSequence(
                            new BTTimer(.5f, false),
                            new SetCurrentState(BTState.GroupInspect),
                            new BTSequence(
                                new BTTimer(.5f, false),
                                new SetCurrentState(BTState.Combat),
                                new BTBreak()
                                )
                            )
                        )
                    ),
                 new BTSequence(
                     new IsInState(BTState.Combat),
                     new BTSelector(
                         new BTSequence(
                             new GetNearestCoverPoint(),
                             new TakeCover(this),
                             new BTLog("Now In Cover"),
                             new BTSequence(
                                new BTTimer(1f),
                                new DoAYell()
                                )
                             ),
                         new ChaseTheTarget(this)
                         )
                     ),
                 new BTSequence(
                     new IsInState(BTState.GroupInspect),
                     new BTSelector(
                         new BTSequence(
                             new WasInGroupInspect(),
                             new SetCurrentState(BTState.Patrol)
                             ),
                        new BTSequence(
                            new BTTimer(1f),
                            new DoAYell()
                            ),
                         new BTSequence(
                             new SaveOldSpot(),
                             new GroupInspectTarget(this),
                             new LookAround(),
                             new MoveBackToOldSpot(this),
                             new SetCurrentState(BTState.Patrol)
                             )
                         )
                     ),
                 new BTSequence(
                    new IsInState(BTState.Inspect),
                    new SaveOldSpot(),
                    new InspectTarget(this),
                    new BTSelector(
                        new BTSequence(
                            new WasInGroupInspect(),
                            new SetCurrentState(BTState.Patrol)
                            ),
                        new BTSequence(
                            new LookAround(),
                            new MoveBackToOldSpot(this),
                            new SetCurrentState(BTState.Patrol)
                            )
                        )
                    ),
                 new BTSequence(
                    new IsInState(BTState.Patrol),
                    new BTSelector(
                        new BTSequence(
                            new WasInGroupInspect(),
                            new BTSelector(
                                new BTSequence(
                                    new BTTimer(1f),
                                    new DoAYell()
                                    ),
                                new DoAPatrol(this)
                                )
                            ),
                        new BTSelector(
                            new BTSequence(
                                new BTTimer(2f),
                                new BTLog("Talk To A Friend")
                                ),
                            new DoAPatrol(this)
                            )
                        )
                    )
                );
    }

    private void Update() {
        BT.Tick(blackBoard);
        Debug.Log(blackBoard.State);
    }

    private void OnDrawGizmos() {
        if (blackBoard.Path != null) {
            for (int i = 0; i < blackBoard.Path.Length; i++) {
                Gizmos.color = Color.cyan;
                try { Gizmos.DrawLine(blackBoard.Path[i - 1], blackBoard.Path[i]); }
                catch { Gizmos.DrawLine(transform.position, blackBoard.Path[i]); }
                Gizmos.DrawCube(blackBoard.Path[i], Vector3.one);
            }
        }
    }
}
