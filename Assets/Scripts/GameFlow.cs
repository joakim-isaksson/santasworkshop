using UnityEngine;
using System.Collections;

public class GameFlow : MonoBehaviour
{
	public GameObject CubePrefab;

	public GameObject[] Rotators;

	[HideInInspector]
	public GameObject ActiveCube;

	void Awake()
	{
		// Create cubes for the rotators
		// Assign the active cube and begin spawn animation
		foreach (GameObject rotator in Rotators)
		{
			GameObject cube = Instantiate(CubePrefab);
			cube.transform.parent = rotator.transform;
		}
	}
	
	void Update()
	{
		
	}
}
