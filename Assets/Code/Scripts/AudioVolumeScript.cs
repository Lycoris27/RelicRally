using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AudioVolumeScript : MonoBehaviour
{
    // Start is called before the first frame update

    public TextMeshProUGUI textMesh;
    public Slider masterVolume;
    public Slider music;
    
    private float one;
    private float two;
    private float three;

    void awake()
    {

        
        textMesh = GetComponent<TextMeshProUGUI>();
        
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        one = masterVolume.value;
        two = music.value;
        three = one * two;
        textMesh.text = three.ToString();
    }
}
