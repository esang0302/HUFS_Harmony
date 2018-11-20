using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO.Ports;

/*
public static class ExtensionMethods
{
    public static float Remap(this float value, float from1, float to1, float from2, float to2)
    // 값을 mapping 해주는 함수
    {
        return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
    }
}
*/
public class uni_serialcom : MonoBehaviour {
    string sensor = "";
    AudioSource source;
    AudioClip sound;
    AudioReverbFilter reverb = null;
    string instrument = "1";
    // 드럼으로 setting 해놓음
    // Use this for initialization
    SerialPort stream;


    // Use this for initialization
    void Start () {
        source = GetComponent<AudioSource>();
        sound = GetComponent<AudioClip>();
        reverb = GetComponent<AudioReverbFilter>();
        sound = source.clip;
        stream = new SerialPort("\\\\.\\COM1", 9600, Parity.None, 8, StopBits.One);

        if (!stream.IsOpen)
        {
            stream.Open(); // serial 연결 open
        }
        Debug.Log(instrument); // 일단 현재 무슨 악기인지 로그로 찍는다.

    }
	
	// Update is called once per frame
	void Update () {

        Pedal(instrument); // 페달 함수 호출
    }

    void Music(int in_state)
    {
        float vol = (float)((float)in_state / 1000.0); // 들어온 센서값이 100의 단위 int 형이라 float로 바꿔주기 위해
        // 100으로 나눠서 volume 값으로 바꾼다.
        source.volume = vol;
        //source.Play();
        source.clip = sound;
        source.PlayOneShot(sound, vol); // Play One shot으로 드럼 소리 출력
    }

    void ReverbEffect(Boolean state)
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
        if (stream.IsOpen) // serial 연결이 open 되어있다면
        {
            sensor = stream.ReadLine(); // 아두이노로부터 data 받아옴
                if (sensor != null && instrument == "1") // drum인 경우 kick sound 출력해주는 함수 call
                {
                    int data = int.Parse(sensor); // 아두이노로부터 받은 data를 int 형으로 parsing
                    Music(data); // 받은 int형 data값을 music 함수의 parameter로 넘겨 volume조절한다.
            }
                else if (sensor != null && instrument == "2") // piano인 경우 Reverb효과 주는 함수 call
                {
                    Boolean data = true; // reverb 상태를 true 로 넘김
                    ReverbEffect(data); // 함수 호출 > reverb 효과
              //      stream.ReadTimeout = 150;
                    Debug.Log(data);
                    /*
                    else if (sensor == "5") // Reverb를 off 하라는 msg 도착
                    {
                        Boolean data = false; // reverb 상태를 false로
                        ReverbEffect(data); // 함수 호출 > reverb off
                        stream.ReadTimeout = 150;
                        Debug.Log(data);
                    }*/
                }
                stream.BaseStream.Flush(); 
        }
    }

}
