using System.Collections;
using System.Collections.Generic;
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

public class Kickpedal : MonoBehaviour {
    string sensor = "";
    AudioSource source;
    AudioClip sound;
    AudioReverbFilter reverb = null;
    string instrument;
    int maximum = 0;
    bool data = false;

    wrmhl myDevice = new wrmhl(); // wrmhl is the bridge beetwen your computer and hardware.

	[Tooltip("SerialPort of your device.")]
	public string portName = "COM1";

	[Tooltip("Baudrate")]
	public int baudRate = 9600;

	[Tooltip("Timeout")]
	public int ReadTimeout = 1000;

	[Tooltip("QueueLenght")]
	public int QueueLenght = 1;

	void Start () {
        source = GetComponent<AudioSource>();
        sound = GetComponent<AudioClip>();
        sound = source.clip;
        myDevice.set (portName, baudRate, ReadTimeout, QueueLenght); // This method set the communication with the following vars;
		//                              Serial Port, Baud Rates, Read Timeout and QueueLenght.
		myDevice.connect(); // This method open the Serial communication with the vars previously given.
        myDevice.send("Drum"); // Send data to the device using thread.
    }

	// Update is called once per frame
	void Update () {
        sensor = myDevice.readQueue(); // 아두이노로부터 data 받아옴(string)

        int data = int.Parse(sensor); // 아두이노로부터 받은 data를 int 형으로 parsing
        if(data != 0)
        {
            Music(data);
            
        }
    }

	void OnApplicationQuit() { // close the Thread and Serial Port
		myDevice.close();
        
	}

    public static float Remap(float value, float from1, float to1, float from2, float to2)
    {
        return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
    }

    void Music(int volume)
    {
        float vol = Remap((float)(volume / 200.0), 0, 0.6f, 0.3f, 1f); // 들어온 센서의 최댓값이 200이여서 linear하게 mapping
        source.volume = vol;
        source.clip = sound;
        source.PlayOneShot(sound, vol); // Play One shot으로 드럼 소리 출력
        //print(vol);
    }

    void Pedal(string instrument)
    {
            if (sensor != "0")
            {
                int data = int.Parse(sensor); // 아두이노로부터 받은 data를 int 형으로 parsing

                if (maximum <= data)
                {
                    maximum = data;
                }
                if (maximum > data)
                {
                    Music(maximum);
                    maximum = 0;
                }

                 // 받은 int형 data값을 music 함수의 parameter로 넘겨 volume조절한다.
                //print(sensor);
            }

    }
   
}
