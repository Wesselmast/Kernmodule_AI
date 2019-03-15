using System.Collections.Generic;
using UnityEngine;

public abstract class ObjectPool<T> : MonoBehaviour where T : Component {
    [SerializeField] private T prefab;
    [SerializeField] private int maxAmt;

    public static ObjectPool<T> Instance { get; private set; }
    private Queue<T> objects = new Queue<T>();

    private void Awake() {
        Instance = this;
        for (int i = 0; i < maxAmt; i++) {
            AddObjects(prefab);
        }
    }

    public T Get() {
        T obj = objects.Dequeue();
        obj.gameObject.SetActive(true);
        return obj;
    }

    public void ReturnToPool(T returnable) {
        returnable.gameObject.SetActive(false);
        objects.Enqueue(returnable);
    }

    private void AddObjects(T prefab) {
        T obj = Instantiate(prefab);
        obj.transform.parent = transform;
        obj.gameObject.SetActive(false);
        objects.Enqueue(obj);
    }
}