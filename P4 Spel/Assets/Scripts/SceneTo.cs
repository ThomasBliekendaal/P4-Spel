using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTo : MonoBehaviour {
    public string sceneName;

    public void OnButtonPress()
    {
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
    }
}
