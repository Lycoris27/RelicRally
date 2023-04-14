using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace mitchel.traps
{
    public class Dart : MonoBehaviour
    {
        [SerializeField] private mitchel.traps.DartLauncher dartLauncher;
        [HideInInspector] public static GameObject hitPlayer;

        private void OnTriggerEnter(Collider other)
        {
            Debug.Log("Something hit!");
            if (other.gameObject.tag == "Player1")
            {
                hitPlayer = other.gameObject;
                dartLauncher.player1 = hitPlayer;
                dartLauncher.dartHit = true;
            }
            else if (other.gameObject.tag == "Player2")
            {
                hitPlayer = other.gameObject;
                dartLauncher.player2 = hitPlayer;
                dartLauncher.dartHit = true;
            }
            Debug.Log("Hit player = " + hitPlayer.transform.parent.gameObject.tag);
        }
    }
}
