using UnityEngine;
using System.Collections;

public class KeyboardDevController : MonoBehaviour
{

    public float MovementSpeed;
    public float RotationSpeed;

    void Update()
    {
        float dt = Time.deltaTime;

        // Lateral movement
        Vector3 dp = new Vector3(
                         -Input.GetAxis("Horizontal"),
                         0f,
                         -Input.GetAxis("Vertical")
                     ) * dt * MovementSpeed;

        transform.position += dp;

        // Rotation
        transform.Rotate(new Vector3(
            Input.GetAxis("Mouse Y"), 
            Input.GetAxis("Mouse X"), 
            0f
            ) * dt * RotationSpeed);
    }
}
