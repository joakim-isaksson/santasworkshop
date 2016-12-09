using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static GameManager instance;

    public bool DebugMode;

    void Awake () {
        if (instance != null) Destroy(this);
        else
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
	}
}
