using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pitfall : MonoBehaviour
{
    [Header("Essentials")]
    public MitchPlayerMovement mitchPlayerMovement;

    [Header("Respawning")]
    public GameObject spawner;
    [Tooltip("Specify the offset on the Y axis from the spawner location. Example: the Super Duper Blox Man prefab requires an offset of 1.")]
    public float spawnOffset;
    [SerializeField] private float respawnDelay;


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<MitchPlayerMovement>())
        {
            Debug.Log("Trigger activated!");
            //other.gameObject.transform.position = new Vector3(-2f,6.1f,10.961f); // Transform player back to a specific point in space (TO BE CHANGED)
            // TODO see if player height is needed for y offset
            other.gameObject.transform.position = spawner.transform.position;
            other.gameObject.transform.Translate(0, spawnOffset, 0);

            StartCoroutine(DelayRespawn());
        }
    }

    IEnumerator DelayRespawn()
    {
        yield return new WaitForSeconds(respawnDelay);
        // TODO Insert code for respawning player here
    }
}
