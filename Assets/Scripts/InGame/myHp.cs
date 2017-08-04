using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class myHp : MonoBehaviour
{
    public Image ig;
    PlayerController player;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
            ig.fillAmount = player.hp / 100f;
        else
            set();
    }
    void set()
    {
        for (int i = 0; i < 2; i++)
        {
            if (NetworkManager.instance.userList[i].name == PlayerDataManager.instance.my.name)
                player = NetworkManager.instance.FindUserController(NetworkManager.instance.userList[i].name);
        }
    }
}