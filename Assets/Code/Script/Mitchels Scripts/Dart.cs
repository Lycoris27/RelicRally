using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace mitchel.traps
{
    public class Dart : MonoBehaviour
    {
        [SerializeField] private mitchel.traps.DartLauncher dartLauncher;

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.GetComponent<ronan.player.PlayerMovement>())
            {
                if (dartLauncher.player1 = null)
                {
                    dartLauncher.player1 = other.gameObject;
                    dartLauncher.dartHit = true;
                }
                else
                {
                    dartLauncher.player2 = other.gameObject;
                    dartLauncher.dartHit = true;
                }
            }
        }
    }
}
