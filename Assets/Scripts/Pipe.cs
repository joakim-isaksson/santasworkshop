using UnityEngine;
using System.Collections;

public class Pipe : MonoBehaviour
{
    public GameObject CubePrefab;
    public GameObject OwnerRotator;
    public CubeSide Side;

    private Transform cubeSpawnPoint;

    void Awake()
    {
        cubeSpawnPoint = transform.Find("BoxSpawnPoint").transform;
    }

    void Start()
    {
        SpawnCube();
	}
	
	void Update()
    {
	
	}

    public void SpawnCube()
    {
        GameObject cube = Instantiate(CubePrefab);
        cube.transform.position = cubeSpawnPoint.position;
        cube.transform.rotation = Random.rotation;

        // Assign the side (left/right) of the cube
        var movableCube = cube.GetComponent<MovableCube>();
        movableCube.Side = Side;
        movableCube.OwnerRotator = OwnerRotator;

        movableCube.Init();
    }
}
