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

        globalEffectVolume = Instance.effectVolume;
        globalMusicVolume = Instance.musicVolume;
        globalClips = Instance.clips;

        source = Instance.GetComponent<AudioSource>();
        source.volume = globalMusicVolume;
        source.loop = false;

        Instance.OnEffectVolumeChanged.Invoke(globalEffectVolume);
        Instance.OnMusicVolumeChanged.Invoke(globalMusicVolume);
        source.volume = globalMusicVolume;
    }

    private void Update() {
        print(source.isPlaying);
        if(!source.isPlaying)
        {
            var index = Random.Range(0, Instance.clips.Count);

            Instance.PlaySound(clips[index]);
        }
    }

    public float MusicVolume
    {
        get { return musicVolume;}
        set 
        {
            Instance.musicVolume = value;
            globalMusicVolume = Instance.musicVolume; 
            source.volume = globalMusicVolume;
            Instance.OnMusicVolumeChanged.Invoke(globalMusicVolume);
        }
    }

    public float EffectVolume
    {
        get { return globalEffectVolume; }
        set 
        {
            Instance.effectVolume = value;
            globalEffectVolume = Instance.effectVolume; 
            OnEffectVolumeChanged.Invoke(globalEffectVolume);
        }
    }

    private void PlaySound(AudioClip clip) {
        source.PlayOneShot(clip);
    } 
}
