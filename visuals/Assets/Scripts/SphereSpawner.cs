using UnityEngine;

public class SphereSpawner : MonoBehaviour
{
    public GameObject spherePrefab;   // Reference to the sphere prefab
    public int numberOfSpheres = 10;  // Number of spheres to spawn
    public float spawnRange = 5f;     // Range within which spheres are spawned

    void Start()
    {
        // Spawn multiple spheres
        for (int i = 0; i < numberOfSpheres; i++)
        {
            SpawnSphere();
        }
    }

    void SpawnSphere()
    {
        // Random position within the spawn range
        Vector3 randomPosition = new Vector3(
            Random.Range(-spawnRange, spawnRange),
            Random.Range(1f, spawnRange), // Height should be above the ground
            Random.Range(-spawnRange, spawnRange)
        );

        // Instantiate the sphere at the random position
        Instantiate(spherePrefab, randomPosition, Quaternion.identity);
    }
}
