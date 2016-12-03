using UnityEngine;
using System.Collections;

public class PeanutMachine : MonoBehaviour
{
    public GameObject PeanutPrefab;
    public float PeanutSpawnRate;
    private float peanutSpawnTimer;

    public float EmitForce;

    private DropZone activationZone;
    private bool spawnPeanuts;
    private Transform spawnPoint;

	void Awake()
	{
	    activationZone = transform.Find("ActivationZone").GetComponent<DropZone>();
	    spawnPoint = transform.Find("PeanutSpawnPoint").transform;
	}

    void Start() 
    {
    
    }
    
    void Update()
    {
        float dt = Time.deltaTime;

        spawnPeanuts = false;
        if (activationZone.ContainsCube && activationZone.ContainedCube.Stationary)
        {
            Debug.Log("HERE");
            spawnPeanuts = true;
        }

        peanutSpawnTimer += dt;

        if (spawnPeanuts)
        {
            if (peanutSpawnTimer > 1.0f/PeanutSpawnRate)
            {
                peanutSpawnTimer = 0f;

                GameObject peanut = Instantiate(PeanutPrefab);
                peanut.transform.position = spawnPoint.position;

                Vector3 spawnDirection = spawnPoint.up*-1.0f*EmitForce;

                peanut.GetComponent<Rigidbody>().AddForce(spawnDirection);
            }
        }
    }
}
