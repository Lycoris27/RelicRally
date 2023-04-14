using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class UIHandler : MonoBehaviour
{
    public PlayerInputMap openMenu;
    public static bool paused = false;
    public AudioVolumeScript audioVolumeScript;
    //private UIScreenFinder;
    
    [Header("GameObjects(Set During Runtime)")]
    public GameObject menu;
    public GameObject settings;
    public GameObject hud;
    public GameObject exitCheck;

    public GameObject resume;
    // Start is called before the first frame update
    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        openMenu = new PlayerInputMap();
        audioVolumeScript = this.gameObject.GetComponent<AudioVolumeScript>();
        menu = GameObject.Find("Menu");
        settings = GameObject.Find("Settings");
        hud = GameObject.Find("HUD");
        resume = GameObject.Find("Resume");   
        exitCheck = GameObject.Find("ExitCheck");
    }
    void Start()
    {
        menu.SetActive(false);
        settings.SetActive(false);
        hud.SetActive(false);
        exitCheck.SetActive(false);
        print("hi");
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
    public void LoadNewScene(int number)
    {
        SceneManager.LoadScene(sceneBuildIndex: number);
        audioVolumeScript.GrabManagers();

    }

    public void MenuOpening(InputAction.CallbackContext context)
    {
        if(context.performed && !menu.activeSelf && !settings.activeSelf)
        {
            int y = SceneManager.GetActiveScene().buildIndex;
            print(y);
            if(y != 0)
            {
                Pause();
            }
        }
        
        else if(context.performed && (menu.activeSelf || settings.activeSelf))
        {
            Resume();
        }
    }
    public void Pause()
    {
        EventSet(resume);
        menu.SetActive(true);
        Time.timeScale = 0f;
        paused = true;
    }
    public void Resume()
    {
        menu.SetActive(false);
        settings.SetActive(false);
        hud.SetActive(false);
        exitCheck.SetActive(false);

        Time.timeScale = 1f;
        paused = false;
    }
    public void EventSet(GameObject newSelectedObject)
    {
        EventSystem.current.SetSelectedGameObject(null);

        EventSystem.current.SetSelectedGameObject(newSelectedObject);
    }
    public void ExitGame()
    {
        Application.Quit();
    }

}