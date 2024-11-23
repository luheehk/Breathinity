// using UnityEngine;

// public class SphereControllerArduino : MonoBehaviour
// {
//     public GameObject redObject;  // Breath point
//     public ArduinoInput arduinoInput; 
//     public float maxSpreadDistance = 5f;
//     public float randomMovementIntensity = 0.5f;
//     public float boundaryCorrectionFactor = 0.5f;
//     public GameObject cube;
//     private Vector3 cubeCenter;
//     private Vector3 cubeBounds;

//     private Transform[] spheres;
//     private Vector3[] noiseOffsets;

//     void Start()
//     {
//         cubeCenter = cube.transform.position;
//         cubeBounds = cube.transform.localScale / 2;

//         spheres = GetComponentsInChildren<Transform>();

//         noiseOffsets = new Vector3[spheres.Length];
//         for (int i = 0; i < spheres.Length; i++)
//         {
//             noiseOffsets[i] = new Vector3(Random.value * 10f, Random.value * 10f, Random.value * 10f);
//         }
//     }

//     void Update()
//     {
//         float spreadFactor = (arduinoInput != null) ? (arduinoInput.GetArduinoInputValue() + 1f) / 2f : 0f;  // Map Arduino value here - now set up with (-1 to 1) to (0 to 1)
//         float time = Time.time * 0.5f;

//         for (int i = 0; i < spheres.Length; i++)
//         {
//             Transform sphere = spheres[i];
//             if (sphere == transform) continue;

//             Vector3 directionToRed = (sphere.position - redObject.transform.position).normalized;

//             Vector3 targetPosition = redObject.transform.position + directionToRed * maxSpreadDistance * spreadFactor;

//             Vector3 noiseOffset = new Vector3(
//                 Mathf.PerlinNoise(time + noiseOffsets[i].x, 0) - 0.5f,
//                 Mathf.PerlinNoise(time + noiseOffsets[i].y, 1) - 0.5f,
//                 Mathf.PerlinNoise(time + noiseOffsets[i].z, 2) - 0.5f
//             ) * randomMovementIntensity * spreadFactor;

//             Vector3 newPosition = Vector3.Lerp(sphere.position, targetPosition + noiseOffset, Time.deltaTime);

//             Vector3 boundaryCorrection = GetBoundaryCorrection(newPosition);
//             if (boundaryCorrection != Vector3.zero)
//             {
//                 newPosition += boundaryCorrection * Time.deltaTime;
//             }

//             sphere.position = newPosition;
//         }
//     }

//     private Vector3 GetBoundaryCorrection(Vector3 position)
//     {
//         Vector3 correction = Vector3.zero;

//         if (Mathf.Abs(position.x - cubeCenter.x) > cubeBounds.x)
//         {
//             correction.x = (cubeCenter.x - position.x) * boundaryCorrectionFactor;
//         }
//         if (Mathf.Abs(position.y - cubeCenter.y) > cubeBounds.y)
//         {
//             correction.y = (cubeCenter.y - position.y) * boundaryCorrectionFactor;
//         }
//         if (Mathf.Abs(position.z - cubeCenter.z) > cubeBounds.z)
//         {
//             correction.z = (cubeCenter.z - position.z) * boundaryCorrectionFactor;
//         }

//         return correction;
//     }
// }
