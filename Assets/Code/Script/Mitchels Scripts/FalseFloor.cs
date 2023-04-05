using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FalseFloor : MonoBehaviour
{
    private void OnTriggerEnter (Collider other)
    {
        if (other.gameObject.GetComponent<MitchPlayerMovement>())
        {
            Destroy(this.gameObject);
        }
    }
}
