using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace mitchel.traps
{
    public class Pitfall : MonoBehaviour
    {
        [Header("Essentials")]
        [SerializeField] private GameObject quicksandPool;

        [Header("Respawning")]
        public GameObject spawner1;
        public GameObject spawner2;
        [Tooltip("Specify the offset on the Y axis from the spawner location. Example: the Super Duper Blox Man prefab requires an offset of 1.")]
        public float spawnOffset;
        [Tooltip("Specify the delay in seconds until the player is translated to the spawner location.")]
        [SerializeField] private float respawnDelay;
        private GameObject player1;
        private GameObject player2;

        private float initialPlayer1WalkSpeed;
        private float initialPlayer2WalkSpeed;
        private float initialPlayer1SprintSpeed;
        private float initialPlayer2SprintSpeed;

        private float timeElapsed;
        private float lerpDuration;
        private float startValue = 0;
        [Range(0,2)]
        [SerializeField] private float sinkFactor;
        private float endValue;
        private float valueToLerp;
        private float initialPositionX;
        private float initialPositionZ;

        private void Start() 
        {
            endValue = sinkFactor;
            GetComponent<BoxCollider>().enabled = false;
        }

        private void OnTriggerEnter(Collider other)
        {
            Debug.Log("Something hit!");
            if (other.gameObject.tag == "Player1")
            {
                player1 = other.gameObject;
                Debug.Log("Trigger activated for player 1!");
                Debug.Log(player1);
                StartCoroutine(DelayRespawnPlayer1());
            }
            else if (other.gameObject.tag == "Player2")
            {
                player2 = other.gameObject;
                Debug.Log("Trigger activated for player 2!");
                Debug.Log(player2);
                StartCoroutine(DelayRespawnPlayer2());
            }
        }

        IEnumerator DelayRespawnPlayer1()
        {
            player1.GetComponent<ronan.player.PlayerMovement>().isStopped = true;
            player1.GetComponent<ronan.player.PlayerMovement>().moveSpeed = 0;

            /*if (timeElapsed < lerpDuration)
                {
                    valueToLerp = Mathf.Lerp(startValue, endValue, timeElapsed / lerpDuration);
                    timeElapsed += Time.deltaTime;
                }
                else
                {
                    yield return new WaitForSeconds(restartDelay);
                    pitfall = new Vector3(initialPositionX, dart.transform.position.y, dart.transform.position.z);
                    timeElapsed = 0;
                }
            dart.transform.position = new Vector3(initialPositionX + valueToLerp, dart.transform.position.y, dart.transform.position.z);*/

            yield return new WaitForSeconds(respawnDelay);
            player1.gameObject.transform.position = spawner1.transform.position;
            player1.gameObject.transform.Translate(0, spawnOffset, 0);
            player1.GetComponent<ronan.player.PlayerMovement>().isStopped = false;
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