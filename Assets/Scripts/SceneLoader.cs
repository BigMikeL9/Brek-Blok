using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{

    private GameSession gameStatusScript;

    // Start is called before the first frame update
    void Start()
    {
        gameStatusScript = FindObjectOfType<GameSession>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadNextScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex; // Gets us the current scene Index integer

        SceneManager.LoadScene(currentSceneIndex + 1); // Loads the next scene
    }

    public void LoadStartScene()
    {
        SceneManager.LoadScene(0);
        gameStatusScript.ResetGame();
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
