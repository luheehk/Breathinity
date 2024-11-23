using UnityEngine;

public class BouncingObject : MonoBehaviour
{
    public Vector3 velocity = new Vector3(1, 1, 1);  // Initial movement direction
    public float speed = 5f;                        // Movement speed

    void Update()
    {
        // Normalize velocity and maintain speed
        transform.position += velocity.normalized * speed * Time.deltaTime;
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Reflect the velocity based on collision normal
        velocity = Vector3.Reflect(velocity, collision.contacts[0].normal);
    }
}
