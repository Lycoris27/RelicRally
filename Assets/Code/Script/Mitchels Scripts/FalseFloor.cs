using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace mitchel.traps
{
    public class FalseFloor : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.GetComponent<ronan.player.PlayerMovement>())
            {
                Destroy(this.gameObject);
            }
        }
    }
}