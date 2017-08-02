using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
    public float speed = 100.0f;
    public const int damage = 25;
    float livedTime = 0.0f;
	// Use this for initialization
	void Start () {
        GetComponent<Rigidbody>().AddForce(transform.forward * speed);
	}
	
	// Update is called once per frame
	void Update () {
        livedTime += Time.deltaTime;
        if(livedTime >= 2)
        {
            gameObject.SetActive(false);
        }
	}
}
