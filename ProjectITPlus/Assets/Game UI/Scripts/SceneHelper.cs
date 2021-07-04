using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHelper : MonoBehaviour {
    public static IEnumerator DoLoadMap1 () {
        return instance.LoadScene(Scene1);
    }

    public static IEnumerator UnloadScene () {
        return instance.UnloadCurrentScene();
    }

    private static SceneHelper instance;
    public static string Scene1 => instance.scene1;

    public static bool IsLoaded { get; private set; }

    [SerializeField] string scene1 = "Map 1";

    private void Awake () {
        instance = this;
        SceneManager.sceneLoaded += SceneManager_sceneLoaded;
        SceneManager.sceneUnloaded += SceneManager_sceneUnloaded;
    }
    private void SceneManager_sceneUnloaded (Scene scene) {
        IsLoaded = false;
    }

    private void SceneManager_sceneLoaded (Scene scene, LoadSceneMode arg1) {
        IsLoaded = false;
        StartCoroutine(WaitForLOad());
    }
    private IEnumerator WaitForLOad () {
        yield return null;
        IsLoaded = true;
    }

    private IEnumerator LoadScene (string sceneName) {
        if (SceneManager.sceneCount > 1) {
            var scene = SceneManager.GetSceneAt(1);
            var sceneUnload = SceneManager.UnloadSceneAsync(scene);

            while (!sceneUnload.isDone) {
                yield return null;
            }
        }
        yield return null;

        var sceneload = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        while (!sceneload.isDone) {
            yield return null;
        }
        if (SceneManager.sceneCount > 1)
            SceneManager.SetActiveScene(SceneManager.GetSceneAt(1));
    }

    private IEnumerator UnloadCurrentScene () {
        if (SceneManager.sceneCount > 1) {
            var scene = SceneManager.GetSceneAt(1);
            var sceneUnload = SceneManager.UnloadSceneAsync(scene);

            while (!sceneUnload.isDone) {
                yield return null;
            }
        }
        yield return null;
    }
}
