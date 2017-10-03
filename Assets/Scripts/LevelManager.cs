using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {

    public void LoadLevel(string name)
    {
        
        Debug.Log("Level load requested for: " + name);
        SceneManager.LoadScene(name);
    }

    public void QuitLevel()
    {
        Debug.Log("Request to quit the game");
        Application.Quit();
    }

    public void returnToScene(string name)
    {
        SceneManager.LoadScene(name);
       
    }

    public void LoadNextLevel()
    {
       
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

   
    

}
