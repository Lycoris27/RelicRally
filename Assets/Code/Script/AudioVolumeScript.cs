using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AudioVolumeScript : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("GameObjects")]
    public GameObject settings;
    //public GameObject ambienceObject;
    //public GameObject musicObject;
    //public GameObject sfxObject;
    [Header("AudioManagers")]
    public AudioManager sfxManager;
    public AudioManager musicManager;
    public AudioManager ambienceManager;
    [Header("texts")]
    public TextMeshProUGUI MVolumetxt;
    public TextMeshProUGUI MusVolumetxt;
    public TextMeshProUGUI SFXVolumetxt;
    public TextMeshProUGUI AmbVolumetxt;
    [Header("Sliders")]
    public Slider masterSlider;
    public Slider musicSlider;
    public Slider SFXSlider;
    public Slider ambienceSlider;
    
    private float master;
    private float music;
    private float sfx;
    private float ambience;
    private float musicValue;
    private float SFXValue;
    private float ambienceValue;

    public string name;

    //public AudioSource[] ambienceArray;
    //public AudioSource[] musicArray;
    //public AudioSource[] SFXArray;

    void awake()
    {
        MVolumetxt = GetComponent<TextMeshProUGUI>();
        MusVolumetxt = GetComponent<TextMeshProUGUI>();
        SFXVolumetxt = GetComponent<TextMeshProUGUI>();
        AmbVolumetxt = GetComponent<TextMeshProUGUI>();

        
  
    }
    void Start()
    {
        GrabManagers();
        
    }
    public void GrabManagers()
    {
        ambienceManager = GameObject.Find("AmbienceObject").GetComponent<AudioManager>();
        musicManager = GameObject.Find("MusicObject").GetComponent<AudioManager>();
        sfxManager = GameObject.Find("SFXObject").GetComponent<AudioManager>();
        PlayAtStart();
    }
    public void PlayAtStart()
    {
        ambienceManager.Play(ambienceManager.sounds[0].name);
    }
    //  Update is called once per frame
    void Update()
    {
        if (settings.activeSelf)
        {
            print("hello!");
            master = masterSlider.value;
            music = musicSlider.value;
            sfx = SFXSlider.value;
            ambience = ambienceSlider.value;
            musicValue = master * music;
            SFXValue = master * sfx;

            if (ambienceManager.sounds.Length > 0)
            {
                foreach (Sound s in ambienceManager.sounds)
                {
                    s.source.volume = master * ambience;
                }
            }
            if (sfxManager.sounds.Length > 0)
            {
                foreach(Sound s in sfxManager.sounds)
                {
                    s.source.volume = master * sfx;
                }
            }
            if (musicManager.sounds.Length > 0)
            {
                foreach(Sound s in musicManager.sounds)
                {
                    s.source.volume = master * music;
                }
            }            
            MVolumetxt.text = master.ToString("00%");
            MusVolumetxt.text = music.ToString("00%");
            SFXVolumetxt.text = sfx.ToString("00%");
            AmbVolumetxt.text = ambience.ToString("00%");
        }
    }
}
            /*
            for(int i = 0; i < ambienceArray.Length; i++)
            {
                ambienceArray[i].volume = master * ambience;
            }
            

            for(int i = 0; i < musicArray.Length; i++)
            {
                musicArray[i].volume = master * ambience;
            }
            for(int i = 0; i < SFXArray.Length; i++)
            {
                SFXArray[i].volume = master * ambience;
            }
            //ambienceGameObjects.audioSource.Volume = master*ambience;
            //ambienceObjects[0].volume = master * ambience;
            */
