using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ARSceneManager : MonoBehaviour {
    public Collider catColl;
    public Collider mouseColl;
    public GameObject popUp;
    bool isCat;
    bool isSelect;
    void Start()
    {
        isCat = false;
        isSelect = false;
    }
    void Update()
    {
        if (!isSelect)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (catColl.Raycast(ray, out hit, 2000.0f))
                {
                    isCat = true;
                    isSelect = true;
                }
                else if (mouseColl.Raycast(ray, out hit, 2000.0f))
                {
                    isCat = false;
                    isSelect = true;
                }
            }
        }
        else
        {
            popUp.SetActive(true);
        }
    }
    public void profileScene()
    {
        SceneManager.LoadScene("ProfileCreateScene");
    }
    public void popUpYes()
    {
        Debug.Log("Hello");
        if (isCat)
        {
            PlayerPrefs.SetInt("isCat", 1);
        }
        else
        {
            PlayerPrefs.SetInt("isCat", 0);
        }
        profileScene();
    }
    public void popUpNo()
    {
        isSelect = false;
        popUp.SetActive(false);
    }
}
