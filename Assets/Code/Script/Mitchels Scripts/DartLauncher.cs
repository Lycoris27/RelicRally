using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace mitchel.traps
{
    public class DartLauncher : MonoBehaviour
    {
        [Header("Essentials")]
        [SerializeField] private GameObject dart;
        //[SerializeField] private ronan.player.PlayerMovement player;
        [SerializeField] private Volume player1Volume;
        [SerializeField] private Volume player2Volume;

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

        [Header("Effects Parameters")]
        [SerializeField] private bool enableImageEffects;
        [SerializeField] private float effectsFadeInTime;
        [SerializeField] private float effectsFadeOutTime;

        private float timeElapsed;
        private float lerpDuration;
        private float startValue = 0;
        private float endValue;
        private float valueToLerp;
        private float initialPositionX;
        private float initialPositionZ;
        [HideInInspector] public bool dartHit;

        private float effectTimeElapsed;
        private float effectEnterLerpDuration;
        private float effectExitLerpDuration;
        private float effectStartValue = 0;
        private float effectEndValue = 1;
        private float effectValueToLerp;

        //private float initialPlayer1WalkSpeed;
        //private float initialPlayer1SprintSpeed;
        //private float affectedPlayer1WalkSpeed;
        //private float affectedPlayer1SprintSpeed;

        //private float initialPlayer2WalkSpeed;
        //private float initialPlayer2SprintSpeed;
        //private float affectedPlayer2WalkSpeed;
        //private float affectedPlayer2SprintSpeed;

        [HideInInspector] public GameObject player1;
        [HideInInspector] public GameObject player2;

        void Start()
        {
            endValue = endWallDistance;
            lerpDuration = (endValue / (dartSpeed * 2));
            initialPositionX = dart.transform.position.x;
            initialPositionZ = dart.transform.position.z;

            //initialPlayer1WalkSpeed = player1.GetComponent<ronan.player.PlayerMovement>().walkSpeed;
            //initialPlayer1SprintSpeed = player1.GetComponent<ronan.player.PlayerMovement>().sprintSpeed;
            //affectedPlayer1WalkSpeed = player1.GetComponent<ronan.player.PlayerMovement>().walkSpeed / 2;
            //affectedPlayer1SprintSpeed = player1.GetComponent<ronan.player.PlayerMovement>().sprintSpeed / 2;

            //initialPlayer2WalkSpeed = player2.GetComponent<ronan.player.PlayerMovement>().walkSpeed;
            //initialPlayer2SprintSpeed = player2.GetComponent<ronan.player.PlayerMovement>().sprintSpeed;
            //affectedPlayer2WalkSpeed = player2.GetComponent<ronan.player.PlayerMovement>().walkSpeed / 2;
            //affectedPlayer2SprintSpeed = player2.GetComponent<ronan.player.PlayerMovement>().sprintSpeed / 2;

            effectEnterLerpDuration = effectsFadeInTime;
            effectExitLerpDuration = effectsFadeOutTime;
            player1Volume.weight = 0;
            player2Volume.weight = 0;
        }

        void Update()
        {
            StartCoroutine(DelayBeforeExecute());
            Debug.Log(player1Volume.weight);

            if (dartHit == true)
            {
                if (player1 == mitchel.traps.Dart.hitPlayer)
                {
                    StartCoroutine(HitEffectPlayer1());
                    dartHit = false;
                    Debug.Log("Player 1 hit!");
                }
                else if (player2 == mitchel.traps.Dart.hitPlayer)
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
            //player1.GetComponent<ronan.player.PlayerMovement>().walkSpeed = affectedPlayer1WalkSpeed;
            //player1.GetComponent<ronan.player.PlayerMovement>().sprintSpeed = affectedPlayer1WalkSpeed;
            player1.GetComponent<ronan.player.PlayerMovement>().walkSpeed = player1.GetComponent<ronan.player.PlayerMovement>().walkSpeed / 2;
            player1.GetComponent<ronan.player.PlayerMovement>().sprintSpeed = player1.GetComponent<ronan.player.PlayerMovement>().sprintSpeed / 2;
            if (enableImageEffects == true)
            {
                player1Volume.weight = 1;
            }
            yield return new WaitForSeconds(playerEffectTime);
            //player1.GetComponent<ronan.player.PlayerMovement>().walkSpeed = initialPlayer1WalkSpeed;
            //player1.GetComponent<ronan.player.PlayerMovement>().sprintSpeed = initialPlayer1SprintSpeed;
            player1.GetComponent<ronan.player.PlayerMovement>().walkSpeed = player1.GetComponent<ronan.player.PlayerMovement>().walkSpeed * 2;
            player1.GetComponent<ronan.player.PlayerMovement>().sprintSpeed = player1.GetComponent<ronan.player.PlayerMovement>().sprintSpeed * 2;
            if (enableImageEffects == true)
            {
                /*if (effectTimeElapsed < effectExitLerpDuration)
                {
                    player1Volume.weight = Mathf.Lerp(effectEndValue, effectStartValue, effectTimeElapsed / effectExitLerpDuration);
                    effectTimeElapsed += Time.deltaTime;
                }
                else
                {
                    player1Volume.weight = effectStartValue;
                    effectTimeElapsed = 0;
                }
                player1Volume.weight = (effectEndValue + effectValueToLerp);*/

                player1Volume.weight = 0;
            }
        }

        IEnumerator HitEffectPlayer2()
        {
            //player2.GetComponent<ronan.player.PlayerMovement>().walkSpeed = affectedPlayer2WalkSpeed;
            //player2.GetComponent<ronan.player.PlayerMovement>().sprintSpeed = affectedPlayer2WalkSpeed;
            player2.GetComponent<ronan.player.PlayerMovement>().walkSpeed = player2.GetComponent<ronan.player.PlayerMovement>().walkSpeed / 2;
            player2.GetComponent<ronan.player.PlayerMovement>().sprintSpeed = player2.GetComponent<ronan.player.PlayerMovement>().sprintSpeed / 2;
            if (enableImageEffects == true)
            {
                player2Volume.weight = 1;
            }
            yield return new WaitForSeconds(playerEffectTime);
            //player2.GetComponent<ronan.player.PlayerMovement>().walkSpeed = initialPlayer2WalkSpeed;
            //player2.GetComponent<ronan.player.PlayerMovement>().sprintSpeed = initialPlayer2SprintSpeed;
            player2.GetComponent<ronan.player.PlayerMovement>().walkSpeed = player2.GetComponent<ronan.player.PlayerMovement>().walkSpeed * 2;
            player2.GetComponent<ronan.player.PlayerMovement>().sprintSpeed = player2.GetComponent<ronan.player.PlayerMovement>().sprintSpeed * 2;
            player2Volume.weight = 0;
        }
    }
}