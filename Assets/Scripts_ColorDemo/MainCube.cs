using System;
using UnityEngine;
using System.Collections;

public class MainCube : MonoBehaviour
{
    public GameObject[] Sides;

    private BoxCollider boxCollider;
    private SphereColor topMostColor;

    // Use this for initialization
    void Start ()
    {
        boxCollider = GetComponent<BoxCollider>();
    }
    
    // Update is called once per frame
    void Update ()
    {
        // Recolor top most side
        float topMostDot = Single.NegativeInfinity;
        ColoredSide topMost = null;
        foreach (GameObject side in Sides)
        {
            ColoredSide sideScript = side.GetComponent<ColoredSide>();
            sideScript.isHighlighted = false;

            float newDot = Vector3.Dot(side.transform.up, Vector3.up);
            if (newDot > topMostDot)
            {
                topMostDot = newDot;
                topMost = sideScript;
            }
        }

        topMost.isHighlighted = true;

        topMostColor = topMost.SideColor;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Sphere")
        {
            if (other.gameObject.GetComponent<FallingSphere>().RandomColor == topMostColor)
            {
				StartCoroutine(HapticUtils.LongVibrationBoth(2, HapticUtils.Buzz));
				Destroy(other.gameObject);
            }
        }
    }
}

public class HapticUtils
{
	public delegate float StrengthFunction(float t);

	public static float MaxStrength(float t) { return 1; }

	public static IEnumerator LongVibrationBoth(float length, StrengthFunction f = null)
	{
		// Default to constant function
		if (f == null) f = MaxStrength;

		int left = SteamVR_Controller.GetDeviceIndex(SteamVR_Controller.DeviceRelation.Leftmost);
		int right = SteamVR_Controller.GetDeviceIndex(SteamVR_Controller.DeviceRelation.Rightmost);

		for (float i = 0; i < length; i += Time.deltaTime)
		{
			float strength = f(i / length);

			if (left != -1) SteamVR_Controller.Input(left).TriggerHapticPulse((ushort)Mathf.Lerp(0, 3999, strength));
			if (right != -1 && left != right) SteamVR_Controller.Input(right).TriggerHapticPulse((ushort)Mathf.Lerp(0, 3999, strength));
			yield return null;
		}
	}

	public static float Buzz(float t)
	{
		return Mathf.Exp(-10 * t);
	}
}
