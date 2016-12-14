using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

	void Update()
	{
		if (Input.GetKeyDown("r"))
		{
			SceneManager.LoadScene("Main");
		}
	}
}
