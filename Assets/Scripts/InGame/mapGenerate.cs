using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mapGenerate : MonoBehaviour {
    public GameObject[] tiles = new GameObject[4];
	// Use this for initialization
	void Start () {
		for(int i=0; i<20; i++)
        {
            for (int j = 0; j < 20; j++)
            {
                GameObject m = Instantiate(tiles[(int)Random.Range(0, 4)]);
                m.transform.position = new Vector3(15 + i * 1.5f,-1.5f, -15+ j * 1.5f);
                m.transform.SetParent(GameObject.Find("Maps").transform);
            }
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
