using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void LoadNextScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex; // current scene
        SceneManager.LoadScene(currentSceneIndex + 1); // load next scene
    }

    public void LoadStartScene()
    {
        FindObjectOfType<GameStatus>().ResetGame(); // reset score
        SceneManager.LoadScene(0); // load main menu
    }

    public void QuitGame()
    {
        Application.Quit(); 
    }

}
