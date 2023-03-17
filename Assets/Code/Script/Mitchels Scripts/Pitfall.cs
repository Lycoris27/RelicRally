using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pitfall : MonoBehaviour
{
    public PlayerControllerThatLevelsUp playerControllerThatLevelsUp;
    public GameObject spawner;
    [Tooltip("Set this as 1 or above so the player doesn't translate to inside the floor.")]
    public float spawnOffset;
    [Tooltip("The amount of delay before the player translates back to the spawner.")]
    public float delayAmount;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<PlayerControllerThatLevelsUp>())
        {
            StartCoroutine(DelayedTranslate());
        }
    }

    IEnumerator DelayedTranslate() 
    {
        yield return new WaitForSecond(delayAmount);
        Debug.Log("Trigger activated!");
        //other.gameObject.transform.position = new Vector3(-2f,6.1f,10.961f); // Transform player back to a specific point in space (TO BE CHANGED)
        other.gameObject.transform.position = spawner.transform.position;
        other.gameObject.transform.Translate(0,spawnOffset,0);
        // TODO see if player height is needed for y offset 
    }
}
