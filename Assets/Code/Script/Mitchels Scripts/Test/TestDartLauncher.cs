using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace mitchel.testtraps
{
public class TestDartLauncher : MonoBehaviour
{
       [Header("Essentials")]
        [SerializeField] private GameObject dart;
        [SerializeField] private mitchel.test.MitchPlayerMovement player;

        [Header("Parameters")]
        [SerializeField] private float initialDelay;
        [Tooltip("The delay before the dart is reset.")]
        [SerializeField] private float restartDelay;
        [Range(1, 10)]
        [SerializeField] private float dartSpeed;
        [Tooltip("To determine how far away the wall is from the launcher, look at the Transform component in Unity's inspector and subtract the relevant axis of the end wall from the relevant axis of the start wall, plus one. F = (startWallAxis - endWallAxis) + 1")]
        [SerializeField] private float endWallDistance;

        private float timeElapsed;
        private float lerpDuration;
        private float startValue = 0;
        private float endValue;
        private float valueToLerp;
        private float initialPosition;
        [HideInInspector] public bool dartHit;

        void Start()
        {
            endValue = endWallDistance;
            lerpDuration = (endValue / (dartSpeed * 2));
            initialPosition = dart.transform.position.x;
        }

        void Update()
        {
            StartCoroutine(DelayBeforeExecute());

            if (dartHit == true)
            {
                Debug.Log("Effect goes here.");
                // TODO Add a short slowdown effect once the new player controller replaces Super Duper Blox Man.
                dartHit = false;
            }
            else
            {
                return;
            }
        }

        IEnumerator DelayBeforeExecute()
        {
            yield return new WaitForSeconds(initialDelay);
            if (timeElapsed < lerpDuration)
            {
                valueToLerp = Mathf.Lerp(startValue, endValue, timeElapsed / lerpDuration);
                timeElapsed += Time.deltaTime;
            }
            else
            {
                yield return new WaitForSeconds(restartDelay);
                dart.transform.position = new Vector3(initialPosition, dart.transform.position.y, dart.transform.position.z);
                timeElapsed = 0;
            }
            //Debug.Log("valueToLerp = " + valueToLerp + ", timeElapsed = " + timeElapsed + ", lerpDuration = " + lerpDuration);
            dart.transform.position = new Vector3(initialPosition + valueToLerp, dart.transform.position.y, dart.transform.position.z);
        }
}
}