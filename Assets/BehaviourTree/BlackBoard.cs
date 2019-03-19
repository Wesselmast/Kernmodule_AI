using UnityEngine;
using IMBT;

[System.Serializable]
public class BlackBoard {
    public BlackBoardSettings Settings;
    [HideInInspector] public Vector3 OldSpot;
    [HideInInspector] public bool Inspected = false;
    [HideInInspector] public bool IsPatrolling = false;
    [HideInInspector] public bool IsMovingBack = false;
    [HideInInspector] public bool IsInspecting = false;
    [HideInInspector] public bool OldSpotSaved = false;
    [HideInInspector] public bool WasInGroupInspect = false;
    [HideInInspector] public Vector3[] Path;
    [HideInInspector] public BTState State = BTState.Patrol;
    [HideInInspector] public GameObject Agent;
    [HideInInspector] public EnemyFOV Fov;
    [HideInInspector] public WaypointCollection PatrolPath;
    [HideInInspector] public Transform Target;
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
