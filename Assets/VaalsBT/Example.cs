using UnityEngine;
using VaalsBT;

public class Example : MonoBehaviour {
    [SerializeField] private BlackBoard blackBoard;
    private BTSelector BT;

    void Start() {
        BT =
            new BTSelector(
                new BTSequence(
                    new PlayerInRange(10),
                    new MoveToObject(blackBoard.target.gameObject),
                    new BTLog("pizza")
                    ),
                new BTLog("broodje")
            );
    }

    private void Update() {
        BT.Tick(blackBoard);
    }
}
