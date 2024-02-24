using LittleFlighter.DataObjects;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(AudioSource))]
public class SoundManager : MonoBehaviour
{
    [SerializeField] private SoundManagerConfiguration _configuration;
    [SerializeField] public UnityEvent<float> OnEffectVolumeChanged;
    [SerializeField] public UnityEvent<float> OnMusicVolumeChanged;

    private static SoundManager _instance;
    private static AudioSource _source;

    #region Getters/Setters
    public float MusicVolume
    {
        get { return Instance._configuration.musicVolume; }
        set 
        {
            _source.volume = value;
            Instance._configuration.musicVolume = value;
            Instance._configuration.OnMusicVolumeChanged.Invoke(value);
            Instance.OnMusicVolumeChanged.Invoke(value);
            SaveSoundSettings();
        }
    }

    public float EffectVolume
    {
        get { return Instance._configuration.effectVolume; }
        set 
        {
            Instance._configuration.effectVolume = value;
            Instance._configuration.OnEffectVolumeChanged.Invoke(value);
            Instance.OnEffectVolumeChanged.Invoke(value);
            SaveSoundSettings();
        }
    }
    #endregion

    public static SoundManager Instance
    {
        get
        {
            // Check if the instance is null
            if (_instance == null)
            {
                // If it's null, try to find an existing instance in the scene
                _instance = FindObjectOfType<SoundManager>();

                // If no instance exists in the scene, create a new GameObject and add the script to it
                if (_instance == null)
                {
                    GameObject singletonObject = new GameObject(typeof(SoundManager).Name);
                    _instance = singletonObject.AddComponent<SoundManager>();
                }
            }
            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        _source = Instance.GetComponent<AudioSource>();
        _source.loop = false;

        MusicVolume = Instance._configuration.musicVolume;
        EffectVolume = Instance._configuration.effectVolume;

        // Load saved sound settings
        LoadSoundSettings();
    }

    private void Update() {
        if(!_source.isPlaying)
        {
            var index = Random.Range(0, Instance._configuration.backgroundMusicClips.Count);

            _source.PlayOneShot(Instance._configuration.backgroundMusicClips[index]);
        }
    }

    // Save sound settings
    private void SaveSoundSettings()
    {
        PlayerPrefs.SetFloat("Music Volume", Instance._configuration.musicVolume);
        PlayerPrefs.SetFloat("Effect Volume", Instance._configuration.effectVolume);

        PlayerPrefs.Save();
    }

    // Load sound settings
    private void LoadSoundSettings()
    {
        MusicVolume = PlayerPrefs.HasKey("Music Volume") ? PlayerPrefs.GetFloat("Music Volume") : 1;
        EffectVolume = PlayerPrefs.HasKey("Effect Volume") ? PlayerPrefs.GetFloat("Effect Volume") : 1;
    }
}
