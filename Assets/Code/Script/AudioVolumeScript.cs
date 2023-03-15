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

    void awake()
    {
        MVolumetxt = GetComponent<TextMeshProUGUI>();
        MusVolumetxt = GetComponent<TextMeshProUGUI>();
        SFXVolumetxt = GetComponent<TextMeshProUGUI>();
        AmbVolumetxt = GetComponent<TextMeshProUGUI>();        
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
            ambienceValue = master * ambience;
            
            MVolumetxt.text = master.ToString("00%");
            MusVolumetxt.text = music.ToString("00%");
            SFXVolumetxt.text = sfx.ToString("00%");
            AmbVolumetxt.text = ambience.ToString("00%");
        }
    }
}
