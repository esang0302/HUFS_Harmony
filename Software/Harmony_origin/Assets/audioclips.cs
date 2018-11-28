using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audioclips : MonoBehaviour {

    public AudioClip[] audioClips;
    public bool isDead { get; private set; }

    private AudioSource[] _sources;

    void Awake()
    {
        _sources = new AudioSource[audioClips.Length];
        for (int i = 0; i < audioClips.Length; i++)
        {
            _sources[i] = gameObject.AddComponent<AudioSource>();
            _sources[i].clip = audioClips[i];
            // set up the properties such as distance for 3d sounds here if you need to.
        }
    }

    public void PlaySound(int clipIdx)
    {
        if (clipIdx >= 0 && clipIdx < _sources.Length)
            _sources[clipIdx].Play();
    }

    private void Die()
    {
        PlaySound(0);
        isDead = true;
    }
}
