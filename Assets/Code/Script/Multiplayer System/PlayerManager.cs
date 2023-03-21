using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.InputSystem;
using Unity.VisualScripting;
using ronan.player;

public class PlayerManager : MonoBehaviour
{
    private List<PlayerInput> players = new List<PlayerInput>();
    [SerializeField] private List<Transform> startingPoints;
    [SerializeField] private List<LayerMask> playerLayers;

    [SerializeField] private List<CinemachineVirtualCamera> cvc;

    public void AddPlayer(PlayerInput player)
    {
        players.Add(player);

        Transform playerParent = player.transform.parent;
        playerParent.transform.position = startingPoints[players.Count - 1].position;

        int layerToAdd = (int)Mathf.Log(playerLayers[players.Count - 1].value, 2);


        playerParent.GetComponentInChildren<CinemachineBrain>().gameObject.layer = layerToAdd;
        playerParent.GetComponentInChildren<Camera>().cullingMask |= 1 << layerToAdd;
        cvc[players.Count - 1].Follow = playerParent.GetComponentInChildren<PlayerMovement>().transform;
        cvc[players.Count - 1].LookAt = playerParent.GetComponentInChildren<PlayerMovement>().transform;
    }
}
