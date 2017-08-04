using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDHp : MonoBehaviour {
    bool isCat;
    Transform mouseHp;
    Transform catHp;
    RectTransform rt;
    Transform HUDTransform;
    public Image ig;

    PlayerController player;

    private void Start()
    {
        rt = gameObject.GetComponent<RectTransform>();
    }

    private void Update()
    {
        if (HUDTransform != null && ig != null && player != null)
        {
            rt.position = Camera.main.WorldToScreenPoint(HUDTransform.position);
            ig.fillAmount = player.hp / 100f;
        }
        else if(GameObject.Find("MouseHpPos")!= null && GameObject.Find("CatHpPos")!= null)
            setTransform();
    }

    void setTransform()
    {
        mouseHp = GameObject.Find("MouseHpPos").GetComponent<Transform>();
        catHp = GameObject.Find("CatHpPos").GetComponent<Transform>();
        for(int i=0; i<2; i++)
        {
            if(NetworkManager.instance.userList[i].name != PlayerDataManager.instance.my.name)
                player = NetworkManager.instance.FindUserController(NetworkManager.instance.userList[i].name);
        }
        if (PlayerDataManager.instance.my.isCat)
        {
            HUDTransform = mouseHp;
            isCat = false;
        }
        else
        {
            HUDTransform = catHp;
            isCat = true;
        }
    }
}
