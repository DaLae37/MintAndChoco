using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainSceneManager : MonoBehaviour {
    public void StartGame()
    {
        SceneManager.LoadScene("Main");
    }
    public void Profile()
    {
        SceneManager.LoadScene("ProfileScene");
    }
    public void Story()
    {
        SceneManager.LoadScene("StoryScene");
    }
}
