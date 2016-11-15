using UnityEngine;
using System.Collections;

public class CubeCalibration : MonoBehaviour
{
    public GameObject LeftRotator;
    public GameObject RightRotator;

    void Awake()
    {
        // Move the calibration point to the center of the cube, so x-0.15, y/z+0.15
        transform.Translate(new Vector3(-0.15f, 0.15f, 0.15f));
    }

    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            MatchRotation(LeftRotator);
        }

        if (Input.GetKeyDown("return"))
        {
            MatchRotation(RightRotator);
        }
    }

    /// <summary>
    /// Calibrates the given rotator object
    /// </summary>
    /// <param name="other"></param>
    void MatchRotation(GameObject other)
    {
        Transform rotator = other.transform;

		// Set the rotator's position and rotation
		rotator.position = transform.position;
        rotator.rotation = transform.rotation;
    }
}
