using UnityEngine;
using System.Collections;
using NewtonVR;

public class Pipe : MonoBehaviour
{
    public GameObject CubePrefab;
    public GameObject[] PresentPrefabs;
    public GameObject OwnerRotator;
    public CubeSide Side;

    private Transform cubeSpawnPoint;

    void Awake()
    {
        cubeSpawnPoint = transform.Find("BoxSpawnPoint").transform;
    }

    void Start()
    {

	}
	
	void Update()
    {
	
	}

    /// <summary>
    /// Spawn the cube and pass it a randomly chosen present
    /// </summary>
    public GameObject SpawnCube(NVRHand hand, bool startAnimation)
    {
        GameObject cube = Instantiate(CubePrefab);
        cube.transform.position = cubeSpawnPoint.position;
        cube.transform.rotation = Random.rotation;

        // Assign the side (left/right) of the cube
        var movableCube = cube.GetComponent<MovableCube>();
        movableCube.Side = Side;
        movableCube.OwnerRotator = hand.gameObject;

        movableCube.Init(hand);

        // Spawn a random present
        GameObject randomPresentPrefab = PresentPrefabs[Random.Range(0, PresentPrefabs.Length)];
        GameObject randomPresent = Instantiate(randomPresentPrefab);

        var presentCube = cube.GetComponent<PresentCube>();
        presentCube.AssignPresent(randomPresent);

		return cube;
    }
}
