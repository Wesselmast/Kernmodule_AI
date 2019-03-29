using UnityEngine;
using IMBT;

public abstract class Enemy : MonoBehaviour {
    [SerializeField] protected BlackBoard blackBoard = null;
    public BlackBoard BlackBoard { get { return blackBoard; } }

    [SerializeField] protected Transform target = null;

    protected BTSelector BT;
}
