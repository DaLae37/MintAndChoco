using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mapGenerate : MonoBehaviour {
    public GameObject[] tiles = new GameObject[4];
    public GameObject tree;
    public GameObject rock;
    // Use this for initialization
    void Start () {
		for(int i=0; i<30; i++)
        {
            for (int j = 0; j < 30; j++)
            {
                GameObject m = Instantiate(tiles[(int)Random.Range(0, 4)]);
                m.transform.position = new Vector3(-22.5f + i * 1.5f,-1.5f, -22.5f+ j * 1.5f);
                m.transform.SetParent(GameObject.Find("Maps").transform);
            }
        }
        int rockSum = Random.Range(0, 4);
        int treeSum = Random.Range(0, 4);
        for(int i=0; i<rockSum; i++)
        {
            GameObject r = Instantiate(rock);
            r.transform.position = new Vector3(Random.Range(-17, 17), 0, Random.Range(-10, 10));
        }
        for(int i=0; i<treeSum; i++)
        {
            GameObject t = Instantiate(tree);
            t.transform.position = new Vector3(Random.Range(-17, 17), 0, Random.Range(-10, 10));
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
