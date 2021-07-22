using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level : MonoBehaviour
{
    public int breakableBlocks; // Serialized for debugging purposes.

    SceneLoader sceneLoaderScript;

    // Start is called before the first frame update
    void Start()
    {
        sceneLoaderScript = FindObjectOfType<SceneLoader>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CountBlocks()
    {
        breakableBlocks++;
    }

    public void BlockDestroyed()
    {
        breakableBlocks--;
        if (breakableBlocks <= 0)
        {
            sceneLoaderScript.LoadNextScene();
        }
        
    }

}
