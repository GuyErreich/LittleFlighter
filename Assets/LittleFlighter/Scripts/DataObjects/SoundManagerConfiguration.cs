using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace LittleFlighter.DataObjects
{
    [CreateAssetMenu(fileName = "Sound Manager Configuration", menuName = "Data Objects/Configurations/Sound Manager")]
    public class SoundManagerConfiguration : ScriptableObject 
    {
        [Range(0f, 1f)] public float musicVolume ,effectVolume;
        [Space]
        [SerializeField] public List<AudioClip> backgroundMusicClips;
        [Space]
        [Space]
        [SerializeField] public UnityEvent<float> OnEffectVolumeChanged;
        [SerializeField] public UnityEvent<float> OnMusicVolumeChanged;
    }
}