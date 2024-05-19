using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class play : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadSceneAsync(sceneName: "Game");
    }

    public void QuitGame()
    {
    Application.Quit();
    }
}