using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class myHp : MonoBehaviour
{
    public Image ig;

    public PlayerController player;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
            ig.fillAmount = player.hp / 100f;
        else if (GameObject.Find(PlayerDataManager.instance.my.name) != null)
            player = GameObject.Find(PlayerDataManager.instance.my.name).GetComponent<PlayerController>();
    }
}