using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DartLauncher : MonoBehaviour
{
    [Header("Essentials")]
    [SerializeField] private GameObject dart;

    [Header("Parameters")]
    [SerializeField] private float initialDelay;
    [SerializeField] private float delay;
    [Range(1,10)]
    [SerializeField] private float dartSpeed;
    [SerializeField] private float endWallDistance;

    private float timeElapsed;
    private float lerpDuration;
    private float startValue = 0;
    private float endValue;
    private float valueToLerp;
    private float initialPosition;

    void Start()
    {
        endValue = endWallDistance;
        lerpDuration = (endValue / (dartSpeed * 2));
        initialPosition = dart.transform.position.x;
    }

    void Update()
    {
        StartCoroutine(DelayBeforeExecute());
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
            yield return new WaitForSeconds(delay);
            dart.transform.position = new Vector3(initialPosition, dart.transform.position.y, dart.transform.position.z);
            timeElapsed = 0;
        }
        Debug.Log("valueToLerp = " + valueToLerp + ", timeElapsed = " + timeElapsed);
        dart.transform.position = new Vector3(initialPosition + valueToLerp, dart.transform.position.y, dart.transform.position.z);
    }
}
