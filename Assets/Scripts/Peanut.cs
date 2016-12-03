using UnityEngine;
using System.Collections;

public class Peanut : MonoBehaviour
{

	void Start()
    {
	    
	}
	
	void Update()
    {
	    
	}

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag != "MovableCube" && 
            other.gameObject.tag != "Peanut" &&
            other.gameObject.tag != "Present")
        {
            Debug.Log(other.gameObject.tag);
            Destroy(gameObject, 3f);
        }
    }
}
