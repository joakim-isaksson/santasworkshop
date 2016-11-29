using UnityEngine;
using System.Collections.Generic;

public class GameFlow : MonoBehaviour
{
    public GameObject[] Pipes;

	public GameObject[] Rotators;
	public GameObject[] PresentPrefabs;

	[HideInInspector]
	public List<GameObject> ActiveCubes;

	void Awake()
	{
        foreach (GameObject pipe in Pipes)
        {

        }
	}
	
	void Update()
	{
		
	}

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
