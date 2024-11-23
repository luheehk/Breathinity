using UnityEngine;
using System.Collections.Generic;
using extOSC;

public class SphereSpawner : MonoBehaviour
{
    public GameObject spherePrefab; // The sphere prefab
    public float inputValue; // Slider-controlled value
    public float minSpeed = 10f; // Minimum sphere speed
    public float maxSpeed = 100f; // Maximum sphere speed
    public int maxSpheres = 1000; // Maximum number of spheres
    private List<GameObject> spherePool = new List<GameObject>();
    public OSCReceiver _receiver;

    void Start()
    {
        // Create a pool of spheres but deactivate them initially
        for (int i = 0; i < maxSpheres; i++)
        {
            GameObject sphere = Instantiate(spherePrefab, Vector3.zero, Quaternion.identity, transform);
            sphere.SetActive(false); // Initially disabled
            spherePool.Add(sphere);
        }

        _receiver.Bind("/address", ReceiveFloat);

        UpdateSpheres();
    }

    // public void receiveSphereVal()
    // {
    //     UpdateSpheres();
    // }

    public void ReceiveFloat(OSCMessage message)
    {
        if (message.ToFloat(out var value))
        {
            inputValue = value;
            UpdateSpheres();
        }
    }

    public void UpdateSpheres()
    {
        int activeSpheres = Mathf.RoundToInt(inputValue * maxSpheres);

        for (int i = 0; i < spherePool.Count; i++)
        {
            if (i < activeSpheres)
            {
                if (!spherePool[i].activeSelf)
                {
                    spherePool[i].SetActive(true);
                    Vector3 randomPosition = GetRandomSpawnPosition();
                    spherePool[i].transform.position = randomPosition;

                    // Set random speed for the BouncingObject
                    BouncingObject bouncingObject = spherePool[i].GetComponent<BouncingObject>();
                    if (bouncingObject != null)
                    {
                        bouncingObject.speed = GetRandomSpeed();
                    }
                }
            }
            else
            {
                if (spherePool[i].activeSelf)
                {
                    spherePool[i].SetActive(false);
                }
            }
        }
    }

    private Vector3 GetRandomSpawnPosition()
    {
        float range = 200f;
        return new Vector3(
            Random.Range(-range, range),
            Random.Range(-range, range),
            Random.Range(-range, range)
        );
    }

    private float GetRandomSpeed()
    {
        return Random.Range(minSpeed, maxSpeed); // Random speed within the range
    }
}
