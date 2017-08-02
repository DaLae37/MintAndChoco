using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class User{
    public string name;
    public bool isCat;
    public int hp;
    public PlayerController controller;
    public User() {
        hp = 100;
    }

    public User(string _name) {
        name = _name;
        isCat = false;
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
