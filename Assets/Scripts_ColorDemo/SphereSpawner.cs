using UnityEngine;
using System.Collections;

public class SphereSpawner : MonoBehaviour
{
    public GameObject FallingSpherePrefab;

    private float spawnTimer;
    private float spawnTimerMax = 3f;

    // Use this for initialization
    void Start () {
    
    }
    
    // Update is called once per frame
    void Update ()
    {
        float dt = Time.deltaTime;

        spawnTimer += dt;

        if (spawnTimer > spawnTimerMax)
        {
            Spawn();
            spawnTimer = 0f;
        }
    }

    void Spawn()
    {
        Vector3 randomOffset = Random.onUnitSphere;
        randomOffset.y = 0f;
        randomOffset *= Random.Range(-2f, 2f);

        GameObject newSphere = Instantiate(FallingSpherePrefab);
        newSphere.transform.position = transform.position + randomOffset;
        newSphere.GetComponent<FallingSphere>().RandomColor = (SphereColor) Random.Range(0, 5);

        newSphere.GetComponent<FallingSphere>().Initialize();
    }

}
