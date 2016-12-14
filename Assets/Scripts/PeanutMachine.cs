using UnityEngine;
using System.Collections;

public class PeanutMachine : MonoBehaviour
{
    public DropZone ActivationZone;

    public GameObject PeanutPrefab;
    public Transform PeanutSpawnPoint;
    public float PeanutSpawnRate;
    public float EmitForce;

    private float peanutSpawnTimer;
    private bool spawnPeanuts;
	private bool startedSpawning;
	private bool stoppedSpawning = true;
    
    void Update()
    {
        float dt = Time.deltaTime;

        spawnPeanuts = false;
        if (ActivationZone.ContainsCube && ActivationZone.ContainedCube.Stationary)
        {
            spawnPeanuts = true;
        }

        peanutSpawnTimer += dt;

        // Handle spawning peanuts on a timer
        if (spawnPeanuts)
        {
			stoppedSpawning = false;
			if (!startedSpawning)
			{
				startedSpawning = true;
				GetComponent<AudioSource>().Play();
			}
            if (peanutSpawnTimer > 1.0f/PeanutSpawnRate)
            {
                peanutSpawnTimer = 0f;

                GameObject peanut = Instantiate(PeanutPrefab);
                peanut.transform.position = PeanutSpawnPoint.position;
                peanut.transform.rotation = Random.rotation;

                // Spawn the peanut with random added force
                Vector3 spawnDirection = PeanutSpawnPoint.up * -1.0f * EmitForce;
                Vector3 randomSpawnOffset = Random.onUnitSphere / 2.0f;
                peanut.GetComponent<Rigidbody>().AddForce(spawnDirection + randomSpawnOffset, ForceMode.Impulse);
            }
        }
		else
		{
			startedSpawning = false;

			if (!stoppedSpawning)
			{
				stoppedSpawning = true;
				GetComponent<AudioSource>().Stop();

				GameObject.Find("Tutorial").GetComponent<Tutorial>().UpdateProgress("Peanut");
			}
		}
    }
}
