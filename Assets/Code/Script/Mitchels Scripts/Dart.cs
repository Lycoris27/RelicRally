using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace mitchel.traps
{
    public class Dart : MonoBehaviour
    {
        [SerializeField] private mitchel.traps.DartLauncher dartLauncher;
        private GameObject player1;
        private GameObject player2;

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.GetComponent<ronan.player.PlayerMovement>())
            {
                if (other.gameObject.tag == "Player1")
                {
                    player1 = other.gameObject;
                    dartLauncher.player1 = player1;
                    dartLauncher.dartHit = true;
                }
                else if (other.gameObject.tag == "Player2")
                {
                    player2 = other.gameObject;
                    dartLauncher.player2 = player2;
                    dartLauncher.dartHit = true;
                }
            }
        }
    }
}
