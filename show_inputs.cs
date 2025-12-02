using System.Collections;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;
using UnityEngine.UI;

public class show_inputs : MonoBehaviour
{
    public Text text = null;
    public static ReadOnlyArray<InputDevice> devices => InputSystem.devices;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    
    {


        text = GetComponent<Text>();
        foreach (var device in devices)
        {
            InputSystem.EnableDevice(device);
            
            
        
    }
    }

    // Update is called once per frame
    void Update()
    {
        text.text = "";
        foreach (var device in devices) {
          Debug.Log(device.displayName + device.magnitude.ToString("F2"));
          text.text += device.displayName + " " +device.allControls[0].ReadValueAsObject().ToString() + "\n";

        }
    }
}
