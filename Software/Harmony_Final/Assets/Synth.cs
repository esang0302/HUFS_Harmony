using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class Synth : MonoBehaviour {
    private Vector3 initialPosition;
    float MaxZRotation = 5.0f;
    float ResetPlayTime;
    bool collided;
    string wave;
    public Button synth;
    public Button sine;
    public Button pulse;
    public Button triangle;
    public Button modeChange;

    public Slider pwmSlider;
    public Slider harSlider;
    public Slider LFOSlider;

    bool isLeap;
    public float triValue = 1;
    public float pwmValue = 0.5f;
    public float harmonicValue = 0;

    private float LFOvalue;
    private double LFOincrement;
    private double increment;
    private float frequency;
    float synthVol;
    public float gain;

    public string myKey;
    public double phase = 0;
    public double phaseTest1 = 0;
    public double phaseTest2 = 0;
    public double phaseTest3 = 0;
    public double phaseTest4 = 0;
    private double sampling_frequency = 48000.0;
    private AudioSource audio;
    public float[] frequencies = new float[37] {0, 65, 69, 73, 78, 82, 87, 92.5f, 98, 104, 110, 116.5f, 123.5f,
                                                    131, 138.5f, 147, 155.5f, 167, 174.5f, 185, 196, 207.5f, 220, 233, 247,
                                                        261.5f, 277, 293.5f, 311, 330, 349, 370, 392, 415.5f, 440, 466, 494};
    public string[] key = new string[17] { "a", "w", "s", "e", "d", "f", "t", "g", "y", "h", "u", "j", "k", "o", "l", "p", ";" };
    private void Awake()
    {
        
        collided = false;
        Physics.IgnoreLayerCollision(LayerMask.NameToLayer("piano"), LayerMask.NameToLayer("piano"), true);
        initialPosition = transform.position;
    }

    // Use this for initialization
    void Start () {
        modeChange.GetComponentInChildren<Text>().text = "Leap motion\nmode";

        if (Int32.Parse(gameObject.name) > 12 && Int32.Parse(gameObject.name) < 30)
            myKey = key[Int32.Parse(gameObject.name) - 13];
        else
            myKey ="0";

        isLeap = true;
        harmonicValue = 0;
        frequency = frequencies[Int32.Parse(gameObject.name)];
        ResetPlayTime = 0.4f;
        audio = GetComponent<AudioSource>();

        wave = "sin";

        sine.onClick.AddListener(selectSineWave);
        pulse.onClick.AddListener(selectPulseWave);
        triangle.onClick.AddListener(selectTriWave);
        modeChange.onClick.AddListener(isleap);
        synth.onClick.AddListener(selectSineWave);
    }

    // Update is called once per frame
    void Update () {
        harmonicValue = harSlider.value;
        LFOvalue = LFOSlider.value;

        if (isLeap)
            Leap();
        else
            NotLeap();
        if (transform.rotation.eulerAngles.z > 0.2f && !collided)
        {
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + new Vector3(0, 0, -0.2f));
            GetComponent<Rigidbody>().angularDrag = 0;
            GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        }
        //Can't over the limit of pressing 
        if (transform.rotation.eulerAngles.z > MaxZRotation)
        {
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, MaxZRotation);
            GetComponent<Rigidbody>().angularDrag = 0;
            GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        }
        //Can't over the limit of origin state
        if (transform.rotation.eulerAngles.z > 350 || transform.rotation.eulerAngles.z < 0)
        {
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, 0);
            GetComponent<Rigidbody>().angularDrag = 0;
            GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        }

        transform.position = initialPosition;
        GetComponent<Rigidbody>().velocity = Vector3.zero;
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "target")
            collided = false;

    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "target")
            collided = true;
    }
    void isleap()
    {
        isLeap = !isLeap;
        if (!isLeap)
            modeChange.GetComponentInChildren<Text>().text = "Keyboard\nmode";
        else
        {
            modeChange.GetComponentInChildren<Text>().text = "Leap motion\nmode";
        }
        
    }

    void Leap()
    {
        if (transform.rotation.eulerAngles.z > 1.5f)
        {
            audio.PlayOneShot(audio.clip, 1f);
            gain = 0.5f;
        }
        else
           gain = 0;
    }
    void NotLeap()
    {
        if (Input.GetKeyDown(myKey))
        {
            audio.PlayOneShot(audio.clip, 1f);
            gain = 0.5f;
        }

        if (Input.GetKeyUp(myKey))
        {
            gain = 0;
        }
    }
    public void OnAudioFilterRead(float[] data, int channels)
    {
        increment = frequency * 2.0 * Mathf.PI / sampling_frequency;
        LFOincrement = LFOvalue * 2.0 * Mathf.PI / sampling_frequency;
        for (int i = 0; i < data.Length; i += channels)
        {
            phase += LFOincrement;
            phaseTest1 += increment * 1 * makeLFO();
            phaseTest2 += increment * 2 * makeLFO();
            phaseTest3 += increment * 3 * makeLFO();

            if (wave == "sin")
                data[i] = (1 - harmonicValue) * makeSinWave(1, phaseTest1) + harmonicValue * (makeSinWave(2, phaseTest2) + makeSinWave(3, phaseTest3));
            else if (wave == "squ")
                data[i] = (1 - harmonicValue) * makeSquareWave(1, phaseTest1) + harmonicValue * (makeSquareWave(2, phaseTest2) + makeSquareWave(3, phaseTest3));
            else if(wave == "tri")
                data[i] = (1 - harmonicValue) * makeTriangleWave(1, phaseTest1) + harmonicValue * (makeTriangleWave(2, phaseTest2) + makeTriangleWave(3, phaseTest3));

            if (channels == 2)
            {
                data[i + 1] = data[i];
            }
            if (phase > (Mathf.PI * 2))
            {
                phase = 0.0;
                phaseTest1 = 0;
                phaseTest2 = 0;
                phaseTest3 = 0;
                phaseTest4 = 0;
            }
        }
    }
    public void selectSineWave()
    {
        Debug.Log("Sine");
        wave = "sin";
    }

    public void selectPulseWave()
    {
        Debug.Log("pulse");
        wave = "squ";
    }

    public void selectTriWave()
    {
        Debug.Log("Triangle");
        wave = "tri";
    }

    public float makeLFO()
    {
        if (LFOvalue == 0)
            return 1;
        else
            return (float)(Mathf.Sin((float)phase));
    }



    //make waveform
    public float makeSinWave(double harmonic, double p)
    {
        return (float)(gain /harmonic* Mathf.Sin((float)p));
    }

    public float makeSquareWave(double harmonic, double p)
    {
        if (Mathf.Sin((float)p) >= 0)
            return (float) (gain / harmonic);
        else
            return 0;
    }

    public float makeTriangleWave(double harmonic, double p)
    {
        return (float)(gain/harmonic * (double)Mathf.PingPong((float)p, 1));
    }
}
