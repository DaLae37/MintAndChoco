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
    Image ig;

    public PlayerController player;

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
        mouseHp = GameObject.Find("MouseHpPos").transform;
        catHp = GameObject.Find("CatHpPos").transform;

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
        ig = GetComponentsInChildren<Image>()[3];
    }
}
