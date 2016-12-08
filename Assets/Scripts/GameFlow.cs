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

		Cubes[0] = Pipes[0].GetComponent<Pipe>().SpawnCube(LeftHand, true);
		Cubes[1] = Pipes[1].GetComponent<Pipe>().SpawnCube(RightHand, false);
	}
	
	void Update()
	{
		
	}

    /// <summary>
    /// Register the given cube for the game flow object.
    /// </summary>
    /// <param name="cube"></param>
    public void RegisterCube(GameObject cube)
    {
        foreach (GameObject activeCube in ActiveCubes)
        {
            if (activeCube.GetComponent<MovableCube>().Side == cube.GetComponent<MovableCube>().Side)
            {
                Debug.Log("Cube on an existing side was spawned. BIG NO-NO");
            }
        }

        ActiveCubes.Add(cube);
    }
}
