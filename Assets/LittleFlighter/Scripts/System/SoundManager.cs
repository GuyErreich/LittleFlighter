using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(AudioSource))]
public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;
    private static float globalMusicVolume, globalEffectVolume;
    private static List<AudioClip> globalClips;

    [SerializeField] private List<AudioClip> clips;

    [SerializeField, Range(0f, 1f)] private float musicVolume ,effectVolume;

    [SerializeField] private UnityEvent<float> OnEffectVolumeChanged;
    [SerializeField] private UnityEvent<float> OnMusicVolumeChanged;

    private static AudioSource source;

    private void Awake()
    {
        Instance = this;
        source = this.GetComponent<AudioSource>();
        source.volume = globalMusicVolume;
        source.loop = false;
    }

    private void Update() {
        print(source.isPlaying);
        if(!source.isPlaying)
        {
            var index = Random.Range(0, this.clips.Count);

            this.PlaySound(clips[index]);
        }
    }

    private void OnValidate() {
        globalEffectVolume = this.effectVolume;
        globalMusicVolume = this.musicVolume;
        globalClips = this.clips;

        this.OnEffectVolumeChanged.Invoke(globalEffectVolume);
        this.OnMusicVolumeChanged.Invoke(globalMusicVolume);
        source.volume = globalMusicVolume;
    }

    public float MusicVolume
    {
        get { return musicVolume;}
        set 
        {
            this.musicVolume = value;
            globalMusicVolume = this.musicVolume; 
            source.volume = globalMusicVolume;
            this.OnMusicVolumeChanged.Invoke(globalMusicVolume);
        }
    }

    public float EffectVolume
    {
        get { return globalEffectVolume;}
        set 
        {
            this.effectVolume = value;
            globalEffectVolume = this.effectVolume; 
            OnEffectVolumeChanged.Invoke(globalEffectVolume);
        }
    }

    private void PlaySound(AudioClip clip) {
        source.PlayOneShot(clip);
    } 
}
