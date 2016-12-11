using UnityEngine;
using System.Collections.Generic;

public class PresentCube : MonoBehaviour
{
	[HideInInspector]
	public bool Spawned;
	public bool Ready
	{
		get { return movableCube.Stationary && HasLid; }
	}

    public bool Stationary
    {
        get { return movableCube.Stationary; }
    }

	/*
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
	*/
	private bool hasLid;
	[HideInInspector]
	public bool HasLid {
		get { return hasLid; }
		set
		{
			hasLid = value;
			if (hasLid)
			{
				DestroyInnards();	
			}
		}
	}
	[HideInInspector]
	public bool HasPresent;
	[HideInInspector]
	public bool SentAway;

	[HideInInspector]
	public GameObject Present;

	// Privates
	private List<GameObject> innards;
	private MovableCube movableCube;

	private GameObject lid;


	void Start()
	{
		movableCube = GetComponent<MovableCube>();
		innards = new List<GameObject>();
		lid = transform.Find("Lid").gameObject;
	}

	void Update()
	{
		// Show/hide the lid
		lid.SetActive(HasLid);
	}

    /// <summary>
    /// Assigns this cube the given present. Repositions the present.
    /// </summary>
    /// <param name="present"></param>
    public void AssignPresent(GameObject present)
    {
        Transform presentAnchor = transform.Find("PresentAnchor").transform;
        
        present.transform.position = presentAnchor.position;
        present.transform.rotation = presentAnchor.rotation;

		present.transform.parent = presentAnchor;
	}

    /// <summary>
    /// Detach the present that was assigned to the cube after the cube has spawned
    /// </summary>
    public void DetachPresent()
    {
        GameObject present;
        foreach (Transform child in transform.Find("PresentAnchor").transform)
        {
            if (child.tag == "Present")
            {
                present = child.gameObject;
                present.GetComponent<Rigidbody>().isKinematic = false;
                present.transform.parent = null;
                break;
            }
        }
    }

	/// <summary>
	/// Destroy the peanuts and other objects belonging to this object.
	/// </summary>
	void OnDestroy()
	{
		DestroyInnards();
	}

	/// <summary>
	/// Clears any objects (Peanuts/Presents) inside the cube.
	/// </summary>
	void DestroyInnards()
	{
		foreach(GameObject obj in innards)
		{
			Debug.Log("Destroying " + obj);
			Destroy(obj);
		}
		innards = new List<GameObject>();
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
			//Debug.Log("Trigger enter when lid is on, shouldn't happen");
			return;
		}

		innards.Add(other.gameObject);
		/*
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
		*/
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
			//Debug.Log("Trigger exit when lid is on, shouldn't happen");
			return;
		}

		innards.Remove(other.gameObject);

		/*
		if (other.tag == "Peanut")
		{
			peanuts.Remove(other.gameObject);
		}
		else if (other.tag == "Present")
		{
			Present = null;
		}
		*/
	}

}
