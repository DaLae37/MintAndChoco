using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDataManager : MonoBehaviour {

    public static PlayerDataManager instance;

    public User my;

    public void Awake()
    {
        if (instance != null)
            Destroy(this.gameObject);

        else
        {
            DontDestroyOnLoad(this.gameObject);
            instance = this;
        }
    }




}
