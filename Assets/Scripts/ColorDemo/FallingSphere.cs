using UnityEngine;
using System.Collections;
using System.Net.Sockets;

public enum SphereColor
{
    Red = 0,
    Blue = 1,
    Teal = 2,
    Yellow = 3,
    Green = 4
}

public class FallingSphere : MonoBehaviour {

    [HideInInspector]
    public SphereColor RandomColor;

    public Material[] Materials;

    public float FallingSpeed;

    private Rigidbody rb;

    public void Initialize()
    {
        rb = GetComponent<Rigidbody>();
        GetComponent<MeshRenderer>().material = Materials[(int) RandomColor];

        rb.AddForce(0f, -20f, 0f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Ground")
        {
            Vector3 randomDirection = Random.onUnitSphere;
            randomDirection.y = 0f;
            rb.AddForce(randomDirection * 70f);
            rb.AddForce(Vector3.up * 20f);

            Destroy(gameObject, 4f);
        }
    }
}
