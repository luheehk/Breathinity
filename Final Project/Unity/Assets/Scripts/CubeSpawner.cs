using UnityEngine;
using System.Collections.Generic;
using extOSC;

public class CubeSpawner : MonoBehaviour
{
    public GameObject CubePrefab; // The sphere prefab
    public float inputValue; // Slider-controlled value
    public int maxCubes = 10; // Maximum number of spheres
    private List<GameObject> cubePool = new List<GameObject>();
    public OSCReceiver _receiver;

    void Start()
    {
        // Create a pool of spheres but deactivate them initially
        for (int i = 0; i < maxCubes; i++)
        {
            GameObject cube = Instantiate(CubePrefab, Vector3.zero, Quaternion.identity, transform);
            cube.SetActive(false); // Initially disabled
            cubePool.Add(cube);
        }

        _receiver.Bind("/address", ReceiveFloat);

        UpdateCubes();
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
            UpdateCubes();
        }
    }

    public void UpdateCubes()
    {
        int activeCubes = Mathf.RoundToInt(inputValue * maxCubes);

        for (int i = 0; i < cubePool.Count; i++)
        {
            if (i < activeCubes)
            {
                if (!cubePool[i].activeSelf)
                {
                    cubePool[i].SetActive(true);
                    Vector3 randomPosition = GetRandomSpawnPosition();
                    cubePool[i].transform.position = randomPosition;
                }
            }
            else
            {
                if (cubePool[i].activeSelf)
                {
                    cubePool[i].SetActive(false);
                }
            }
        }
    }

    private Vector3 GetRandomSpawnPosition()
    {
        float range = 300f;
        return new Vector3(
            Random.Range(-range, range),
            Random.Range(-range, range),
            Random.Range(-range, range)
        );
    }
}
