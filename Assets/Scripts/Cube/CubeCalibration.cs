using UnityEngine;
using System.Collections;
using NewtonVR;

public class CubeCalibration : MonoBehaviour
{
	private GameFlow gameFlow;

    void Awake()
    {
        // Move the calibration point to the center of the cube, so x-0.15, y/z+0.15
        transform.Translate(new Vector3(-0.15f, 0.15f, 0.15f));

		gameFlow = GameObject.Find("General").GetComponent<GameFlow>();
    }

    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
			GameObject cube = gameFlow.Cubes[0];
            MatchRotation(cube);
        }

        if (Input.GetKeyDown("return"))
        {
			GameObject cube = gameFlow.Cubes[1];
			MatchRotation(cube);
        }
    }

    /// <summary>
    /// Calibrates the given rotator object
    /// </summary>
    /// <param name="other"></param>
    void MatchRotation(GameObject other)
    {
		if (other == null) return;

        Transform rotator = other.transform.Find("Rotator");

		NVRHand hand = other.GetComponent<MovableCube>().hand;

		// Set the rotator's position and rotation
		rotator.position = other.transform.position - ( transform.position - hand.transform.position );
		rotator.transform.localRotation = Quaternion.Inverse(transform.rotation) * hand.transform.rotation;
    }
}
