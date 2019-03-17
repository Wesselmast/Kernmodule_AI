using UnityEngine;
using UnityEngine.AI;

[System.Serializable]
public class BlackBoard {
    public Transform target;
    public GameObject agent;
    public NavMeshAgent navAgent;
    public float moveSpeed = 3f;
    public float sightRange = 10f;
    public EnemyFOV fov;
    [HideInInspector] public Vector3 OldSpot;
    [HideInInspector] public bool Inspected = true;
    [HideInInspector] public bool OldSpotSaved = false;
}

/*
More generic:

public class BlackBoard {
	
	private Dictionary<string, object> variables = new Dictionary<string, object>();
	
	public T GetValue<T>(string name) {
		
		if(variables.ContainsKey(name)) {
			return (T)variables[name];
		}
		return default(T);
	}
	
	public void SetValue<T> (string name, T value) {
		if(variables.ContainsKey(name)) {
			variables[name] = value;
		}
        else {
			variables.Add(name, value);
		}
	}
}
*/
