using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class StorySceneManager : MonoBehaviour {

    public GameObject[] storyBoard = new GameObject[10];
    public int index = 0;
	// Use this for initialization
	void Start () {
        if (PlayerPrefs.HasKey("name") && !PlayerPrefs.HasKey("isCat") || !PlayerPrefs.HasKey("name") && PlayerPrefs.HasKey("isCat"))
        {
            profileCreate();
        }
    }
	void StoryBoardCheck()
    {
        for(int i=0; i<9; i++)
        {
            if (index == i)
                storyBoard[i].gameObject.SetActive(true);
            else
                storyBoard[i].gameObject.SetActive(false);
        }
    }
	// Update is called once per frame
	void Update () {
	    if(Application.platform == RuntimePlatform.Android)
        {
            if (Input.GetKey(KeyCode.Escape))
            {
                Skip();
            }
        }
        StoryBoardCheck();
	}
    void mainScene()
    {
        SceneManager.LoadScene("MainScene");
    }
    public void Skip()
    {
        if (!PlayerPrefs.HasKey("name") || !PlayerPrefs.HasKey("isCat"))
        {
            PlayerPrefs.SetInt("total", 0);
            PlayerPrefs.SetInt("win", 0);
            profileCreate();
        }
        else
        {
            mainScene();
        }
    }
    public void profileCreate()
    {
        SceneManager.LoadScene("ProfileCreateScene");
    }
    public void next()
    {
        index++;
        if(index >= 10)
        {
            index = 0;
        }
    }
    public void before()
    {
        index--;
        if(index < 0)
        {
            index = 9;
        }
    }
}
