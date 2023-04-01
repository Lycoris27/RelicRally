using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.InputSystem;

public class CameraSetUp : MonoBehaviour
{
    [SerializeField] private bool Player1;
    private CinemachineVirtualCamera cvm;
    private Transform target;
    private void Start()
    {
        cvm = FindObjectOfType<CinemachineVirtualCamera>();
    }
    public void OnSetFollow()
    {
        string player = "Player2";
        if (Player1) player = "Player1";

        target = GameObject.Find(player).transform;

        cvm.Follow = target;
        cvm.LookAt = target;

    }
    

}
