using UnityEngine;

public class MoveBall : MonoBehaviour
{
    public float radius = 5.0f; // Radius of the circular path
    public float speed = 2.0f;  // Speed of movement
    private Transform player;   // Reference to the player's position
    private ChuckSubInstance chuckInstance;

    void Start()
    {
        // Find the Player object
        player = GameObject.Find("Player").transform;

        // Get the ChuckSubInstance component and run the ChucK code
        chuckInstance = GetComponent<ChuckSubInstance>();
        
        // Run ChucK code directly from C# with RunCode
        chuckInstance.RunCode(@"
            TriOsc oscillator => dac;
            0.5 => oscillator.gain;
            440 => float baseFreq;
            
            while (true) {
                10::ms => now;
            }
        ");
    }

    void Update()
    {
        // Calculate new position of the ball in a circular path
        float x = player.position.x + Mathf.Cos(Time.time * speed) * radius;
        float z = player.position.z + Mathf.Sin(Time.time * speed) * radius;
        transform.position = new Vector3(x, player.position.y, z);
    }
}
