using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ErrorText : MonoBehaviour {
    bool isActive;
    float liveTime;
	// Use this for initialization
	void Start () {
        isActive = false;
        liveTime = 0.0f;
    }
	
	// Update is called once per frame
	void Update () {
        if (gameObject.activeSelf && !isActive)
        {
            isActive = true;
        }
        if (isActive)
        {
            liveTime += Time.deltaTime;
            if(liveTime >= 1)
            {
                gameObject.SetActive(false);
                liveTime = 0.0f;
            }
        }
	}
}
