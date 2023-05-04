using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class UIHandler : MonoBehaviour
{
    private float countingDown = -1;
    private float countDownMax = 4;
    private float countdownRounded;
    public static bool paused = false;
    private string go = "GO!!!";
    private string noTxt = " ";
    private bool infoRead = false;
    private bool done = false;

    public PlayerInputMap openMenu;
    public AudioVolumeScript audioVolumeScript;

    [Header("GameObjects(Set During Runtime)")]
    public GameObject menu;
    public GameObject settings;
    public GameObject hud;
    public GameObject exitCheck;
    public GameObject menuBackground;
    public GameObject playerInformation;
    public TextMeshProUGUI countdownTxt;
    public GameObject resume;
    public GameObject playerInfoButton;

    
    //All gameobjects are found and added to the corresponding variables, and the prefab is made so it cannot be destroyed on loading to a new scene
    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        openMenu = new PlayerInputMap();
        audioVolumeScript = this.gameObject.GetComponent<AudioVolumeScript>();
        menuBackground = GameObject.Find("MenuBackground");
        menu = GameObject.Find("Menu");
        settings = GameObject.Find("Settings");
        hud = GameObject.Find("HUD");
        resume = GameObject.Find("Resume");   
        exitCheck = GameObject.Find("ExitCheck");
        playerInformation = GameObject.Find("PlayerInformation");
        playerInfoButton = GameObject.Find("PlayerInfoButton");
        
        //countdownTxt = GetComponent<TextMeshProUGUI>();
    }
    // everything that needed to be accessed in awake but be turned off is turned off here
    void Start()
    {
        menuBackground.SetActive(false);
        menu.SetActive(false);
        settings.SetActive(false);
        hud.SetActive(false);
        exitCheck.SetActive(false);
        playerInformation.SetActive(false);
        playerInfoButton.SetActive(false);

    }
    //Checks to see if the HUD should be on, only should be on if it is the greek or nordic levels
    void Update()
    {


        
        PlayerInfoChecker();
        int y = SceneManager.GetActiveScene().buildIndex;
        if (countdownTxt == null)
        {
            countdownTxt = GameObject.Find("Countdowntxt").GetComponent<TextMeshProUGUI>();
        }
        if (y==2||y==3)
        {
            //countdownTxt = GameObject.Find("Countdowntxt").GetComponent<TextMeshProUGUI>();
            CheckForHUD();
            CountingDown();
        }

    }

    //On Enable and OnDisable both allow the menu to be turned on and off
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
    //Loads the new scene and turns off the UI elements
    public void LoadNewScene(int number)
    {
        SceneManager.LoadScene(sceneBuildIndex: number);
        //audioVolumeScript.GrabManagers();
        Resume();
    }
    //keeps the HUD open for as long as the hud needs to be
    public void CheckForHUD()
    {
        hud.SetActive(true);
    }
    // opens the menu by checking to see if the button is pressed, and neither the menu nor the settings are active, also checks to see if we are on the title screen because it shouldn't be open there
    // if these are not correct, then it will resume instead
    public void MenuOpening(InputAction.CallbackContext context)
    {
        int y = SceneManager.GetActiveScene().buildIndex;
        if(context.performed && !menu.activeSelf && !settings.activeSelf && !playerInformation.activeSelf)
        {
            print(y);
            if(y != 0) {Pause();}
        }
        else if(context.performed && (menu.activeSelf || settings.activeSelf || playerInformation.activeSelf) && y != 0) {Resume();}
    }
    //pauses the game, opens the paused menu, turns game time to 0, plays new audio
    public void Pause()
    {
        EventSet(resume);
        menu.SetActive(true);
        menuBackground.SetActive(true);
        Time.timeScale = 0f;
        paused = true;
        audioVolumeScript.PlayPausedAudio();
    }
    // turns off all UI elements but the hud, sets the time back to normal, turns the old audio back on
    public void Resume()
    {
        menu.SetActive(false);
        settings.SetActive(false);
        exitCheck.SetActive(false);
        menuBackground.SetActive(false);
        playerInformation.SetActive(false);
        Time.timeScale = 1f;
        paused = false;
    }
    //Controls the ability 
    public void EventSet(GameObject newSelectedObject)
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(newSelectedObject);
    }
    public void ExitGame(){Application.Quit();}
    //provides the Change that allows CountingDown to start
    public void CountDownStart()
    {
        print("countingStarted!");
        Time.timeScale = 0f;
        countingDown = countDownMax;
    }
    //Creates a timer that is projected onto the screen for the start of the relic rally game sections, specifically the "3-2-1,GO!"
    public void CountingDown()
    {
        if (countingDown < -1)
        {
            countdownTxt.text = noTxt.ToString();
        }
        else if (countingDown <= 0)
        {
            countingDown -= Time.unscaledDeltaTime;
            Time.timeScale = 0f;
            countdownTxt.text = go.ToString();
            Time.timeScale = 1f;
        }
        else
        {
            countdownRounded = Mathf.Ceil(countingDown);
            countdownTxt.text = countdownRounded.ToString();
            countingDown -= Time.unscaledDeltaTime;
            print(countingDown);
        }
    }
    public void PlayerInfoChecker()
    {
        int y = SceneManager.GetActiveScene().buildIndex;
        if (y==1 && !infoRead)
        {
            Time.timeScale = 0f;
            menuBackground.SetActive(true);
            playerInformation.SetActive(true);
            infoRead = true;
        }
    }
}