using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Audio;
public class sustain : MonoBehaviour {
    string sensor = "";
    int val;
    int loop = -1;
    float timeSpan;
    float checkTime;
    public AudioMixer audio;
    public bool isSustain;

    wrmhl myDevice = new wrmhl();
    // Use this for initialization
    string instrument = "2";
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
        isSustain = false;
        myDevice.set(portName, baudRate, ReadTimeout, QueueLenght); // This method set the communication with the following vars;
                                                                    // Serial Port, Baud Rates, Read Timeout and QueueLenght.
        myDevice.connect(); // This method open the Serial communication with the vars previously given.
        myDevice.send(instrument); // Send data to the device using thread.
    }

    // Update is called once per frame
    void Update()
    {
        sensor = myDevice.readQueue(); // 아두이노로부터 data 받아옴(string)
        val = Convert.ToInt32(sensor);
        if (val > 50 && !isSustain)
        {
            isSustain = true;
                Debug.Log("sustain Start!");
                loop = loop * -1;
                audio.SetFloat("Reverb", 1300);
            
        }
        else {
            audio.SetFloat("Reverb", -10000);
            isSustain = false;
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
