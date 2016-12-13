using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using NewtonVR;

public class GameFlow : MonoBehaviour
{
    public GameObject[] Pipes;
	[HideInInspector]
	public GameObject[] Cubes;

	[HideInInspector]
	public List<GameObject> ActiveCubes;

	public NVRHand LeftHand;
	public NVRHand RightHand;

	void Awake()
	{
        Cubes = new GameObject[2];

		StartCoroutine(StartGame());
	}

	IEnumerator StartGame()
	{
		yield return new WaitForSeconds(2);

		Cubes[0] = Pipes[0].GetComponent<Pipe>().SpawnCube(LeftHand);
		Cubes[1] = Pipes[1].GetComponent<Pipe>().SpawnCube(RightHand);
	}
	
	void Update()
	{
		
	}

}
