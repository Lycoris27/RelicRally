using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace mitchel.testtraps
{
public class TestDart : MonoBehaviour
{
        [SerializeField] private mitchel.testtraps.TestDartLauncher dartLauncher;

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.GetComponent<mitchel.test.MitchPlayerMovement>())
            {
                dartLauncher.dartHit = true;
            }
        }
}
}