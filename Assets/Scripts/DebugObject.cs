using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugObject : MonoBehaviour {

	void Update () {
        if (GameManager.instance.DebugMode) gameObject.SetActive(true);
        else gameObject.SetActive(false);
    }
}
