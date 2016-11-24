using UnityEngine;
using System.Collections;

public class MovableCube : MonoBehaviour
{
	public float StationaryThreshold;

	[HideInInspector]
	public bool Stationary
	{
		get { return IsStationary(); }
	}

	private Vector3[] lastPositions;
	private int lastPositionIndex;
	private int lastPositionsMax = 60;

	//temporary
	private bool wasStationary;

	void Awake()
	{
		// Initialize the lastPositions array
		lastPositions = new Vector3[lastPositionsMax];
		lastPositionIndex = 0;
		for (int i = 0; i < lastPositionsMax; ++i)
		{
			lastPositions[i] = transform.position;
		}

	}

	void Update()
	{
		StorePosition();

		if (Stationary)
		{
			if (!wasStationary)
			{
				wasStationary = true;
			}
		}
		else
		{
			wasStationary = false;
		}
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
