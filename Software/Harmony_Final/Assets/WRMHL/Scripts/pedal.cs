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

public class pedal : MonoBehaviour {
    string sensor = "";
    AudioSource source;
    AudioClip sound;
    AudioReverbFilter reverb = null;
    string instrument;
    int maximum = 0;
    bool data = false;

    wrmhl myDevice = new wrmhl(); // wrmhl is the bridge beetwen your computer and hardware.

	[Tooltip("SerialPort of your device.")]
	public string portName = "COM3";

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
        myDevice.send(instrument); // Send data to the device using thread.
        instrument = "1";
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

    void Music(int volume)
    {
        float vol = (float)((float)volume / 200.0); // 들어온 센서값이 100의 단위 int 형이라 float로 바꿔주기 위해
        // 100으로 나눠서 volume 값으로 바꾼다.
        source.volume = vol;
        //source.Play();
        source.clip = sound;
        source.PlayOneShot(sound, vol); // Play One shot으로 드럼 소리 출력
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
                print(sensor);
            }

    }
   
}
