using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dart : MonoBehaviour
{
    [SerializeField] private DartLauncher dartLauncher;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<MitchPlayerMovement>())
        {
            dartLauncher.dartHit = true;
        }
    }
}
