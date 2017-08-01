using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class ProfileSceneManager : MonoBehaviour {
    public Image cat;
    public Image mouse;
    public Text winText;
    public Text loseText;
    public Text nameText;
    public GameObject popUp;
    int total;
    int win;
    string name;
	// Use this for initialization
	void Start () {
        win = PlayerPrefs.GetInt("win");
        total = PlayerPrefs.GetInt("total");
        name = PlayerPrefs.GetString("name");
        if (PlayerPrefs.GetInt("isCat") == 1)
        {
            cat.gameObject.SetActive(true);
        }
        else
        {
            mouse.gameObject.SetActive(true);
        }
        winText.text = "승리 : " + win;
        loseText.text = "패배 : " + (total - win);
        nameText.text = "별명 : " + name;

    }
	
	// Update is called once per frame
	void Update () {
		if(Application.platform == RuntimePlatform.Android)
        {
            if (Input.GetKey(KeyCode.Escape))
            {
                mainScene();
            }
        }
	}
    public void removeClick()
    {
        popUp.SetActive(true);
    }
    public void removeYes()
    {
        PlayerPrefs.DeleteAll();
        mainScene();
    }
    public void removeNo()
    {
        popUp.SetActive(false);
    }
    public void mainScene()
    {
        SceneManager.LoadScene("MainScene");
    }
}
