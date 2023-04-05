using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RelicSceneTransition : MonoBehaviour
{
    [SerializeField] int sceneNum;
    private void OnTriggerEnter(Collider other)
    {
        SceneManager.LoadScene(sceneNum);
    }
}