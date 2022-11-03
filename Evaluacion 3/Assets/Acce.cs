using System;
using System.Collections;
using System.Collections.Generic;
using System.IO.Ports;
using UnityEngine;

public class Acce : MonoBehaviour
{
    private SerialPort _serialPort = new SerialPort();
    private byte[] buffer = new byte[16];

    private float inter = 0.08f;
    private float timer;
    
    public float qw, qx, qy, qz;
    void Start()
    {
        _serialPort.PortName = "/dev/ttyUSB0";
        _serialPort.BaudRate = 115200;
        _serialPort.DtrEnable = true;
        _serialPort.Open();
    }
    
    void Update()
    {
        if (_serialPort.BytesToRead >= 16)
        {
            _serialPort.Read(buffer, 0, 16);

            qw = BitConverter.ToSingle(buffer, 0);
            qx = BitConverter.ToSingle(buffer, 4);
            qy = BitConverter.ToSingle(buffer, 8);
            qz = BitConverter.ToSingle(buffer, 12);
      
            transform.rotation = new Quaternion(qx, -qy, -qz, qw);
        }
        timer += Time.deltaTime;
        if (timer > inter) timer = timer - inter;
    }
}