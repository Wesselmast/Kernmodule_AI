using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// responsible for handling (un)loading of Unity scenes based
//  on current level and current mind state
public class MindStateSceneLoader : MonoBehaviour {
    private bool isLoading;
    private string currentLevel = "Level1";
    private MindState currentOpenState = MindState.NORMAL;
    private readonly Dictionary<MindState, string> stateToString = new Dictionary<MindState, string>() {
        { MindState.NORMAL, "normal" },
        { MindState.DEPRESSION, "depression" },
        { MindState.MANIC, "manic" },
        { MindState.PSYCHOTIC, "psychotic" }
    };

    public static MindStateSceneLoader Instance { get; private set; }

    private void Awake() {
        if (Instance == null) Instance = this;
        if (Instance != this) Destroy(gameObject);
    }

    public void ChangeState(MindState state) {
        if (isLoading) return;

        IEnumerator loadingFunc = ChangeStateInternal(state);
        StartCoroutine(loadingFunc);
    }

    private IEnumerator ChangeStateInternal(MindState state) {
        isLoading = true;
        string sceneName = currentLevel + "_" + stateToString[state];

        // wait until scene is completely loaded until allowing the loading of a new scene
        AsyncOperation loadScene = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        while (!loadScene.isDone) {
            yield return null;
        }

        // check if the scene exists
        Scene scene = SceneManager.GetSceneByName(sceneName);
        bool sceneExists = scene.buildIndex != -1;
        if (sceneExists) {
            // also wait until old scene is completely unloaded until allowing the loading of a new scene
            AsyncOperation unloadScene = SceneManager.UnloadSceneAsync(currentLevel + "_" + stateToString[currentOpenState]);
            while (!unloadScene.isDone) {
                yield return null;
            }
            currentOpenState = state;
        }

        isLoading = false;
    }

    public void ChangeLevel(string levelName) {
        // unload persistent- and state-specific level scenes
        SceneManager.UnloadSceneAsync(currentLevel + "_" + stateToString[currentOpenState]);
        SceneManager.UnloadSceneAsync(currentLevel + "_persistent");

        // load new persistent- and state-specific level scenes
        currentLevel = levelName;
        SceneManager.LoadSceneAsync(currentLevel + "_" + stateToString[currentOpenState], LoadSceneMode.Additive);
        SceneManager.LoadSceneAsync(currentLevel + "_persistent", LoadSceneMode.Additive);
    }
}