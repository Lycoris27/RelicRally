using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace mitchel.traps
{
    public class DartLauncher : MonoBehaviour
    {
        [Header("Essentials")]
        [SerializeField] private GameObject dart;
        //[SerializeField] private ronan.player.PlayerMovement player;

        [Header("Parameters")]
        [SerializeField] private float initialDelay;
        [Tooltip("The delay before the dart is reset.")]
        [SerializeField] private float restartDelay;
        [Range(1, 10)]
        [SerializeField] private float dartSpeed;
        [Tooltip("To determine how far away the wall is from the launcher, look at the Transform component in Unity's inspector and subtract the relevant axis of the end wall from the relevant axis of the start wall, plus one. F = (startWallAxis - endWallAxis) + 1")]
        [SerializeField] private float endWallDistance;
        [SerializeField] private float playerEffectTime;
        [Tooltip("Enabled for Z axis, disabled for X axis.")]
        [SerializeField] private bool xOrZAxis;

        private float timeElapsed;
        private float lerpDuration;
        private float startValue = 0;
        private float endValue;
        private float valueToLerp;
        private float initialPositionX;
        private float initialPositionZ;
        [HideInInspector] public bool dartHit;

        private float initialPlayer1WalkSpeed;
        private float initialPlayer1SprintSpeed;
        private float affectedPlayer1WalkSpeed;
        private float affectedPlayer1SprintSpeed;

        private float initialPlayer2WalkSpeed;
        private float initialPlayer2SprintSpeed;
        private float affectedPlayer2WalkSpeed;
        private float affectedPlayer2SprintSpeed;

        [HideInInspector] public GameObject player1;
        [HideInInspector] public GameObject player2;

        void Start()
        {
            endValue = endWallDistance;
            lerpDuration = (endValue / (dartSpeed * 2));
            initialPositionX = dart.transform.position.x;
            initialPositionZ = dart.transform.position.z;
            initialPlayer1WalkSpeed = player1.GetComponent<ronan.player.PlayerMovement>().walkSpeed;
            initialPlayer1SprintSpeed = player1.GetComponent<ronan.player.PlayerMovement>().sprintSpeed;
            affectedPlayer1WalkSpeed = player1.GetComponent<ronan.player.PlayerMovement>().walkSpeed / 2;
            affectedPlayer1SprintSpeed = player1.GetComponent<ronan.player.PlayerMovement>().sprintSpeed / 2;

            initialPlayer2WalkSpeed = player2.GetComponent<ronan.player.PlayerMovement>().walkSpeed;
            initialPlayer2SprintSpeed = player2.GetComponent<ronan.player.PlayerMovement>().sprintSpeed;
            affectedPlayer2WalkSpeed = player2.GetComponent<ronan.player.PlayerMovement>().walkSpeed / 2;
            affectedPlayer2SprintSpeed = player2.GetComponent<ronan.player.PlayerMovement>().sprintSpeed / 2;
        }

        void Update()
        {
            StartCoroutine(DelayBeforeExecute());

            if (dartHit == true)
            {
                if (player2 = null)
                {
                    StartCoroutine(HitEffectPlayer1());
                    dartHit = false;
                    Debug.Log("Player 1 hit!");
                }
                else
                {
                    StartCoroutine(HitEffectPlayer2());
                    dartHit = false;
                    Debug.Log("Player 2 hit!");
                }
            }
            else
            {
                return;
            }
        }

        IEnumerator DelayBeforeExecute()
        {
            yield return new WaitForSeconds(initialDelay);
            if (xOrZAxis == true)
            {
                if (timeElapsed < lerpDuration)
                {
                    valueToLerp = Mathf.Lerp(startValue, endValue, timeElapsed / lerpDuration);
                    timeElapsed += Time.deltaTime;
                }
                else
                {
                    yield return new WaitForSeconds(restartDelay);
                    dart.transform.position = new Vector3(initialPositionX, dart.transform.position.y, dart.transform.position.z);
                    timeElapsed = 0;
                }
                //Debug.Log("valueToLerp = " + valueToLerp + ", timeElapsed = " + timeElapsed + ", lerpDuration = " + lerpDuration);
                dart.transform.position = new Vector3(initialPositionX + valueToLerp, dart.transform.position.y, dart.transform.position.z);
            }
            else if (xOrZAxis == false)
            {
                if (timeElapsed < lerpDuration)
                {
                    valueToLerp = Mathf.Lerp(startValue, endValue, timeElapsed / lerpDuration);
                    timeElapsed += Time.deltaTime;
                }
                else
                {
                    yield return new WaitForSeconds(restartDelay);
                    dart.transform.position = new Vector3(dart.transform.position.x, dart.transform.position.y, initialPositionZ);
                    timeElapsed = 0;
                }
                //Debug.Log("valueToLerp = " + valueToLerp + ", timeElapsed = " + timeElapsed + ", lerpDuration = " + lerpDuration);
                dart.transform.position = new Vector3(dart.transform.position.x, dart.transform.position.y, initialPositionZ + valueToLerp);
            }
        }

        IEnumerator HitEffectPlayer1()
        {
            player1.GetComponent<ronan.player.PlayerMovement>().walkSpeed = affectedPlayer1WalkSpeed;
            player1.GetComponent<ronan.player.PlayerMovement>().sprintSpeed = affectedPlayer1WalkSpeed;
            yield return new WaitForSeconds(playerEffectTime);
            player1.GetComponent<ronan.player.PlayerMovement>().walkSpeed = initialPlayer1WalkSpeed;
            player1.GetComponent<ronan.player.PlayerMovement>().sprintSpeed = initialPlayer1SprintSpeed;
        }

        IEnumerator HitEffectPlayer2()
        {
            player2.GetComponent<ronan.player.PlayerMovement>().walkSpeed = affectedPlayer2WalkSpeed;
            player2.GetComponent<ronan.player.PlayerMovement>().sprintSpeed = affectedPlayer2WalkSpeed;
            yield return new WaitForSeconds(playerEffectTime);
            player2.GetComponent<ronan.player.PlayerMovement>().walkSpeed = initialPlayer2WalkSpeed;
            player2.GetComponent<ronan.player.PlayerMovement>().sprintSpeed = initialPlayer2SprintSpeed;
        }
    }
}