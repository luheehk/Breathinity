using UnityEngine;

public class BouncingObject : MonoBehaviour
{
    public Vector3 velocity = new Vector3(1, 1, 1);  // Initial movement direction
    public float speed = 5f;                          // Movement speed

    // Update is called once per frame
    void Update()
    {
        // Move the object based on velocity and speed
        transform.position += velocity * speed * Time.deltaTime;
    }

    // This function is called when the object collides with something
    private void OnCollisionEnter(Collision collision)
    {
        // Reflect the velocity to make the object bounce off the surface
        velocity = Vector3.Reflect(velocity, collision.contacts[0].normal);
    }
}
