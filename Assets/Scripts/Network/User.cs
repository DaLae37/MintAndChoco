using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class User {

    public string name;
    public bool isCat;
    public int hp;
    public PlayerController controller;
    void start()
    {
        name = PlayerPrefs.GetString("name");
        if (PlayerPrefs.GetInt("isCat") == 0)
        {
            isCat = false;
        }
        else
        {
            isCat = true;
        }
    }
    public User() {
        hp = 100;
    }

    public User(string _name) {
        name = _name;
        hp = 100;
    }

    public User(string _name, bool _isCat)
    {
        name = _name;
        isCat = _isCat;
        hp = 100;
    }

    public User(string _name, bool _isCat, int _hp)
    {
        name = _name;
        isCat = _isCat;
        hp = _hp;
    }
}
