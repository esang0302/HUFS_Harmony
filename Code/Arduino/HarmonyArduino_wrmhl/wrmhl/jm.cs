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

public class jm : MonoBehaviour {
    string sensor = "";
    AudioSource source;
    AudioClip sound;
    AudioReverbFilter reverb = null;
    string instrument = "1";

    wrmhl myDevice = new wrmhl(); // wrmhl is the bridge beetwen your computer and hardware.

	[Tooltip("SerialPort of your device.")]
	public string portName = "COM6";

	[Tooltip("Baudrate")]
	public int baudRate = 9600;

	[Tooltip("Timeout")]
	public int ReadTimeout = 1000;

	[Tooltip("QueueLenght")]
	public int QueueLenght = 50;

	void Start () {
        source = GetComponent<AudioSource>();
        sound = GetComponent<AudioClip>();
        reverb = GetComponent<AudioReverbFilter>();
        sound = source.clip;
        myDevice.set (portName, baudRate, ReadTimeout, QueueLenght); // This method set the communication with the following vars;
		//                              Serial Port, Baud Rates, Read Timeout and QueueLenght.
		myDevice.connect(); // This method open the Serial communication with the vars previously given.
        myDevice.send(instrument); // Send data to the device using thread.
    }

	// Update is called once per frame
	void Update () {
        sensor = myDevice.readQueue(); // 아두이노로부터 data 받아옴(string)
        if (sensor != "")
        {
            Pedal(instrument); // 페달 함수 호출
        }
    }

	void OnApplicationQuit() { // close the Thread and Serial Port
		myDevice.close();
	}

    void Music(int volume)
    {
        float vol = (float)((float)volume / 600.0); // 들어온 센서값이 100의 단위 int 형이라 float로 바꿔주기 위해
        // 100으로 나눠서 volume 값으로 바꾼다.
        source.volume = vol;
        source.Play();
        //source.clip = sound;
        //source.PlayOneShot(sound, vol); // Play One shot으로 드럼 소리 출력
    }

    void ReverbEffect(bool state)
    {
        if (state) // Reverb 효과 on
        {
            reverb.reverbLevel = 2000; // Reverb 효과 최대
            source.PlayOneShot(sound, 0.7f); // Reverb 효과 주면 소리 커지니까 좀 줄임.
        }
        else if (!state) // Reverb 효과 off
        {
            reverb.reverbLevel = 0; // Reverb 효과 없음
            //source.volume = 1f;
            //source.Play();
            source.PlayOneShot(sound, 1f);
        }
    }

    void Pedal(string instrument)
    {
        print(sensor);
        if (instrument == "1") // drum인 경우 kick sound 출력해주는 함수 call
        {
            int data = int.Parse(sensor); // 아두이노로부터 받은 data를 int 형으로 parsing
            Music(data); // 받은 int형 data값을 music 함수의 parameter로 넘겨 volume조절한다.
            
        }
        else if (sensor != null && instrument == "2") // piano인 경우 Reverb효과 주는 함수 call
        {
            bool data = true; // reverb 상태를 true 로 넘김
            ReverbEffect(data); // 함수 호출 > reverb 효과
                                //      stream.ReadTimeout = 150;
           
        }
    }
}
