using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillator : MonoBehaviour {
    public double frequency = 440.0;
    private double increment;
    private double phase;
    private double sampling_frequency = 48000.0;
    public float gain;
    public float volume = 0.1f;

    public float[] frequencies = new float[36] {0, 65, 69, 73, 78, 82, 87, 92.5f, 98, 104, 110, 116.5f, 123.5f,
                                                    131,138.5f,147,155.5f,174.5f,185,196,207.5f,220,233,247,
                                                        261.5f,277,293.5f,311,330,349,370,392,415.5f,440,466,494};
    public int thisFreq = 1;

    public float makeSinWave()
    {
        return (float)(gain * Mathf.Sin((float)phase));
    }

    public float makeSquareWave()
    {
        if (gain * Mathf.Sin((float)phase) >= 0 * gain)
            return (float)gain * 0.6f;
        else
            return (-(float)gain) * 0.6f;
    }

    public float makeTriangleWave()
    {
        return (float)(gain * (double)Mathf.PingPong((float)phase, 1.0f));
    }
}
