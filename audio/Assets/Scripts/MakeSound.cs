using UnityEngine;

public class MakeSound : MonoBehaviour
{
    public ChuckSubInstance chuckInstance;
    public Transform playerTransform;

    private Vector3 lastPosition;
    private float totalDistanceTraveled = 0.0f;
    private float reverbAmount = 0.0f;
    private int lastDistanceCheckpoint = 0;
    
    void Start()
    {
        lastPosition = playerTransform.position;

        chuckInstance.RunCode(@"
            global int pitch;
            global float reverb;

            SinOsc foo => dac;
            JCRev verb => dac;
            foo => verb;

            // Set gain to 0.5
            0.1 => dac.gain;

            // Update pitch periodically
            while(true)
            {
                // Adjust the oscillator frequency according to pitch
                pitch => foo.freq;
                reverb => verb.mix;
                100::ms => now;
            }
        ");
    }

    void Update()
    {
        // Calculate distance traveled since the last frame
        float distanceThisFrame = Vector3.Distance(lastPosition, playerTransform.position);
        totalDistanceTraveled += distanceThisFrame;

        // Update the last position for the next frame
        lastPosition = playerTransform.position;

        // Send the pitch update to ChucK
        chuckInstance.SetInt("pitch", (int)totalDistanceTraveled);
        Debug.Log("Total steps: " + (int)totalDistanceTraveled);

        int currentCheckpoint = Mathf.FloorToInt(totalDistanceTraveled / 10) * 10;
        if (currentCheckpoint > lastDistanceCheckpoint)
        {
            // Increment reverb by 5% (or 0.05 in range 0.0 to 1.0)
            reverbAmount = Mathf.Clamp(reverbAmount + 0.05f, 0.0f, 0.9f);

            // Send updated reverb amount to ChucK
            chuckInstance.SetFloat("reverb", reverbAmount);

            // Update last checkpoint reached
            lastDistanceCheckpoint = currentCheckpoint;

            Debug.Log("Reverb increased to: " + reverbAmount);
        }
        
    }
}
