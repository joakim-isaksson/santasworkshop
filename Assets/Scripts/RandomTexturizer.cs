using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomTexturizer : MonoBehaviour {

	public Texture[] RandomTextures;

	[HideInInspector]
	public int ChosenTexture;

	public GameObject linkedParent;

	// Use this for initialization
	void Awake () {
		ChosenTexture = Random.Range(0, RandomTextures.Length);
        Texture randomTexture = RandomTextures[ChosenTexture];

		GetComponent<MeshRenderer>().material.mainTexture = randomTexture;
	}

	void Start()
	{
		if (linkedParent != null)
		{
			GetComponent<MeshRenderer>().material.mainTexture = RandomTextures[linkedParent.GetComponent<RandomTexturizer>().ChosenTexture];
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
