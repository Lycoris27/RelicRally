using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.InputSystem;
using Unity.VisualScripting;
using ronan.player;

public class PlayerManager : MonoBehaviour
{

    [HideInInspector] public List<PlayerInput> playerInputs = new List<PlayerInput>();
    [HideInInspector] public List<PlayerMovement> playerMovement = new List<PlayerMovement>();
    public List<GameObject> players = new List<GameObject>();

    private int count = 0;

    private int playerManagerNumber = 0;
    private void Start()
    {
        foreach (GameObject player in players)
        {
            playerInputs.Add(player.GetComponent<PlayerInput>());
            playerMovement.Add(player.GetComponent<PlayerMovement>());
        }
        
        players[0].tag = "Player1";
        players[1].tag = "Player2";
    }

    public void AddPlayer(Gamepad player)
    {
        playerInputs[count].SwitchCurrentControlScheme("Controller", player);
        count++;
    }
}
