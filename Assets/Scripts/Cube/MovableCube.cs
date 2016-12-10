using UnityEngine;
using System.Collections;
using NewtonVR;

public enum CubeSide
{
    Left = 0,
    Right = 1
}

public class MovableCube : MonoBehaviour
{
	public float StationaryThreshold;

	public bool Stationary
	{
		get { return IsStationary(); }
	}

	private Vector3[] lastPositions;
	private int lastPositionIndex;
	private int lastPositionsMax = 60;

    [HideInInspector]
    public CubeSide Side;
    [HideInInspector]
    public GameObject OwnerRotator;

	// Privates

	private Transform rotator;

    private PresentCube presentCube;

	private bool wasStationary; //temporary?

    private bool isInPlay;
    private bool isAnimatingSpawn;
    private bool isAnimatingExit;

    // Animation helpers
    private float spawnAnimationProgress;
    public float spawnAnimationSpeed;
    private Vector3 spawnAnimationStartPosition;
    private Quaternion spawnAnimationStartRotation;

	[HideInInspector]
	public NVRHand hand;

	void Awake()
	{
	    presentCube = GetComponent<PresentCube>();
		// Initialize the lastPositions array
		lastPositions = new Vector3[lastPositionsMax];
		lastPositionIndex = 0;
		for (int i = 0; i < lastPositionsMax; ++i)
		{
			lastPositions[i] = transform.position;
		}
	}

    /// <summary>
    /// Cube spawns into existence. Starts animating the cube towards the assigned controller location.
    /// </summary>
    public void Init(NVRHand assignedHand)
    {
		hand = assignedHand;

		//Copy the calibrated rotator values from the gameobjects CalibratedLeftRotator and CalibratedRightRotator
		Transform calibratedRotator;
		if (Side == CubeSide.Left)
		{
			calibratedRotator = GameObject.Find("CalibratedLeftRotator").transform;
		}
		else
		{
			calibratedRotator = GameObject.Find("CalibratedRightRotator").transform;
		}
		rotator = transform.Find("Rotator").transform;
		rotator.localPosition = calibratedRotator.position;
		rotator.localRotation = calibratedRotator.rotation;

		isAnimatingSpawn = true;

        spawnAnimationStartPosition = transform.position;
        spawnAnimationStartRotation = transform.rotation;
    }

    /// <summary>
    /// Assigns the cube as a child of the controller and does other activation duties like activating the present physics.
    /// </summary>
    public void TakeIntoPlay()
    {
        isAnimatingSpawn = false;
        // Announces itself to the general gameflow object
        GameObject.Find("General").GetComponent<GameFlow>().RegisterCube(gameObject);

        isInPlay = true;
		hand.BeginInteraction(gameObject.GetComponent<NVRInteractableItem>());
        presentCube.DetachPresent();
    }

    /// <summary>
    /// Takes the cube out of gameplay.
    /// </summary>
    public void TakeOutOfPlay()
    {
		Debug.Log("Take out of play");
        isAnimatingExit = true; 
        isInPlay = false;

		hand.EndInteraction(gameObject.GetComponent<NVRInteractableItem>());
    }

	/*
	public void ReattachHand()
	{
		Debug.Log("Detaching hand");
		hand.EndInteraction(gameObject.GetComponent<NVRInteractableItem>());

		StartCoroutine(AttachHand());
	}

	private IEnumerator AttachHand()
	{
		yield return new WaitForEndOfFrame();
		Debug.Log("Attaching hand");
		hand.BeginInteraction(gameObject.GetComponent<NVRInteractableItem>());
	}
	*/

	void Update()
	{
		StorePosition();

		if (Stationary)
		{
			if (!wasStationary) // Start being stationary, can be used for events
			{
				wasStationary = true;
			}
		}
		else // Remove stationary status
		{
			wasStationary = false;
		}

        if (isAnimatingSpawn)
        {
            AnimateSpawn();
        }
        if (isAnimatingExit)
        {
            AnimateExit();
        }
        
	}

    /// <summary>
    /// Animates the object's rotation and position during the spawn.
    /// </summary>
    void AnimateSpawn()
    {
        float dt = Time.deltaTime;

        spawnAnimationProgress += dt * spawnAnimationSpeed;

		Vector3 endPosition = OwnerRotator.transform.position - rotator.localPosition;
		Quaternion endRotation = OwnerRotator.transform.rotation * Quaternion.Inverse(rotator.localRotation);
        
        transform.position = Vector3.Lerp(spawnAnimationStartPosition, endPosition, spawnAnimationProgress);
        transform.rotation = Quaternion.Lerp(spawnAnimationStartRotation, endRotation, spawnAnimationProgress);

        if (spawnAnimationProgress >= 1f)
        {
            TakeIntoPlay();
        }
    }

    /// <summary>
    /// Animates the object's rotation and position when it is taken out of play.
    /// </summary>
    void AnimateExit()
    {

    }

	/// <summary>
	/// Stores current position into the lastPositions array. Overwrites old values.
	/// </summary>
	void StorePosition()
	{
		lastPositionIndex = (lastPositionIndex + 1) % lastPositionsMax;

		lastPositions[lastPositionIndex] = transform.position;
	}

	/// <summary>
	/// Returns true if the object has been relatively stationary.
	/// </summary>
	/// <returns></returns>
	bool IsStationary()
	{
		float sum = 0f;
		for (int i = 0; i < lastPositionsMax; ++i)
		{
			sum += Vector3.Distance(lastPositions[i], transform.position);
		}
		sum /= lastPositionsMax;

		return sum <= StationaryThreshold;
	}
}
