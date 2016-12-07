using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameFlow : MonoBehaviour
{
    public GameObject[] Pipes;

	public GameObject[] Rotators;

	[HideInInspector]
	public List<GameObject> ActiveCubes;

	void Awake()
	{
		//StartCoroutine(StartGame());
	}

	IEnumerator StartGame()
	{
		yield return new WaitForSeconds(2);

		foreach (GameObject pipe in Pipes)
		{
			pipe.GetComponent<Pipe>().SpawnCube();
		}
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
