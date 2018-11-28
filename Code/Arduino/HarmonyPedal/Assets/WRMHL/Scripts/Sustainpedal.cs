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

public class Sustainpedal : MonoBehaviour
{
    string sensor = "";
    AudioSource source;
    AudioClip sound;
    AudioReverbFilter reverb = null;
    string instrument;
    bool data = false;

    wrmhl myDevice = new wrmhl(); // wrmhl is the bridge beetwen your computer and hardware.

    [Tooltip("SerialPort of your device.")]
    public string portName = "COM6";

    [Tooltip("Baudrate")]
    public int baudRate = 9600;

    [Tooltip("Timeout")]
    public int ReadTimeout = 1000;

    [Tooltip("QueueLenght")]
    public int QueueLenght = 1;

    void Start()
    {
        source = GetComponent<AudioSource>();
        sound = GetComponent<AudioClip>();
        reverb = GetComponent<AudioReverbFilter>();
        sound = source.clip;
        myDevice.set(portName, baudRate, ReadTimeout, QueueLenght); // This method set the communication with the following vars;
                                                                    //                              Serial Port, Baud Rates, Read Timeout and QueueLenght.
        myDevice.connect(); // This method open the Serial communication with the vars previously given.
        myDevice.send("Keyboard"); // Send data to the device using thread.
    }

    // Update is called once per frame
    void Update()
    {
        sensor = myDevice.readQueue(); // 아두이노로부터 data 받아옴(string)
        if (sensor != "0")
        {
            data = true; // reverb 상태를 true 로 넘김
        }
        ReverbEffect(data); // 함수 호출 > reverb 효과
    }

    void OnApplicationQuit()
    { // close the Thread and Serial Port
        myDevice.close();
    }

    void ReverbEffect(bool state)
    {
        if (state) // Reverb 효과 on
        {
            reverb.reverbLevel = 900; // Reverb 효과 최대
            reverb.decayTime = 8;
            source.volume = 0.5f; // Reverb 효과 주면 소리 커지니까 좀 줄임.
            data = false;
        }
        else if (!state) // Reverb 효과 off
        {
            reverb.decayTime = 1; // Reverb 효과 없음
            source.volume = 1f;
        }
    }
    /* 키보드가 없을때 testing 하기 위한 객체 충돌 (마치 건반을 누르는 것으로 가정)
    void OnCollisionEnter(Collision other)
    {
        if (other.transform.tag == "Player")
        {
            if (reverb.reverbLevel == 0)
            {
                //reverb.reverbLevel = -10000; // Reverb 효과 없음
                //source.volume = 1f;
                //source.Play();
                source.PlayOneShot(sound, 1f);
            }
            else if (reverb.reverbLevel == 2000)
            {
                //reverb.reverbLevel = 2000; // Reverb 효과 최대
                source.volume = 0.0005f;
                source.PlayOneShot(sound, 0.0005f); // Reverb 효과 주면 소리 커지니까 좀 줄임.
            }
            source.PlayOneShot(sound, 1f);
        }
    }
    */
}
