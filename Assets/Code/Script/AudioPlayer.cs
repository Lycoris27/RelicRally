using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioPlayer : MonoBehaviour
{
    public AudioClip audioClip;
    public AudioSource source;
    public void Start()
    {
        //audioClip = this.gameObject.GetComponent<AudioClip>();
        //source = this.gameObject.GetComponent<source>();
        source.PlayOneShot(audioClip);
    }
}
