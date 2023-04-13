using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace mitchel.debugscripts
{
    public class GetInstanceID : MonoBehaviour
    {
        public GameObject firstObj;
        public GameObject secondObj;

        // Update is called once per frame
        void Update()
        {
            Debug.Log("firstObj = " + firstObj.GetInstanceID());
            Debug.Log("secondObj = " + secondObj.GetInstanceID());
        }
    }
}