using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class HandleVolumeChanged : MonoBehaviour
{
    private AudioSource source;

    private void Awake() {
        this.source = this.GetComponent<AudioSource>();
    }

    public void ChangeVolume(float volume)
    {
        this.source.volume = volume;
    }
}
