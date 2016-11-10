using UnityEngine;
using System.Collections;

public class DropZone : MonoBehaviour
{

    private SphereCollider sphereCollider;

    void Awake()
    {
        sphereCollider = GetComponent<SphereCollider>();
    }

    void OnTriggerStay(Collider collider)
    {
        // Check if the other collider is completely encapsuled in our sphere collider
        if (collider.tag == "MovableCube")
        {
            // Find all corners of the cube, and see if all of them are inside the trigger
            GameObject cube = collider.gameObject;
            // TODO make const
            Vector3[] corners = {
                new Vector3(1, 1, 1),
                new Vector3(1, 1, -1),
                new Vector3(1, -1, 1),
                new Vector3(-1, 1, 1),
                new Vector3(1, -1, -1),
                new Vector3(-1, 1, -1),
                new Vector3(-1, -1, 1),
                new Vector3(-1, -1, -1)
            };

            bool partiallyOutside = false;

            foreach (Vector3 corner in corners)
            {
                Vector3 cornerPosition = cube.transform.TransformPoint(corner / 2.0f);
                if (Vector3.Distance(cornerPosition, transform.position) >= sphereCollider.radius)
                {
                    partiallyOutside = true;
                    break;
                }
            }

            // If the whole cube is contained within the sphere
            if (!partiallyOutside)
            {
                cube.GetComponent<Rigidbody>().AddTorque(new Vector3(1, 0, 0));
            }

        } 
    }
    
}
