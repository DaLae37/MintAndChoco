using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static GameManager instance;
    public GameObject chocoImage;
    public GameObject mintImage;
    public GameObject mintPrefab;
    public GameObject chocoPrefab;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        NetworkManager.instance.EmitGameLoad();
    }

    public void StartGame() {
        for (int i = 0; i < NetworkManager.instance.userList.Count; i++) {
            GameObject g;
            if (NetworkManager.instance.userList[i].isCat)
            {
                g = Instantiate(chocoPrefab);
                chocoImage.SetActive(true);
            }
            else
            {
                g = Instantiate(mintPrefab);
                mintImage.SetActive(true);
            }
            g.name = NetworkManager.instance.userList[i].name;
            NetworkManager.instance.userList[i].controller = g.GetComponent<PlayerController>();
            if (PlayerDataManager.instance.my.name == NetworkManager.instance.userList[i].name) {
                g.GetComponent<PlayerController>().SetLocal(true);
            }
        }

        NetworkManager.instance.SetAllUserInput(true);
    }


}
