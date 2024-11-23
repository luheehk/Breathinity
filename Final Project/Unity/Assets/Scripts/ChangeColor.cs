using UnityEngine;
using UnityEngine.UI;
using extOSC;

public class ChangeColor : MonoBehaviour
{
    public OSCReceiver _receiver;
    [SerializeField] private string startHex = "#2E0C3C"; // Start color in hex (blue)
    [SerializeField] private string endHex = "#e1ccff";   // End color in hex (red)
    [SerializeField] private Renderer planeRenderer;

    private Color startColor;
    private Color endColor;

    private float inputval;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startColor = GenerateRandomColor();
        endColor = GenerateRandomColor();
        // ColorUtility.TryParseHtmlString(startHex, out startColor);
        // ColorUtility.TryParseHtmlString(endHex, out endColor);
        _receiver.Bind("/address", ReceiveFloat);
    }
    public void ReceiveFloat(OSCMessage message)
    {
        if (message.ToFloat(out var value))
        {
            inputval = value;
            Update();
        }
    }

    public static Color GenerateRandomColor()
    {
        // Randomly generate RGB values between 0 and 1
        float r = Random.Range(0f, 1f);
        float g = Random.Range(0f, 1f);
        float b = Random.Range(0f, 1f);

        // Return the new color
        return new Color(r, g, b);
    }

    void Update() 
    {
        // Interpolate between startColor and endColor based on the value
        Color newColor = Color.Lerp(startColor, endColor, inputval);

        // Apply the color to the plane's material
        if (planeRenderer != null)
        {
            planeRenderer.material.color = newColor;
        }

    }
}
