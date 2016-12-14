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
		yield return new WaitForSeconds(3);

		Cubes[0] = Pipes[0].GetComponent<Pipe>().SpawnCube(LeftHand);

		// Spawn both cubes if in debug mode
		if (GameObject.Find("GameManager").GetComponent<GameManager>().DebugMode == false)
		{
			yield return new WaitForSeconds(3);
		}

		Cubes[1] = Pipes[1].GetComponent<Pipe>().SpawnCube(RightHand);
	}
	
	void Update()
	{
		
	}

	/// <summary>
	/// Spawns and stores a new cube on the given side.
	/// </summary>
	/// <param name="side"></param>
	public void SpawnCube(CubeSide side)
	{
		if (side == CubeSide.Left)
		{
			Cubes[0] = Pipes[0].GetComponent<Pipe>().SpawnCube(LeftHand);
		}
		else
		{
			Cubes[1] = Pipes[1].GetComponent<Pipe>().SpawnCube(RightHand);
		}
	}

}
