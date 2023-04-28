using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerSceneLoad : MonoBehaviour
{
    [SerializeField] private int desiredIndex;
    [SerializeField] private bool restart;
    private void OnTriggerEnter(Collider other)
    {
        FindObjectOfType<BaseSceneManager>().sceneTrigger(desiredIndex, restart);
    }
}
