using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO.Ports;
using System.Security.Principal;

public class arduino1 : MonoBehaviour
{
    string sensor = "";
    AudioSource source;
    AudioClip sound;
    AudioReverbFilter reverb = null;
    string instrument = "1";
    // 드럼으로 setting 해놓음
    // Use this for initialization
    SerialPort stream;


    void Start()
    {
        source = GetComponent<AudioSource>();
        sound = GetComponent<AudioClip>();
        reverb = GetComponent<AudioReverbFilter>();
        sound = source.clip;
        stream = new SerialPort("\\\\.\\COM1", 9600, Parity.None, 8, StopBits.One);

        
            if (!stream.IsOpen)
            {
                stream.Open(); // serial 연결 open
            }
            stream.Write(instrument); // 처음 무슨 악기를 골랐는지 아두이노에게 알림
            Debug.Log(instrument);

        
        
        if (stream.IsOpen) // serial 연결이 open 되어있다면
        {
            try
            {
                sensor = stream.ReadLine(); // 아두이노로부터 data 받아옴
                                            //stream.ReadTimeout = 50;
                if (sensor != null && instrument == "1") // drum인 경우 kick sound 출력해주는 함수 call
                {
                    float data = float.Parse(sensor); // string으로 들어온 값을 float로 형변환
                    Music(data); // float값을 volume 조절을 위해 parameter로
                    stream.ReadTimeout = 150; // 0.15초 안에 read 되지 않으면 exception 처리
                    Debug.Log(data);
                }
                else if (sensor != null && instrument == "2") // piano인 경우 Reverb효과 주는 함수 call
                {
                    if (sensor == "4") // Reverb를 on하라는 msg 도착
                    {
                        Boolean data = true; // reverb 상태를 true 로 넘김
                        ReverbEffect(data); // 함수 호출 > reverb 효과
                        stream.ReadTimeout = 150;
                        Debug.Log(data);
                    }
                    else if (sensor == "5") // Reverb를 off 하라는 msg 도착
                    {
                        Boolean data = false; // reverb 상태를 false로
                        ReverbEffect(data); // 함수 호출 > reverb off
                        stream.ReadTimeout = 150;
                        Debug.Log(data);
                    }
                }
                stream.BaseStream.Flush();
            }
            catch (TimeoutException e) // timeout으로 발생하는 exception handling
            {
                Debug.Log(e);
            }

        }

    }

    void Update()
    {
    }
    /*
    void OnApplicationQuit() // app 끌때
    {
        if (stream.IsOpen) // serial 포트가 열려있다면
        {
            stream.Close(); // 닫아줘
        }
    }
    */

    

    void Music(float in_state)
    {
        source.volume = in_state;
        //source.Play();
        //source.clip = sound;
        source.PlayOneShot(sound, in_state);
        //source.bypassReverbZones = true;
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
    /*
    void SerialCom()
    {
        if (!stream.IsOpen)
        {
            stream.Open(); // serial 연결 open
        }
        stream.Write(instrument); // 처음 무슨 악기를 골랐는지 아두이노에게 알림
        Debug.Log(instrument);
    }

    void Pedal(string instrument)
    {

        if (stream.IsOpen) // serial 연결이 open 되어있다면
        {
            try
            {
                sensor = stream.ReadLine(); // 아두이노로부터 data 받아옴
                                            //stream.ReadTimeout = 50;
                if (sensor != null && instrument == "1") // drum인 경우 kick sound 출력해주는 함수 call
                {
                    float data = float.Parse(sensor); // string으로 들어온 값을 float로 형변환
                    Music(data); // float값을 volume 조절을 위해 parameter로
                    stream.ReadTimeout = 150; // 0.15초 안에 read 되지 않으면 exception 처리
                    Debug.Log(data);
                }
                else if (sensor != null && instrument == "2") // piano인 경우 Reverb효과 주는 함수 call
                {
                    if (sensor == "4") // Reverb를 on하라는 msg 도착
                    {
                        Boolean data = true; // reverb 상태를 true 로 넘김
                        ReverbEffect(data); // 함수 호출 > reverb 효과
                        stream.ReadTimeout = 150;
                        Debug.Log(data);
                    }
                    else if (sensor == "5") // Reverb를 off 하라는 msg 도착
                    {
                        Boolean data = false; // reverb 상태를 false로
                        ReverbEffect(data); // 함수 호출 > reverb off
                        stream.ReadTimeout = 150;
                        Debug.Log(data);
                    }
                }
                stream.BaseStream.Flush();
            }
            catch (TimeoutException e) // timeout으로 발생하는 exception handling
            {
                Debug.Log(e);
            }

        }
    }

    */
}