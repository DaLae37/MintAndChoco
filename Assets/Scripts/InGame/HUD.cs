using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUD : MonoBehaviour {
    Transform HUDTransform;
    RectTransform rt;
	// Use this for initialization
	void Start () {
        HUDTransform = GameObject.Find("HUDPos").transform;
        rt = GetComponent<RectTransform>();
	}
	
	// Update is called once per frame
	void Update () {
        //게임 월드 좌표계 --> 캔버스 유아이 화면 좌표계
        rt.position = Camera.main.WorldToScreenPoint(HUDTransform.position);
	}
}
