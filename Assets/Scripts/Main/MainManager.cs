using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class MainManager : MonoBehaviour {
    public static MainManager instance;
    public bool isMatching;
    public Button backScene;
    public Button matchingStart;
    public Image loadingAni;
    public Text loadingText;
    private void Start()
    {
        instance = this;
        isMatching = false;
    }
    private void Update()
    {
        if (!isMatching)
        {
            if(Application.platform == RuntimePlatform.Android)
            {
                if (Input.GetKey(KeyCode.Escape))
                {
                    mainScene();
                }
            }
        }
        else
        {
            backScene.gameObject.SetActive(false);
            matchingStart.gameObject.SetActive(false);
            loadingAni.gameObject.SetActive(true);
            loadingText.gameObject.SetActive(true);
        }
    }
    public void mainScene()
    {
        Destroy(GameObject.Find("GameObject").gameObject);
        SceneManager.LoadScene("MainScene");       
    }
    public void matchStart()
    {
        NetworkManager.instance.EmitMatch();
    }   
}
