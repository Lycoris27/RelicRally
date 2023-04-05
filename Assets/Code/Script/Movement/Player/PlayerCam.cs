using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ronan.player
{
    public class PlayerCam : MonoBehaviour
    {

        private void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        private void Update()
        {

        }
    }
}
