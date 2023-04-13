using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.InputSystem;

public class CameraSetUp : MonoBehaviour
{
    private CinemachineVirtualCamera cvm;
    private Transform target;
    private int playerNum = 1;

    private void Start()
    {
        cvm = this.gameObject.GetComponent<CinemachineVirtualCamera>();
        Debug.Log(cvm);
    }
    public void OnSetFollow(Transform player)
    {
        cvm.Follow = player;
        cvm.LookAt = player;
    }
}
