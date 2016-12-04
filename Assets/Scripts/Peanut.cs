using UnityEngine;
using System.Collections;

public class Peanut : MonoBehaviour
{
    public float FadeStartTime;
    public float DestroyTime;

    [HideInInspector]
    public bool willDestroy;

    private float hitTimer;
    private Material material;
    private Color originalColor;

    void Start()
    {
        material = GetComponent<MeshRenderer>().material;
        originalColor = material.color;
    }

    /// <summary>
    /// If the peanut is getting destroyed, fade it out.
    /// </summary>
	void Update()
	{
	    float dt = Time.deltaTime;
        if (willDestroy)
        {
            hitTimer += dt;

            if (hitTimer > FadeStartTime)
            {
                float fade = 1 - (hitTimer - FadeStartTime)/(DestroyTime - FadeStartTime);
                material.color = new Color(originalColor.r, originalColor.g, originalColor.b, fade);
            }
        }
	}

    /// <summary>
    /// If the peanut hits anything but the box, another peanut, or the presents, fade out and destroy.
    /// </summary>
    /// <param name="other"></param>
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag != "MovableCube" && 
            other.gameObject.tag != "Peanut" &&
            other.gameObject.tag != "Present")
        {
            Destroy(gameObject, DestroyTime);
            willDestroy = true;
        }
    }
}
