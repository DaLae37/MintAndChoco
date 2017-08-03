using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class StorySceneManager : MonoBehaviour {
    public static StorySceneManager instance;
    public GameObject[] storyBoard = new GameObject[9];
    public int index = 0;
	// Use this for initialization
	void Start () {
        instance = this;
        if (PlayerPrefs.HasKey("name") && !PlayerPrefs.HasKey("isCat") || !PlayerPrefs.HasKey("name") && PlayerPrefs.HasKey("isCat"))
        {
            profileCreate();
        }
        StoryBoardCheck();
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
        if (index >= 9)
        {
            index = 0;
        }
        StoryBoardCheck();
    }
    public void before()
    {
        index--;
        if (index < 0)
        {
            index = 8;
        }
        StoryBoardCheck();
    }
}
