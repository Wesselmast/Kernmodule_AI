using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(EnemyFOV))]
public class FOVEditor : Editor {
    private void OnSceneGUI() {
        EnemyFOV fov = (EnemyFOV)target;
        Handles.color = Color.yellow;
        Handles.DrawWireArc(fov.transform.position, Vector3.up, Vector3.forward, 360, fov.CommunicationRadius);
        Handles.color = Color.blue;
        Handles.DrawWireArc(fov.transform.position, Vector3.up, Vector3.forward, 360, fov.CoverPointSearchRadius);
        Handles.color = Color.white;
        Handles.DrawWireArc(fov.transform.position, Vector3.up, Vector3.forward, 360, fov.ViewRadius);
        Handles.color = Color.red;
        Handles.DrawWireArc(fov.transform.position, Vector3.up, Vector3.forward, 360, fov.ShootRadius);
        Vector3 viewAngleA = fov.DirFromAngle(-fov.ViewAngle / 2);
        Vector3 viewAngleB = fov.DirFromAngle(fov.ViewAngle / 2);
        float biggestRadius = fov.ViewRadius >= fov.ShootRadius ? fov.ViewRadius : fov.ShootRadius;
        Handles.color = Color.green;
        Handles.DrawDottedLine(fov.transform.position, fov.transform.position + viewAngleA * biggestRadius, 5f);
        Handles.DrawDottedLine(fov.transform.position, fov.transform.position + viewAngleB * biggestRadius, 5f);
        Handles.ArrowHandleCap(
            0, 
            fov.transform.position, 
            fov.transform.rotation * Quaternion.LookRotation(Vector3.forward),
            fov.ViewRadius / 5f, 
            EventType.Repaint
        );
    }
}