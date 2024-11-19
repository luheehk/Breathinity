using System.IO.Ports; 
using UnityEngine;     

public class ArduinoInput : MonoBehaviour
{
    public string portName = "COM5"; // Set the COM port
    public int baudRate = 9600;

    private SerialPort serialPort;
    private float arduinoInputValue = 0f;

    void Start()
    {
        serialPort = new SerialPort(portName, baudRate);
        try
        {
            serialPort.Open();
        }
        catch (System.Exception e)
        {
            Debug.LogError("Failed to open serial port: " + e.Message);
        }
    }

    void Update()
    {
        if (serialPort != null && serialPort.IsOpen)
        {
            try
            {
                string data = serialPort.ReadLine();  // Read data
                if (float.TryParse(data, out float parsedValue))
                {
                    arduinoInputValue = parsedValue;
                }
            }
            catch (System.Exception e)
            {
                Debug.LogWarning("Serial read error: " + e.Message);
            }
        }
    }

    public float GetArduinoInputValue()
    {
        return arduinoInputValue;
    }

    private void OnApplicationQuit()
    {
        if (serialPort != null && serialPort.IsOpen)
        {
            serialPort.Close();
        }
    }
}
