using UnityEngine;
using System.Collections;
using NewtonVR;

public class Pipe : MonoBehaviour
{
    public GameObject CubePrefab;
    public GameObject[] PresentPrefabs;
    public GameObject OwnerRotator;
    public Transform CubeSpawnPoint;
    public CubeSide Side;
    public Animator Anim;
    public float AnimationTime;

    [HideInInspector]
    public bool Closed;

    DropZone dropZone;
    bool onCooldown;

    void Awake()
    {
        dropZone = GetComponentInChildren<DropZone>();
    }

    void Start()
    {
        StartCoroutine(ClosePipe(null));
    }
	
	void Update()
    {
        if (onCooldown) return;

        if (Closed)
        {

        }

        // Open
        else
        {
            if (dropZone.ContainsCube)
            {
                PresentCube cube = dropZone.ContainedCube;
                if (cube.HasLid && cube.Stationary)
                {
                    MovableCube mCube = cube.GetComponent<MovableCube>();
                    StartCoroutine(ClosePipe(mCube));
                }
            }
        }
    }

    /// <summary>
    /// Spawn the cube and pass it a randomly chosen present
    /// </summary>
    public GameObject SpawnCube(NVRHand hand, bool startAnimation)
    {
        GameObject cube = Instantiate(CubePrefab);
        cube.transform.position = CubeSpawnPoint.position;
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

    IEnumerator ClosePipe(MovableCube insideCube)
    {
        Anim.SetTrigger("Close");
        if (insideCube != null) insideCube.TakeOutOfPlay();
        
        yield return new WaitForSeconds(AnimationTime);


        Closed = true;
    }

    IEnumerator OpenPipe()
    {
        Anim.SetTrigger("Open");
        yield return new WaitForSeconds(AnimationTime);
        Closed = false;
    }
}
