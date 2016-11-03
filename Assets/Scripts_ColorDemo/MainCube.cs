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
                Destroy(other.gameObject);
            }
        }
    }
}
