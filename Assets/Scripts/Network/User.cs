using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class User {

    public string name;
    public bool isCat;
    public PlayerController controller;

    public User() {
        name = "UserName";
        isCat = false;
    }

    public User(string _name) {
        name = _name;
        isCat = false;
    }

    public User(string _name, bool _isCat)
    {
        name = _name;
        isCat = _isCat;
    }

}
