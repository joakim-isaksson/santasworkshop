using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomTexturizer : MonoBehaviour {

	public Texture[] RandomTextures;

	// Use this for initialization
	void Start () {
		Texture randomTexture = RandomTextures[Random.Range(0, RandomTextures.Length)];

		GetComponent<MeshRenderer>().material.mainTexture = randomTexture;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
