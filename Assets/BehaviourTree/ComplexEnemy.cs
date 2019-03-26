
using UnityEngine;
using IMBT;

public class ComplexEnemy : MonoBehaviour {
    [SerializeField] private BlackBoard blackBoard = null;
    public BlackBoard BlackBoard { get { return blackBoard; } }

    [SerializeField] private WaypointCollection patrolPath = null;
    [SerializeField] private Transform target = null;

    private BTSelector BT;

    private void Awake() {
        blackBoard.SetValue("FOV", GetComponent<EnemyFOV>());
        blackBoard.SetValue("Patrol Path", patrolPath);
        blackBoard.SetValue("Target", target);
        blackBoard.SetValue("Path", patrolPath);
        blackBoard.SetValue("State", BTState.Patrol);
        blackBoard.SetValue("Agent", gameObject);
        blackBoard.SetValue("Speed", blackBoard.Settings.WalkSpeed);
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
                             new LookAtTarget(),
                             new BTSequence(
                                 new BTTimer(3f),
                                 new BTBreak()
                                 )
                             ),
                         new BTSequence(
                             new TargetInVisibleRange(),
                             new ShootPlayer()
                             ),
                         new BTSequence(
                             new BTTimer(1f),
                             new DoAYell()
                             ),
                         new BTSequence(
                            new BTTimer(3f),
                            new BTLog("LALALALLA"),
                            new ChaseTheTarget(this),
                            new SetCurrentState(BTState.Patrol)
                            )
                         )
                     ),
                 new BTSequence(
                     new IsInState(BTState.GroupInspect),
                     new BTSelector(
                         new BTSequence(
                             new WasInGroupInspect(),
                             new ChaseTheTarget(this),
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
        Debug.Log(blackBoard.GetValue<BTState>("State"));
    }
}