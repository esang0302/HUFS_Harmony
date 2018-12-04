using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class LoopStation : MonoBehaviour
{
    #region Fields, Properties, and Inner Classes
    // constants for the wave file header
    private const int HEADER_SIZE = 44;
    private const short BITS_PER_SAMPLE = 16;
    private const int SAMPLE_RATE = 48100;

    // the number of audio channels in the output file
    private int channels = 2;

    // the audio stream instance
    private MemoryStream outputStream;
    private BinaryWriter outputWriter;
    public int click = -1;
    int count = 0;

    AudioSource audio;
    AudioClip loadedClip;
    string date;
    string loopfile;
    // should this object be rendering to the output stream?
    public bool Rendering = false;

    /// The status of a render
    public enum Status
    {
        UNKNOWN,
        SUCCESS,
        FAIL,
        ASYNC
    }

    /// The result of a render.
    public class Result
    {
        public Status State;
        public string Message;

        public Result(Status newState = Status.UNKNOWN, string newMessage = "")
        {
            this.State = newState;
            this.Message = newMessage;
        }
    }
    #endregion

    public LoopStation()
    {
        this.Clear();
    }

    // reset the renderer
    public void Clear()
    {
        this.outputStream = new MemoryStream();
        this.outputWriter = new BinaryWriter(outputStream);
    }

    /// Write a chunk of data to the output stream.
    /*public void Write(float[] audioData)
    {
        //Debug.Log(audioData.Length);
        // Convert numeric audio data to bytes
        for (int i = 0; i < audioData.Length; i++)
        {
            // write the short to the stream
            this.outputWriter.Write((short)(audioData[i] * (float)Int16.MaxValue));
        }
    }*/

    // write the incoming audio to the output string
    void OnAudioFilterRead(float[] data, int channels)
    {
        if (this.Rendering)
        {/*
            // store the number of channels we are rendering
            this.channels = channels;

            // store the data stream
            //this.Write(data);
         */
            for (int i = 0; i < data.Length; i++)
            {
                // write the short to the stream
                this.outputWriter.Write((short)(data[i] * (float)Int16.MaxValue));
            }

        }

    }

    public void StartRecord()
    {
        Rendering = true;
    }

    public void SaveRecord()
    {
        Rendering = false;
        Save(Directory.GetCurrentDirectory() + "\\Harmony_Data\\resources\\test.wav");
        //Save("test.wav");
        Clear();
    }

    public void LoopStart()
    {
        click = click * -1;
        if (click > 0 && count == 0)
        {
            Rendering = true;
            loopfile = DateTime.Now.ToString("yyyy-MM-dd-HH-mm");
        }
        else if (click < 0)
        {
            Rendering = false;
            //Save(Directory.GetCurrentDirectory() + "\\Assets\\resources\\"+ loopfile + " loop.wav");             //unity
            Save(Directory.GetCurrentDirectory() + "\\Harmony_Data\\resources\\"+ loopfile + " loop.wav");     //build
            Clear();
            StartCoroutine(loadAudio());

        }
    }
    public IEnumerator loadAudio()
    {
        //WWW audio_loader = new WWW("file:///" + "\\Assets\\resources\\" + loopfile + " loop.wav");   //unity
        WWW audio_loader = new WWW("file:///" + "\\Harmony_Data\\resources\\" + loopfile + " loop.wav");    //build
        while (!audio_loader.isDone)
            Debug.Log(audio_loader.progress);
        yield return audio_loader;

        Debug.Log(audio_loader.progress);
        Debug.Log(audio_loader.url);
        loadedClip = audio_loader.GetAudioClip(false, false, AudioType.WAV);
        audio = GameObject.Find("track3_audio").GetComponent<AudioSource>();
        audio.clip = loadedClip;
        audio.Play();
    }

    public void LoopTermination()
    {
        date = DateTime.Now.ToString("yyyy-MM-dd-HH-mm");
        Rendering = false;
        //Save(Directory.GetCurrentDirectory() + "\\Assets\\resources\\LoopMusic" + date + ".wav");     //unity
        Save(Directory.GetCurrentDirectory() + "\\Harmony_Data\\resources\\" + date + ".wav");          //build
        count = 0;
    }



    #region File I/O
    public LoopStation.Result Save(string filename)
    {
        Result result = new LoopStation.Result();

        if (outputStream.Length > 0)
        {
            Debug.Log(outputStream.Length);
            // add a header to the file so we can send it to the SoundPlayer
            this.AddHeader();

            // if a filename was passed in
            if (filename.Length > 0)
            {
                // Save to a file. Print a warning if overwriting a file.
                if (File.Exists(filename))
                    Debug.LogWarning("Overwriting " + filename + "...");

                // reset the stream pointer to the beginning of the stream
                outputStream.Position = 0;

                // write the stream to a file
                FileStream fs = File.OpenWrite(filename);

                this.outputStream.WriteTo(fs);

                fs.Close();

                // for debugging only
                Debug.Log("Finished saving to " + filename + ".");
            }

            result.State = Status.SUCCESS;
        }
        else
        {
            Debug.LogWarning("There is no audio data to save!");

            result.State = Status.FAIL;
            result.Message = "There is no audio data to save!";
        }

        return result;
    }

    /// This generates a simple header for a canonical wave file, 
    /// which is the simplest practical audio file format. It
    /// writes the header and the audio file to a new stream, then
    /// moves the reference to that stream.
    /// 
    /// See this page for details on canonical wave files: 
    /// http://www.lightlink.com/tjweber/StripWav/Canon.html
    private void AddHeader()
    {
        // reset the output stream
        outputStream.Position = 0;

        // calculate the number of samples in the data chunk
        long numberOfSamples = outputStream.Length / (BITS_PER_SAMPLE / 8);

        // create a new MemoryStream that will have both the audio data AND the header
        MemoryStream newOutputStream = new MemoryStream();
        BinaryWriter writer = new BinaryWriter(newOutputStream);

        writer.Write(0x46464952); // "RIFF" in ASCII

        // write the number of bytes in the entire file
        writer.Write((int)(HEADER_SIZE + (numberOfSamples * BITS_PER_SAMPLE * channels / 8)) - 8);

        writer.Write(0x45564157); // "WAVE" in ASCII
        writer.Write(0x20746d66); // "fmt " in ASCII
        writer.Write(16);

        // write the format tag. 1 = PCM
        writer.Write((short)1);

        // write the number of channels.
        writer.Write((short)channels);

        // write the sample rate. 44100 in this case. The number of audio samples per second
        writer.Write(SAMPLE_RATE);

        writer.Write(SAMPLE_RATE * channels * (BITS_PER_SAMPLE / 8));
        writer.Write((short)(channels * (BITS_PER_SAMPLE / 8)));

        // 16 bits per sample
        writer.Write(BITS_PER_SAMPLE);

        // "data" in ASCII. Start the data chunk.
        writer.Write(0x61746164);

        // write the number of bytes in the data portion
        writer.Write((int)(numberOfSamples * BITS_PER_SAMPLE * channels / 8));

        // copy over the actual audio data
        this.outputStream.WriteTo(newOutputStream);

        // move the reference to the new stream
        this.outputStream = newOutputStream;
    }
    #endregion
}
