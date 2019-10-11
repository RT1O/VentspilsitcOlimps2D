using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class MenuButton : MonoBehaviour
{
    public string scene;

    public void ChangeScene() {
        SceneManager.LoadScene(scene);
    }

    public void Continue() {
        // ...
    }

    public void Play() {
        SceneManager.LoadScene("Test_Juris");
    }

    public void Exit() {
        Application.Quit();
    }
}
