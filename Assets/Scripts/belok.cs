using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;

public class belok : MonoBehaviour {
    SerialPort serial = new SerialPort("COM4", 9600);
    void Update()
    {
        if (!serial.IsOpen)
            serial.Open();

        int rotation = int.Parse(serial.ReadLine());
        transform.localEulerAngles = new Vector3(0, rotation, 0);
    }
}
