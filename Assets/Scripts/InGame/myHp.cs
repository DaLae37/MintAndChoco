using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class myHp : MonoBehaviour {
    public Image ig;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if(GameObject.Find(PlayerDataManager.instance.my.name)!=null)
        ig.fillAmount = GameObject.Find(PlayerDataManager.instance.my.name).GetComponent<PlayerController>().hp / 100f;
	}
}
