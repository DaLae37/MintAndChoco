using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainSceneManager : MonoBehaviour {
    public GameObject popUp;
    bool isNew;
    private void Start()
    {
        isNew = false;
        if(!PlayerPrefs.HasKey("name") || !PlayerPrefs.HasKey("isCat"))
        {
            PlayerPrefs.SetInt("total", 0);
            PlayerPrefs.SetInt("win", 0);
            isNew = true;
            popUp.gameObject.SetActive(true);
        }
    }
    private void Update()
    {
        if (isNew)
        {
            if (Input.anyKeyDown)
            {
                Story();
            }
        }
    }
    public void StartGame()
    {
        SceneManager.LoadScene("Main");
        NetworkManager.instance.EmitJoin(PlayerPrefs.GetString("name"), PlayerPrefs.GetInt("isCat"));
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
