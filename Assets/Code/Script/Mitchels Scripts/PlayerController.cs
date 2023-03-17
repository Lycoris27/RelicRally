using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class PlayerController : MonoBehaviour
{
    [SerializeField]  // Setting a variable as private removes its parametre controls from the Unity inspector, but using SerializeField makes it visible again.
    private float moveSpeed = 5f;
    [SerializeField]
    private float turnSpeed = 100f;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Hello, Number " + AddTwoNumbers(2, 4) + "!");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W) == true || Input.GetKey(KeyCode.UpArrow) == true) { this.transform.position += this.transform.forward * Time.deltaTime * this.moveSpeed; }
        if (Input.GetKey(KeyCode.S) == true || Input.GetKey(KeyCode.DownArrow) == true) { this.transform.position -= this.transform.forward * Time.deltaTime * this.moveSpeed; }

        if (Input.GetKey(KeyCode.A) == true || Input.GetKey(KeyCode.LeftArrow) == true) { this.transform.Rotate(this.transform.up, Time.deltaTime * -this.turnSpeed); }
        if (Input.GetKey(KeyCode.D) == true || Input.GetKey(KeyCode.RightArrow) == true) { this.transform.Rotate(this.transform.up, Time.deltaTime * this.turnSpeed); }

        if (Input.GetKey(KeyCode.E) == true) { this.transform.position += this.transform.right * Time.deltaTime * this.moveSpeed; }
        if (Input.GetKey(KeyCode.Q) == true) { this.transform.position -= this.transform.right * Time.deltaTime * this.moveSpeed; }

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
    }

    public int AddTwoNumbers(int number1, int number2)
    {
        return number1 + number2;
    }
}