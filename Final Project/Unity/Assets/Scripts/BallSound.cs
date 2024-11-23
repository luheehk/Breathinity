using UnityEngine;

public class BallSound : MonoBehaviour
{
    private ChuckSubInstance chuckInstance;

    void Start()
    {
        // Get the ChuckSubInstance component and run the ChucK code
        chuckInstance = GetComponent<ChuckSubInstance>();
        
        // Run ChucK code directly from C# with RunCode
        chuckInstance.RunCode(@"
            TriOsc oscillator => dac;
            0.9 => oscillator.gain;
            440 => float baseFreq;
            
            while (true) {
                10::ms => now;
            }
        ");
    }

    void Update()
    {
        
    }
}
