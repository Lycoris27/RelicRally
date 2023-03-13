using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MenuOpener : MonoBehaviour
{


    public PlayerInputMap openMenu;
    public GameObject menu;
    public static bool paused = false;
    // Start is called before the first frame update
    private void Awake()
    {
        //menu = GameObject.Find("Menu");
        openMenu = new PlayerInputMap();
    }
    private void OnEnable()
    {
        openMenu.Enable();
        openMenu.UI.OpenMenu.performed += MenuOpening;
    }
    private void OnDisable()
    {
        openMenu.Disable();
        openMenu.UI.OpenMenu.performed -= MenuOpening;
    }

    void MenuOpening(InputAction.CallbackContext context)
    {
        if(context.performed && !menu.activeSelf)
        {
            print("hi");
            print(menu.name);
            menu.SetActive(true);
            Time.timeScale = 0f;
            paused = true;
        }
        
        else if(context.performed && menu.activeSelf)
        {
            print("pengus");
            menu.SetActive(false);
            Time.timeScale = 1f;
            paused = false;

        }
    }

}