using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
	public int Progress;

	public GameObject LeftSpot;
	public GameObject RightSpot;
	public GameObject PeanutSpot;
	public GameObject PressSpot;

	public string[] spotOrder;

	// Use this for initialization
	void Start()
	{
		spotOrder = new string[] { "Spawn", "Peanut", "Press", "Spawn", "Spawn", "Peanut", "Press" };

		Progress = 0;

		LeftSpot.SetActive(true);
		RightSpot.SetActive(false);
		PeanutSpot.SetActive(false);
		PressSpot.SetActive(false);
	}

	public void UpdateProgress(string invoker)
	{
		Debug.Log("Called tutorial from " + invoker);
		
		if (Progress < spotOrder.Length)
		{
			if (spotOrder[Progress] != invoker)
			{
				return;
			}
        }
		Progress++;

		LeftSpot.SetActive(false);
		RightSpot.SetActive(false);
		PeanutSpot.SetActive(false);
		PressSpot.SetActive(false);


		switch (Progress)
		{
			case 0:
				LeftSpot.SetActive(true);
				break;
			case 1:
				PeanutSpot.SetActive(true);
				break;
			case 2:
				PressSpot.SetActive(true);
				break;
			case 3:
				LeftSpot.SetActive(true);
				break;
				/*
			case 4:
				RightSpot.SetActive(true);
				break;
			case 5:
				PeanutSpot.SetActive(true);
				break;
			case 6:
				PressSpot.SetActive(true);
				break;
			case 7:
				RightSpot.SetActive(true);
				break;
				*/
			default:
				break;

		}
	}
}
