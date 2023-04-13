using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace mitchel.traps
{
    public class Pitfall : MonoBehaviour
    {
        [Header("Respawning")]
        public GameObject spawner;
        [Tooltip("Specify the offset on the Y axis from the spawner location. Example: the Super Duper Blox Man prefab requires an offset of 1.")]
        public float spawnOffset;
        [Tooltip("Specify the delay in seconds until the player is translated to the spawner location.")]
        [SerializeField] private float respawnDelay;
        private GameObject desiredObj1;
        private GameObject desiredObj2;

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.GetComponent<ronan.player.PlayerMovement>())
            {
                if (desiredObj1 = null)
                {
                    desiredObj1 = other.gameObject;
                    Debug.Log("Trigger activated for player 1!");
                    Debug.Log(desiredObj1);
                    StartCoroutine(DelayRespawnPlayer1());
                }
                else
                {
                    desiredObj2 = other.gameObject;
                    Debug.Log("Trigger activated for player 2!");
                    Debug.Log(desiredObj2);
                    StartCoroutine(DelayRespawnPlayer2());
                }
            }
        }

        IEnumerator DelayRespawnPlayer1()
        {
            yield return new WaitForSeconds(respawnDelay);
            desiredObj1.gameObject.transform.position = spawner.transform.position;
            desiredObj1.gameObject.transform.Translate(0, spawnOffset, 0);
            desiredObj1 = null;
        }

        IEnumerator DelayRespawnPlayer2()
        {
            yield return new WaitForSeconds(respawnDelay);
            desiredObj2.gameObject.transform.position = spawner.transform.position;
            desiredObj2.gameObject.transform.Translate(0, spawnOffset, 0);
            desiredObj2 = null;
        }
    }
}