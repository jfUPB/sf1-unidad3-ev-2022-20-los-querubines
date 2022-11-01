using System.Collections;
using System.Collections.Generic;
using System.IO.Ports;
using UnityEngine;

public class Acce : MonoBehaviour
{
    
    SerialPort stream = new SerialPort("/dev/ttyUSB0", 115200);
    public string strReceived;
     
    public string[] strData = new string[4];
    public string[] strData_received = new string[4];
    public float qw, qx, qy, qz;
    void Start()
    {
        stream.DtrEnable = true;
        stream.Open(); //Open the Serial Stream.
    }

    // Update is called once per frame
    void Update()
    {
        strReceived = stream.ReadLine(); //Read the information  

        strData = strReceived.Split(','); 
        if (strData[0] != "" && strData[1] != "" && strData[2] != "" && strData[3] != "")
        {
            strData_received[0] = strData[0];
            strData_received[1] = strData[1];
            strData_received[2] = strData[2];
            strData_received[3] = strData[3];
         
            qw = float.Parse(strData_received[0]);
            qz = float.Parse(strData_received[1]);
            qx = float.Parse(strData_received[2]);
            qy = float.Parse(strData_received[3]);
      
            transform.rotation = new Quaternion(qx, -qy, -qz, qw);
    
        }      
    }
}