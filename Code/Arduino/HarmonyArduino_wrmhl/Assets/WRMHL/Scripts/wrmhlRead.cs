/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

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
/*
public class wrmhlRead : MonoBehaviour {
    string sensor = "";
    AudioSource source;
    public AudioClip sound;
    public AudioClip Psound;
    AudioReverbFilter reverb = null;
    int flag = 0;
    string instrument = "1";

	wrmhl myDevice = new wrmhl(); // wrmhl is the bridge beetwen your computer and hardware.

	[Tooltip("SerialPort of your device.")]
	public string portName = "COM6";

	[Tooltip("Baudrate")]
	public int baudRate = 250000;


	[Tooltip("Timeout")]
	public int ReadTimeout = 200;

	[Tooltip("QueueLenght")]
	public int QueueLenght = 1;

	void Start () {
		myDevice.set (portName, baudRate, ReadTimeout, QueueLenght); // This method set the communication with the following vars;
		//                              Serial Port, Baud Rates, Read Timeout and QueueLenght.
		myDevice.connect (); // This method open the Serial communication with the vars previously given.
	}
    void Music(int in_state)
    {
        float vol = (float)((float)in_state / 1000.0); // 들어온 센서값이 100의 단위 int 형이라 float로 바꿔주기 위해
        // 100으로 나눠서 volume 값으로 바꾼다.
        source.volume = vol * 10;
        //source.Play();
        source.clip = sound;
        source.PlayOneShot(sound, vol); // Play One shot으로 드럼 소리 출력
    }

    void ReverbEffect(Boolean state)
    {
        if (state) // Reverb 효과 on
        {
            reverb.reverbLevel = 2000; // Reverb 효과 최대
            source.PlayOneShot(Psound, 0.7f); // Reverb 효과 주면 소리 커지니까 좀 줄임.
        }
        else if (!state) // Reverb 효과 off
        {
            reverb.reverbLevel = -10000; // Reverb 효과 없음
            source.volume = 1f;
            //source.Play();
            source.PlayOneShot(Psound, 1f);
        }
    }
    // Update is called once per frame
    void Update () {
        sensor = myDevice.readQueue();
        Debug.Log(sensor);
        if (sensor != null && instrument == "1") // drum인 경우 kick sound 출력해주는 함수 call
        {
            int data = int.Parse(sensor); // 아두이노로부터 받은 data를 int 형으로 parsing
            Music(data); // 받은 int형 data값을 music 함수의 parameter로 넘겨 volume조절한다.
        }
        if (instrument == "2" && sensor != null) // piano인 경우 Reverb효과 주는 함수 call
        {
            if (flag == 1)
            {
                Boolean data = false;
                ReverbEffect(data);
                Debug.Log(data);
                flag = 0;
            }
            else
            {
                Boolean data = true; // reverb 상태를 true 로 넘김
                ReverbEffect(data); // 함수 호출 > reverb 효과
                                    //      stream.ReadTimeout = 150;
                Debug.Log(data);
                flag = 1;
            }
        }
            //print (myDevice.readQueue () ); // myDevice.read() return the data coming from the device using thread.
	} 
    

	void OnApplicationQuit() { // close the Thread and Serial Port
		myDevice.close();
	}
}*/

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

public class wrmhlRead : MonoBehaviour
{
    string sensor = "";
    AudioSource source;
    AudioClip sound;
    AudioReverbFilter reverb = null;
    string instrument = "1";

    wrmhl myDevice = new wrmhl(); // wrmhl is the bridge beetwen your computer and hardware.

    [Tooltip("SerialPort of your device.")]
    public string portName = "COM6";

    [Tooltip("Baudrate")]
    public int baudRate = 250000;

    [Tooltip("Timeout")]
    public int ReadTimeout = 1000;

    [Tooltip("QueueLenght")]
    public int QueueLenght = 50;

    void Start()
    {
        source = GetComponent<AudioSource>();
        sound = GetComponent<AudioClip>();
        reverb = GetComponent<AudioReverbFilter>();
        //sound = source.clip;
        myDevice.set(portName, baudRate, ReadTimeout, QueueLenght); // This method set the communication with the following vars;
                                                                    //                              Serial Port, Baud Rates, Read Timeout and QueueLenght.
        myDevice.connect(); // This method open the Serial communication with the vars previously given.
        print(instrument); // 일단 현재 무슨 악기인지 로그로 찍는다.
    }

    // Update is called once per frame
    void Update()
    {
        Pedal(instrument); // 페달 함수 호출
    }

    void OnApplicationQuit()
    { // close the Thread and Serial Port
        myDevice.close();
    }

    void Music(int volume)
    {
        float vol = (float)((float)volume / 600.0); // 들어온 센서값이 100의 단위 int 형이라 float로 바꿔주기 위해
        // 100으로 나눠서 volume 값으로 바꾼다.
        source.volume = vol;
        //source.Play();
        source.clip = sound;
        source.PlayOneShot(sound, vol); // Play One shot으로 드럼 소리 출력
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
        sensor = myDevice.readQueue(); // 아두이노로부터 data 받아옴(string)
        print(sensor);
        if (sensor != null && instrument == "1") // drum인 경우 kick sound 출력해주는 함수 call
        {
            int data = int.Parse(sensor); // 아두이노로부터 받은 data를 int 형으로 parsing
            print("h");
            //Music(data); // 받은 int형 data값을 music 함수의 parameter로 넘겨 volume조절한다.
            //print(data); // 일단 현재 무슨 악기인지 로그로 찍는다.
        }
        else if (sensor != null && instrument == "2") // piano인 경우 Reverb효과 주는 함수 call
        {
            bool data = true; // reverb 상태를 true 로 넘김
            ReverbEffect(data); // 함수 호출 > reverb 효과
                                //      stream.ReadTimeout = 150;
            print(data); // 일단 현재 무슨 악기인지 로그로 찍는다.
            /*
            else if (sensor == "5") // Reverb를 off 하라는 msg 도착
            {
                Boolean data = false; // reverb 상태를 false로
                ReverbEffect(data); // 함수 호출 > reverb off
                stream.ReadTimeout = 150;
                Debug.Log(data);
            }*/
        }
    }
}
