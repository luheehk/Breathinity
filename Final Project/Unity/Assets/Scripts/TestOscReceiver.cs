// using UnityEngine;
// using ExtOSC;

// public class TestOscReceiver : MonoBehaviour
// {
//     [SerializeField] private int port = 8000;

//     private OSCReceiver receiver;

//     private void Start()
//     {
//         receiver = gameObject.AddComponent<OSCReceiver>();
//         receiver.LocalPort = port;

//         receiver.Bind("/example", OnMessageReceived);
//         Debug.Log("Listening for OSC messages on port " + port);
//     }

//     private void OnMessageReceived(OSCMessage message)
//     {
//         Debug.Log("Received OSC Message: " + message);
//     }
// }
