using UnityEngine;
using System.Collections.Generic;

public class PresentCube : MonoBehaviour
{
	public GameObject PresentPrefab;

	public bool Spawned;
	public bool Ready
	{
		get
		{
			return movableCube.Stationary && HasLid;
		}
	}

	public float PeanutFillRatio
	{
		get
		{
			if (peanuts == null)
			{
				return 0f;
			}

			return (float)peanuts.Count / (float)MaxPeanuts;
		}
	}
	public int MaxPeanuts;

	[HideInInspector]
	public bool HasLid;
	[HideInInspector]
	public bool HasPresent;
	public bool SentAway;

	// Privates
	private List<GameObject> peanuts;
	private MovableCube movableCube;


	void Start()
	{
		movableCube = GetComponent<MovableCube>();
	}
	

	void Update()
	{
	
	}

	/// <summary>
	/// Destroy the peanuts and other objects belonging to this object.
	/// </summary>
	void OnDestroy()
	{

	}

	/// <summary>
	/// Handle objects entering the present
	/// </summary>
	/// <param name="other"></param>
	void OnTriggerEnter(Collider other)
	{
		if (HasLid)
		{
			Debug.Log("Trigger enter when lid is on, shouldn't happen");
			return;
		}

		if (other.tag == "Peanut")
		{

		}
		else if (other.tag == "Present")
		{

		}
	}

	/// <summary>
	/// Handle objects exiting the present
	/// </summary>
	/// <param name="other"></param>
	void OnTriggerExit(Collider other)
	{
		if (HasLid)
		{
			Debug.Log("Trigger exit when lid is on, shouldn't happen");
			return;
		}

		if (other.tag == "Peanut")
		{

		}
		else if (other.tag == "Present")
		{

		}
	}
}
