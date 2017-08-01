using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class ProfileCreateSceneManager : MonoBehaviour {
    public Canvas []can = new Canvas[3];
    public GameObject popUp;
    public GameObject index2popUp;
    public InputField inputField;
    public Text ErrorMessage;
    int index = 0;
    int isCat;
    // Use this for initialization
    void Start () {
        if (PlayerPrefs.HasKey("isCat"))
        {
            can[0].gameObject.SetActive(false);
            can[1].gameObject.SetActive(false);
            can[2].gameObject.SetActive(true);
            index = 2;
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void ArYes()
    {
        SceneManager.LoadScene("ARScene");
    }
    public void ArNo()
    {
        can[index++].gameObject.SetActive(false);
    }
    public void Cat()
    {
        isCat = 1;
        popUp.SetActive(true);
    }
    public void Mouse()
    {
        isCat = 0;
        popUp.SetActive(true);
    }
    public void index1PopUpYes()
    {
        PlayerPrefs.SetInt("isCat", isCat);
        can[index++].gameObject.SetActive(false);
        can[index].gameObject.SetActive(true);
    }
    public void index1PopUpNo()
    {
        popUp.SetActive(false);
    }
    public void index2Okay()
    {
        if(inputField.text.Length == 0)
        {
            ErrorMessage.gameObject.SetActive(true);
            return;
        }
        index2popUp.gameObject.SetActive(true);
    }
    public void index2PopUpYes()
    {
        PlayerPrefs.SetString("name", inputField.text);
        SceneManager.LoadScene("MainScene");
    }
    public void index2PopUpNo()
    {
        index2popUp.gameObject.SetActive(false);
    }
}
