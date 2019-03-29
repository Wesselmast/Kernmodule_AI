using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CollisionState))]
public abstract class PlayerBehaviour : MonoBehaviour {
    protected static Rigidbody rb;
    protected static CollisionState collisionState;
    protected static PlayerSettings settings;

    private static Player player;

    protected virtual void Awake() {
        rb = GetComponent<Rigidbody>();
        player = GetComponent<Player>();
        collisionState = GetComponent<CollisionState>();
        Wake();
    }

    protected virtual void Start() {
        settings = player.Settings;
        Begin();
    }

    protected virtual void Update() {
        Tick();
    }

    ///<summary>replaces the Awake() method in EnemyComponents</summary>
    protected virtual void Wake() { }
    ///<summary>replaces the Start() method in EnemyComponents</summary>
    protected virtual void Begin() { }
    ///<summary>replaces the Update() method in EnemyComponents</summary>
    protected virtual void Tick() { }
}