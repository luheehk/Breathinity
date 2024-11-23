using UnityEngine;

public class SphereMovement : MonoBehaviour
{
    public float speed = 100f;  // Speed that can be set by the spawner

    private Vector3 direction;

    // Start is called before the first frame update
    void Start()
    {
        // Set a random movement direction
        direction = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
    }

    // Update is called once per frame
    void Update()
    {
        // Move the sphere based on speed and direction
        transform.Translate(direction * speed * Time.deltaTime);
    }
}
