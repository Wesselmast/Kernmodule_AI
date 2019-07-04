using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System.Linq;

#if (UNITY_EDITOR)
[ExecuteInEditMode]
public class PrefabBrush : MonoBehaviour {
    [SerializeField] private bool enableBrush = false;
    [SerializeField] private float brushSize = 5f;
    [SerializeField] private float minimumDistance = 1f;
    [SerializeField] private Vector2 scale = new Vector2(.5f, 1.5f);
    [SerializeField] private int prefabDensity = 3;
    [SerializeField] [Range(0, 1)] private float maxSlopeRange = .9f;
    [SerializeField] private bool eraserOn = false;
    [SerializeField] private GameObject[] prefabs = null;
    [SerializeField] private bool hideObjects = false;
    [Header("Leave blank if you're painting on terrain")]
    [SerializeField] private GameObject targetObject = null;

    private List<GameObject> meshCollection;
    private Terrain[] terrains = null;
    private bool hideObject = false;

    private void OnEnable() {
        enableBrush = false;
        eraserOn = false;
        terrains = FindObjectsOfType<Terrain>();

        meshCollection = GetComponentsInChildren<Transform>()
            .Select(t => t.gameObject)
            .Where(go => go != gameObject)
            .ToList();
        if (!meshCollection.Any()) {
            meshCollection = new List<GameObject>();
        }
        SceneView.duringSceneGui += OnScene;
        EditorApplication.hierarchyChanged += OnHierarchyChanged;
    }

    private void OnDestroy() {
        SceneView.duringSceneGui -= OnScene;
        EditorApplication.hierarchyChanged -= OnHierarchyChanged;
    }

    private void OnHierarchyChanged() {
        if (FindObjectsOfType<PrefabBrush>()
            .Select(b => b.gameObject)
            .Where(go => go != null && go != gameObject)
            .ToArray()
            .Length > 0) {
            if (EditorUtility.DisplayDialog(
                "WARNING, THIS MIGHT BE A DESTRUCTIVE ACTION",
                "BE CAREFUL! " +
                "You're about to replace the prefab brush, " +
                "meaning all the painted assets will be removed. " +
                "IF YOU DON'T WANT THIS, CLOSE UNITY AND/OR REVERT YOUR CHANGES",
                "DO IT!")) {

                SceneView.duringSceneGui -= OnScene;
                EditorApplication.hierarchyChanged -= OnHierarchyChanged;
                DestroyImmediate(gameObject);
            }
        }
    }

    private void OnScene(SceneView scene) {
        Event e = Event.current;
        int controlID = GUIUtility.GetControlID(FocusType.Keyboard);
        EventType controlType = e.GetTypeForControl(controlID);
        bool hideObjectLast = hideObjects;
        if (hideObject != hideObjectLast) {
            foreach(GameObject obj in meshCollection) {
                obj.SetActive(hideObject);
            }
            hideObject = !hideObject;
        }

        if (controlType == EventType.KeyDown && e.keyCode == KeyCode.B) {
            enableBrush = !enableBrush;
        }

        if (!enableBrush) return;

        HandleUtility.AddDefaultControl(controlID);

        if (controlType == EventType.KeyDown && e.keyCode == KeyCode.E) {
            eraserOn = !eraserOn;
        }
        else if (controlType == EventType.KeyDown && e.keyCode == KeyCode.RightBracket) {
            brushSize += 2;
        }
        else if (controlType == EventType.KeyDown && e.keyCode == KeyCode.LeftBracket && brushSize >= 0) {
            brushSize -= 2;
        }

        Vector3 mousePosition = e.mousePosition;
        float ppp = EditorGUIUtility.pixelsPerPoint;
        mousePosition.y = scene.camera.pixelHeight - mousePosition.y * ppp;
        mousePosition.x *= ppp;

        if (Physics.Raycast(scene.camera.ScreenPointToRay(mousePosition), out RaycastHit hit)) {
            if (eraserOn) Handles.color = new Color(1.0f, .1f, .1f, .3f);
            else Handles.color = new Color(.9f, .9f, .9f, .3f);
            Handles.DrawSolidArc(hit.point, Vector3.up, Vector3.one * 0.001f, 360, brushSize);


            Terrain terrain = null;
            if (targetObject == null) {
                for (int i = 0; i < terrains.Length; i++) {
                    if (terrains[i].gameObject == hit.collider.gameObject) {
                        terrain = terrains[i];
                        break;
                    }
                }
                if (terrain == null) return;
            }
            else if (targetObject != hit.collider.gameObject) return;

            if (e.modifiers != EventModifiers.None) {
                return;
            }

            if (!((e.type == EventType.MouseDown || e.type == EventType.MouseDrag) && e.button == 0)) {
                if (e.type != EventType.Layout && e.type != EventType.Repaint) e.Use();
                return;
            }

            UnityEditor.SceneManagement.EditorSceneManager.MarkSceneDirty(UnityEngine.SceneManagement.SceneManager.GetActiveScene());

            if (eraserOn) {
                foreach (GameObject go in meshCollection.ToArray()) {
                    if (Vector3.Distance(go.transform.position, hit.point) <= brushSize) {
                        meshCollection.Remove(go);
                        DestroyImmediate(go);
                    }
                }
                return;
            }

            for (int i = 0; i < prefabDensity; i++) {
                int randomPrefab = Random.Range(0, prefabs.Length);

                Vector3 randomPosition = hit.point + Random.insideUnitSphere * brushSize;

                if (terrain != null) {
                    randomPosition = randomPosition.SetY(terrain.SampleHeight(randomPosition) + prefabs[randomPrefab].transform.position.y);

                    Vector3 terrainSize = terrain.terrainData.size;
                    float posX = -(terrain.GetPosition().x / terrainSize.x);
                    float posZ = -(terrain.GetPosition().z / terrainSize.z);
                    Vector3 normal = terrain.terrainData.GetInterpolatedNormal(posX + (randomPosition.x / terrainSize.x), posZ + (randomPosition.z / terrainSize.z));
                    if (normal.y < maxSlopeRange) {
                        continue;
                    }
                }
                else {
                    randomPosition = randomPosition.SetY(Random.Range(-1,0));
                }

                bool tooClose = false;
                foreach (GameObject go in meshCollection) {
                    if (Vector3.Distance(go.transform.position, randomPosition) < minimumDistance) {
                        tooClose = true;
                    }
                }
                if (tooClose) continue;

                GameObject finalInstance = Instantiate(prefabs[randomPrefab]);
                finalInstance.transform.parent = transform;
                finalInstance.transform.position = randomPosition;
                finalInstance.transform.eulerAngles = finalInstance.transform.eulerAngles.SetY(Random.Range(0, 360));
                finalInstance.transform.localScale *= Random.Range(scale.x, scale.y);
                meshCollection.Add(finalInstance);
            }
        }
        hideObjectLast = hideObject;
    }
}
#endif