using UnityEngine;
using IMBT;

public class SearcherEnemy : Enemy {
    [SerializeField] private WaypointCollection patrolPath = null;

    private void Awake() {
        blackBoard.SetValue("FOV", GetComponent<EnemyFOV>());
        blackBoard.SetValue("Patrol Path", patrolPath);
        blackBoard.SetValue("Target", target);
        blackBoard.SetValue("Path", patrolPath.GetWaypoints());
        blackBoard.SetValue("State", BTState.Patrol);
        blackBoard.SetValue("Agent", gameObject);
        blackBoard.SetValue("Speed", blackBoard.Settings.WalkSpeed);
        blackBoard.SetValue("Patrol Index", 0);
        blackBoard.SetValue("CurrentPathEnumeration", default(System.Collections.IEnumerator));
    }

    private void Start() {
        BT = new BTSelector(
                new BTSequence(
                    new TargetInVisibleRange(),
                    new BTSequence(
                        new BTTimer(.5f, false),
                        new PlaySound(blackBoard.Settings.ChargeSound, gameObject),
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
                           new BTTimer(7f),
                           new SetCurrentState(BTState.GroupInspect)
                           ),
                        new BTSequence(
                            new BTTimer(1f),
                            new DoAYell()
                            ),
                        new BTSequence(
                            new TargetInVisibleRange(),
                            new ShootPlayer(),
                            new BTTimer(1.5f),
                            new BTBreak()
                            ),
                        new BTSequence(
                             new GetNearestCoverPoint(),
                             new TakeCover(this)
                            )
                        )
                     ),
                 new BTSequence(
                     new IsInState(BTState.GroupInspect),
                     new BTSelector(
                        new BTSequence(
                            new CheckBool("WasInGroupInspect"),
                            new SetCurrentState(BTState.Patrol)
                            ),
                        new BTSequence(
                            new BTTimer(1f),
                            new DoAYell()
                            ),
                        new BTSelector(
                            new BTSequence(
                                new CheckBool("MovingTowardsDestination", false),
                                new SetValue<float>("Speed", blackBoard.Settings.RunSpeed),
                                new SaveOldSpot(),
                                new InspectTarget(this),
                                new SetValue<bool>("MovingTowardsDestination", true)
                                ),
                            new BTSequence(
                                new LookAround(),
                                new MoveBackToOldSpot(this),
                                new SetValue<bool>("WasInGroupInspect", true)
                                )
                            )
                        )
                    ),
                 new BTSequence(
                    new IsInState(BTState.Inspect),
                    new SaveOldSpot(),
                    new InspectTarget(this),
                    new BTSelector(
                        new BTSequence(
                            new CheckBool("WasInGroupInspect"),
                            new SetCurrentState(BTState.Patrol)
                            ),
                        new BTSequence(
                            new LookAround(),
                            new MoveBackToOldSpot(this),
                            new SetCurrentState(BTState.Patrol)
                            )
                        )
                    ),
                new BTSelector(
                    new BTSequence(
                        new CheckBool("WasInGroupInspect"),
                        new BTSelector(
                            new BTSequence(
                                new BTTimer(1f),
                                new DoAYell()
                                ),
                            new DoAPatrol(this)
                            )
                        ),
                    new BTSequence(
                        new CheckBool("MoveTowardsAlly"),
                        new BTSelector(
                            new BTSequence(
                                new CheckBool("MovingTowardsDestination", false),
                                new SaveOldSpot(),
                                new MoveTowardsAlly(this),
                                new SetValue<bool>("MovingTowardsDestination", true)
                                ),
                            new BTSequence(
                                new DoAPray(2f), //would be DoATalk if I had dialogue and stuff!
                                new MoveBackToOldSpot(this),
                                new SetValue<bool>("MoveTowardsAlly", false)
                                )
                            )
                        ),
                    new BTSequence(
                        new CheckBool("MoveTowardsShrine"),
                        new BTSelector(
                            new BTSequence(
                                new CheckBool("MovingTowardsDestination", false),
                                new SaveOldSpot(),
                                new MoveTowardsShrine(this),
                                new SetValue<bool>("MovingTowardsDestination", true)
                                ),
                            new BTSequence(
                                new DoAPray(3f),
                                new MoveBackToOldSpot(this),
                                new SetValue<bool>("MoveTowardsShrine", false)
                                )
                            )
                        ),
                    new BTSequence(
                        new BTTimer(Random.Range(25f, 50f)),
                        new FindNearestShrine(),
                        new SetValue<bool>("MoveTowardsShrine", true)
                        ),
                    new BTSequence(
                        new BTTimer(Random.Range(20f, 50f)),
                        new GetNearestAlly(),
                        new SetValue<bool>("MoveTowardsAlly", true)
                        ),
                    new DoAPatrol(this)
                    )
            );
    }

    private void Update() {
        BT.Tick(blackBoard);
    }

    //private void OnDrawGizmos() {
    //    if (Application.isPlaying) {
    //        Vector3[] path = blackBoard.GetValue<Vector3[]>("Path");
    //        if (path == null) return;
    //        Gizmos.color = Color.blue;
    //        for (int i = 0; i < path.Length - 1; i++) {
    //            Gizmos.DrawLine(path[i], path[i + 1]);
    //            Gizmos.DrawCube(path[i], Vector3.one);
    //        }
    //        Gizmos.DrawCube(path[path.Length - 1], Vector3.one);
    //    }
    //}
}