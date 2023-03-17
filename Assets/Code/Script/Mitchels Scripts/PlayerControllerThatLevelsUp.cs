using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;

/*
 
       Player Controller that moves and turns faster as it gains XP;
	    Manages moving and rotating the player via the arrow keys.
		Up/down arrow keys to move forward and backward.
		Left/right arrow keys to rotate left and right.
*/

public class PlayerControllerThatLevelsUp : MonoBehaviour
{

    //The base move and turn speed
    public float moveSpeed = 1f;
    public float turnSpeed = 45f;

    //The move and turn speed with the buffs you have from leveling up.   
    public float currentMoveSpeed;
    public float currentTurnSpeed;

    //[INSERT DESCRIPTION HERE]
    public float sprintSpeedModifier = 2f;

    //[INSERT DESCRIPTION HERE]
    public float activeSprintModifier = 1f;

    //The base jump height
    public float jumpHeight = 2f;

    //The jump height with the buffs you have from leveling up. 
    public float currentJumpHeight;

    public int currentLockPickSkill = 1;


    public float xp = 0;	// Amount of XP the player has
    public float xpForNextLevel = 10;   //Xp needed to level up, the higher the level, the harder it gets. 
    public int level = 0;   // Level of the player


    // Begin by calling all these functions
    private void Start()
    {
        SetXpForNextLevel();
        SetCurrentMoveSpeed();
        SetCurrentTurnSpeed();
        SetCurrentJumpHeight();
        SetCurrentLockPickSkill();
    }



    // To level up you need to collect an amount of xp;
    // This starts at 10 xp
    // Each level you gain the xp required gets higher exponentially
    // The exponential growth is slowed by scaling it by 10%

    void SetXpForNextLevel()
    {
        xpForNextLevel = (10f + (level * level * 0.1f)); // Set the xpForNextLevel value to 10 + the result of the equation next to it
        Debug.Log("xpForNextLevel " + xpForNextLevel); // Report the XP needed for the next level
    }



    // For each level, the player adds 10% to the move speed 
    void SetCurrentMoveSpeed()
    {
        currentMoveSpeed = this.moveSpeed + (this.moveSpeed * 0.1f * level); // Set the currentMoveSpeed value to that of moveSpeed + the result of the equation next to it
        Debug.Log("currentMoveSpeed = " + currentMoveSpeed); // Report the new current move speed
    }

    // For each level, the player adds 10% to the turn speed 
    void SetCurrentTurnSpeed()
    {
        currentTurnSpeed = this.turnSpeed + (this.turnSpeed * (level * 0.1f)); // Set the currentTurnSpeed value to that of turnSpeed + the result of the equation next to it
        Debug.Log("currentTurnSpeed = " + currentTurnSpeed); // Report the new current turn speed
    }

    // For each level, the player adds 10% to the jump height
    void SetCurrentJumpHeight()
    {
        currentJumpHeight = this.jumpHeight + (this.jumpHeight * 0.1f * level); // Set the currentJumpHeight value to that of jumpHeight + the result of the equation next to it
        Debug.Log("currentJumpHeight = " + currentJumpHeight); // Report the new current jump height
    }

    // For each level, increase the lock picking skill by 1
    void SetCurrentLockPickSkill()
    {
        currentLockPickSkill = this.currentLockPickSkill + 1; // Increase the value of currentLockPickSkill by 1
        Debug.Log("currentLockPickSkill = " + currentLockPickSkill); // Report the new current lock pick skill level
    }


    void LevelUp()
    {
        xp = 0f; // Reset the current XP amount
        level++; // Increase the value of level by 1 
        Debug.Log("level" + level); // Report the new level achieved

        // Call these functions and do it all again
        SetXpForNextLevel();
        SetCurrentMoveSpeed();
        SetCurrentTurnSpeed();
        SetCurrentJumpHeight();
        SetCurrentLockPickSkill();
    }




    //a function to make the player gain the amount of Xp the you tell it. 
    public void GainXP(int xpToGain)
    {
        xp += xpToGain;
        Debug.Log("Gained " + xpToGain + " XP, Current Xp = " + xp + ", XP needed to reach next Level = " + xpForNextLevel);
    }


    void Update()
    {
        //Test the GainXp function by pressing the x button. 
        if (Input.GetKeyDown(KeyCode.X) == true) { GainXP(1); }


        //LevelUp when the appropriate conditions are met.
        if (xp >= xpForNextLevel)
        {
            LevelUp();
        }

        if (Input.GetKeyDown(KeyCode.Escape) == true)
        {
            
        #if UNITY_EDITOR
            {
                EditorApplication.isPlaying = false;
            }
        #else
            {
                Application.Quit();
            }
        #endif
        }



        // Rotation and movement speed is modifed by the level (currentMoveSpeed) of the player and by the time between update frames (Time.deltaTime). 

        // Increase player speed by sprinting via holding down shift key
        if (Input.GetKey(KeyCode.LeftShift) == true)
        {
            activeSprintModifier = sprintSpeedModifier;
        }
        else
        {
            activeSprintModifier = 1f;
        }

        // Move player via up/down arrow keys or W/S keys
        if (Input.GetKey(KeyCode.UpArrow) == true || Input.GetKey(KeyCode.W) == true) { this.transform.position += this.transform.forward * currentMoveSpeed * activeSprintModifier * Time.deltaTime; }
        if (Input.GetKey(KeyCode.DownArrow) == true || Input.GetKey(KeyCode.S) == true) { this.transform.position -= this.transform.forward * currentMoveSpeed * activeSprintModifier * Time.deltaTime; }

        // Strafe player via Q/E keys
        if (Input.GetKey(KeyCode.E) == true) { this.transform.position += this.transform.right * currentMoveSpeed * activeSprintModifier * Time.deltaTime; }
        if (Input.GetKey(KeyCode.Q) == true) { this.transform.position -= this.transform.right * currentMoveSpeed * activeSprintModifier * Time.deltaTime; }

        // Rotate player via left/right arrow keys or A/D keys
        // Identify this position, set the vertical axis as the axis to rotate around the set the rotation speed.
        if (Input.GetKey(KeyCode.RightArrow) == true || Input.GetKey(KeyCode.D) == true) { this.transform.RotateAround(this.transform.position, Vector3.up, currentTurnSpeed * Time.deltaTime); }
        if (Input.GetKey(KeyCode.LeftArrow) == true || Input.GetKey(KeyCode.A) == true) { this.transform.RotateAround(this.transform.position, Vector3.up, -currentTurnSpeed * Time.deltaTime); }

        // Check spacebar to trigger jumping. Checks if vertical velocity (eg velocity.y) is near to zero.
        if (Input.GetKey(KeyCode.Space) == true && Mathf.Abs(this.GetComponent<Rigidbody>().velocity.y) < 0.01f)
        {
            this.GetComponent<Rigidbody>().velocity += Vector3.up * (this.jumpHeight * this.currentJumpHeight);
        }

        // Check R to restart level (for testing purposes)
        if (Input.GetKey(KeyCode.R) == true) { SceneManager.LoadScene(SceneManager.GetActiveScene().name); }
    }
}
