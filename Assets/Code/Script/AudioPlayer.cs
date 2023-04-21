using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioPlayer : MonoBehaviour
{
    public AudioManager ambienceObject;
    public AudioManager musicObject;
    public string name;
    [Range(1,2)]
    public int managerSelect;
    // Start is called before the first frame update
    void Awake()
    {
        ambienceObject = GameObject.Find("AmbienceObject").GetComponent<AudioManager>();;
        musicObject = GameObject.Find("MusicObject").GetComponent<AudioManager>();;
    }

    // Update is called once per frame
    void Update()
    {
        if (managerSelect == 1)
        {
            ambienceObject.Play(name);
        }
        if (managerSelect == 2)
        {
            musicObject.Play(name);
        }
    }
}
