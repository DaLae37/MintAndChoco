using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameManager : MonoBehaviour {
    public static InGameManager instance;
    private void Awake()
    {
        instance = this;
    }
}
