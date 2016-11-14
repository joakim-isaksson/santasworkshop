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
        Transform controller = other.transform.parent;
        Transform rotator = other.transform;

        // Set the parent Rotator's local position, relative to the controller, as the difference from the calibration point to the controller
        rotator.localPosition = transform.position - controller.position;

        // Set the parent Rotator's local rotation, relative to the controller, as the rotation of the table
        rotator.localRotation = transform.rotation;
    }
}
