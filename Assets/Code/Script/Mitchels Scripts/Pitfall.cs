using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace mitchel.traps
{
    public class Pitfall : MonoBehaviour
    {
        [Header("Respawning")]
        public GameObject spawner1;
        public GameObject spawner2;
        [Tooltip("Specify the offset on the Y axis from the spawner location. Example: the Super Duper Blox Man prefab requires an offset of 1.")]
        public float spawnOffset;
        [Tooltip("Specify the delay in seconds until the player is translated to the spawner location.")]
        [SerializeField] private float respawnDelay;
        private GameObject player1;
        private GameObject player2;

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.GetComponent<ronan.player.PlayerMovement>())
            {
                Debug.Log("Something hit!");
                if (other.transform.parent.gameObject.tag == "Player1")
                {
                    player1 = other.gameObject;
                    Debug.Log("Trigger activated for player 1!");
                    Debug.Log(player1);
                    StartCoroutine(DelayRespawnPlayer1());
                }
                if (other.transform.parent.gameObject.tag == "Player2")
                {
                    player2 = other.gameObject;
                    Debug.Log("Trigger activated for player 2!");
                    Debug.Log(player2);
                    StartCoroutine(DelayRespawnPlayer2());
                }
            }
        }

        IEnumerator DelayRespawnPlayer1()
        {
            yield return new WaitForSeconds(respawnDelay);
            player1.gameObject.transform.position = spawner1.transform.position;
            player1.gameObject.transform.Translate(0, spawnOffset, 0);
            player1 = null;
        }

        IEnumerator DelayRespawnPlayer2()
        {
            yield return new WaitForSeconds(respawnDelay);
            player2.gameObject.transform.position = spawner2.transform.position;
            player2.gameObject.transform.Translate(0, spawnOffset, 0);
            player2 = null;
        }
    }
}