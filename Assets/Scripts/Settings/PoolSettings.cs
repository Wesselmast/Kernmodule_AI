using UnityEngine;

public abstract class PoolSettings<T> : ScriptableObject where T : Component {
    [SerializeField] private T prefab;
    public T Prefab { get { return prefab; } }
    [SerializeField] private int maxAmt;
    public int MaxAmt { get { return maxAmt; } }
}
