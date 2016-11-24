using UnityEngine;
using System.Collections.Generic;

public class PresentCube : MonoBehaviour
{
	[HideInInspector]
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
	[HideInInspector]
	public bool SentAway;

	[HideInInspector]
	public GameObject Present;

	// Privates
	private List<GameObject> peanuts;
	private MovableCube movableCube;

	private GameObject lid;


	void Start()
	{
		movableCube = GetComponent<MovableCube>();
		peanuts = new List<GameObject>();
		lid = transform.Find("Lid").gameObject;
	}

	void Update()
	{
		// Show/hide the lid
		lid.SetActive(HasLid);
	}

	/// <summary>
	/// Destroy the peanuts and other objects belonging to this object.
	/// </summary>
	void OnDestroy()
	{
		foreach (GameObject peanut in peanuts)
		{
			Destroy(peanut);
		}
		Destroy(Present);
	}

	/// <summary>
	/// Handle objects entering the present
	/// </summary>
	/// <param name="other"></param>
	void OnTriggerEnter(Collider other)
	{
		if ( !(other.tag == "Peanut" || other.tag == "Present") )
		{
			return;
		}

		if (HasLid)
		{
			Debug.Log("Trigger enter when lid is on, shouldn't happen");
			return;
		}

		if (other.tag == "Peanut")
		{
			if (peanuts.Count > MaxPeanuts)
			{
				Destroy(other.gameObject);
				return;
			}
			peanuts.Add(other.gameObject);
		}
		else if (other.tag == "Present")
		{
			Present = other.gameObject;
		}
	}

	/// <summary>
	/// Handle objects exiting the present
	/// </summary>
	/// <param name="other"></param>
	void OnTriggerExit(Collider other)
	{
		if (!(other.tag == "Peanut" || other.tag == "Present"))
		{
			return;
		}

		if (HasLid)
		{
			Debug.Log("Trigger exit when lid is on, shouldn't happen");
			return;
		}

		if (other.tag == "Peanut")
		{
			peanuts.Remove(other.gameObject);
		}
		else if (other.tag == "Present")
		{
			Present = null;
		}
	}
}
