// Arrays for pitch, volume, and reverb
new float[0] @=> float spherePitches[];
new float[0] @=> float sphereVolumes[];
new float[0] @=> float sphereReverbs[];

// Arrays for audio UGens
new SinOsc[0] @=> SinOsc spheresOsc[];
new Gain[0] @=> Gain spheresGain[];
new JCRev[0] @=> JCRev spheresReverb[];

// Global reverb connected to DAC
dac => JCRev globalReverb => dac;
0.2 => globalReverb.mix;

// Function to resize a float array
fun void resizeFloatArray(float value, float array[])
{
    new float[array.size() + 1] @=> float newArray[];
    for (0 => int i; i < array.size(); i++)
    {
        array[i] @=> newArray[i];
    }
    value @=> newArray[array.size()];
    newArray @=> array;
}

// Function to resize a SinOsc array
fun void resizeSinOscArray(SinOsc value, SinOsc array[])
{
    new SinOsc[array.size() + 1] @=> SinOsc newArray[];
    for (0 => int i; i < array.size(); i++)
    {
        array[i] @=> newArray[i];
    }
    value @=> newArray[array.size()];
    newArray @=> array;
}

// Function to resize a Gain array
fun void resizeGainArray(Gain value, Gain array[])
{
    new Gain[array.size() + 1] @=> Gain newArray[];
    for (0 => int i; i < array.size(); i++)
    {
        array[i] @=> newArray[i];
    }
    value @=> newArray[array.size()];
    newArray @=> array;
}

// Function to resize a JCRev array
fun void resizeJCRevArray(JCRev value, JCRev array[])
{
    new JCRev[array.size() + 1] @=> JCRev newArray[];
    for (0 => int i; i < array.size(); i++)
    {
        array[i] @=> newArray[i];
    }
    value @=> newArray[array.size()];
    newArray @=> array;
}

// Function to add a new sphere
fun void addSphere()
{
    // Create new UGens
    SinOsc s => Gain g => JCRev r => globalReverb;

    // Add UGens to their respective arrays
    resizeSinOscArray(s, spheresOsc);
    resizeGainArray(g, spheresGain);
    resizeJCRevArray(r, spheresReverb);

    // Add default parameters to their arrays
    resizeFloatArray(440.0, spherePitches); // Default pitch
    resizeFloatArray(0.5, sphereVolumes);  // Default volume
    resizeFloatArray(0.1, sphereReverbs);  // Default reverb
}

// Function to update sphere properties from Unity
fun void updateSphere(int index, float pitch, float volume, float reverb)
{
    // Add new spheres if necessary
    while (index >= spheresOsc.size())
    {
        addSphere();
    }

    // Update the specified sphere's parameters
    pitch => spherePitches[index];
    volume => sphereVolumes[index];
    reverb => sphereReverbs[index];

    // Apply parameters to the UGens
    spherePitches[index] => spheresOsc[index].freq;
    sphereVolumes[index] => spheresGain[index].gain;
    sphereReverbs[index] => spheresReverb[index].mix;

    <<< "Updated sphere: ", index, " Pitch: ", pitch, " Volume: ", volume, " Reverb: ", reverb >>>;
}

// Main real-time processing loop
spork ~ processAudio();

fun void processAudio()
{
    while (true)
    {
        10::ms => now; // Keep the program running
    }
}
