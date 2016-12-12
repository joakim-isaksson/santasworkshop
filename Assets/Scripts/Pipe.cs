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
    public Transform ExitPoint;
	public float ExitSpeed = 1.2f;

	[HideInInspector]
    public bool Closed;

    DropZone dropZone;
    bool animating;

    void Awake()
    {
        dropZone = GetComponentInChildren<DropZone>();
    }

    void Start()
    {

    }
	
	void Update()
    {
        if (animating) return;

        if (!Closed)
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

		StartCoroutine(OpenPipe());

		return cube;
    }

	IEnumerator ClosePipe(MovableCube cube)
    {
        animating = true;
		if (cube != null) cube.TakeOutOfPlay();

		Anim.SetTrigger(Side.ToString() + "Up");
        yield return new WaitForSeconds(AnimationTime);

        if (cube != null)
        {
            Anim.SetTrigger(Side.ToString() + "Hatch");

			while (Vector3.Distance(cube.transform.position, ExitPoint.position) < 0.01)
			{
				cube.transform.position = Vector3.MoveTowards(cube.transform.position, ExitPoint.position, Time.deltaTime * ExitSpeed);
			}
			Destroy(gameObject);
		}

        Closed = true;
        animating = false;
    }

    IEnumerator OpenPipe()
    {
        animating = true;

        Anim.SetTrigger(Side.ToString() + "Down");
        yield return new WaitForSeconds(AnimationTime);

        Closed = false;
        animating = false;
    }
}
