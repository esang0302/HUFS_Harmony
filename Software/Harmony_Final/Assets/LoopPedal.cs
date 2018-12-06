using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

/*
This script is used to read all the data coming from the device. For instance,
If arduino send ->
								{"1",
								"2",
								"3",}
readQueue() will return ->
								"1", for the first call
								"2", for the second call
								"3", for the thirst call

This is the perfect script for integration that need to avoid data loose.
If you need speed and low latency take a look to wrmhlReadLatest.
*/

public class LoopPedal : MonoBehaviour
{
    string sensor = "";
    int val;
    int loop = -1;
    float timeSpan;
    float checkTime;

    string instrument = "3";

    wrmhl myDevice = new wrmhl(); // wrmhl is the bridge beetwen your computer and hardware.

    [Tooltip("SerialPort of your device.")]
    public string portName = "COM3";

    [Tooltip("Baudrate")]
    public int baudRate = 9600;

    [Tooltip("Timeout")]
    public int ReadTimeout = 500;

    [Tooltip("QueueLenght")]
    public int QueueLenght = 50;

    void Start()
    {
        myDevice.set(portName, baudRate, ReadTimeout, QueueLenght); // This method set the communication with the following vars;
                                                                    // Serial Port, Baud Rates, Read Timeout and QueueLenght.
        myDevice.connect(); // This method open the Serial communication with the vars previously given.
        myDevice.send(instrument); // Send data to the device using thread.
        timeSpan = 0.0f;
        checkTime = 0.5f; //2sec
    }

    // Update is called once per frame
    void Update()
    {
        timeSpan += Time.deltaTime;

        sensor = myDevice.readQueue(); // 아두이노로부터 data 받아옴(string)
        val = Convert.ToInt32(sensor);
        if (val > 50)
        {
            if (timeSpan >= checkTime)
            {
                Debug.Log("Loop Start!");
                loop = loop * -1;
                GameObject.Find("Main Camera").GetComponent<LoopStation>().LoopStart();
                timeSpan = 0.0f;
            }
        }
    }
    public void disconnection()
    {
        myDevice.close();

    }
    void OnApplicationQuit()
    { // close the Thread and Serial Port
        myDevice.close();

    }
}
