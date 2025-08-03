using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class UIManager : MonoBehaviour
{
    [Header("GAMEPLAY UI TEXT")]
    public TMP_Text success;
    public TMP_Text timer;
    public TMP_Text keysCollected;
    public TMP_Text badLuck;
    public TMP_Text tutorialText;

    [Header("GAMEPLAY UI panels")]
    public GameObject tutorialPanel;

    [Header("BUTTON HOLDERS")]
    public GameObject inGameWinStateButtons;
    // public GameObject mainMenuButtons;
    // public GameObject settingsMenuButtons;
    public GameObject pauseMenuButtons;

    [Header("Supported Fonts")]
    // public TMP_FontAsset chinese;
    public TMP_FontAsset ukranian;
    public TMP_FontAsset defaultFont;

    private GameObject _nextLevel, _mainMenu, _continueGame, _exitGame;

    private TextMeshProUGUI _nextLevelText, _mainMenuText, _continueGameText, _exitGameText;

    private bool _pauseMenuVisible = false;

  
    private void Awake()
    {
        var allText = Component.FindObjectsOfType<TMP_Text>();

        var lang = PlayerPrefs.GetString("language");


        foreach (var component in allText)
        {
            switch(lang)
            {
                case "eng":
                case "hun":
                case "deu":
                    component.font = defaultFont;
                    break;
                case "ukr":
                    component.font = ukranian;
                    break;
            }
        
        }
            

        success.enabled = false;
        // raname thes to follow the Name Convention
        // badLuck.SetText(TranslationManager.GetStringById("badLuckCounter", GameManager.gm.badLuckCount.ToString()));

        success.SetText(TranslationManager.GetStringById("success"));

        _nextLevel = inGameWinStateButtons.transform.Find("ToNextLevelBtn").gameObject;
        _mainMenu = inGameWinStateButtons.transform.Find("ToMainMenuBtn").gameObject;

        _continueGame = pauseMenuButtons.transform.Find("Continue Button").gameObject;
        _exitGame = pauseMenuButtons.transform.Find("Exit Level Button").gameObject;

        _nextLevelText = _nextLevel.GetComponentInChildren<TextMeshProUGUI>();
        _mainMenuText = _mainMenu.GetComponentInChildren<TextMeshProUGUI>();

        _continueGameText = _continueGame.GetComponentInChildren<TextMeshProUGUI>();
        _exitGameText = _exitGame.GetComponentInChildren<TextMeshProUGUI>();

        _nextLevelText.SetText(TranslationManager.GetStringById("btnNextLevel"));
        _mainMenuText.SetText(TranslationManager.GetStringById("btnMainMenu"));

        _continueGameText.SetText(TranslationManager.GetStringById("btnContinueGame"));
        _exitGameText.SetText(TranslationManager.GetStringById("btnMainMenu"));

        inGameWinStateButtons.SetActive(false);
        pauseMenuButtons.SetActive(false);
        tutorialPanel.SetActive(false);
        
        GameManager.OnGameStateChanged += OnGameStateChanged;

        GameManager.OnGUICall += OnUIStateChanged;
    }

    private void Start()
    {
        _nextLevel.GetComponent<Button>().onClick.AddListener(NextLevel);
        _mainMenu.GetComponent<Button>().onClick.AddListener(MainMenu);
        _continueGame.GetComponent<Button>().onClick.AddListener(ContinueGame);
        _exitGame.GetComponent<Button>().onClick.AddListener(ExitGame);

    }

    private void OnGameStateChanged(GameState state)
    {
        switch (state)
        {
            case GameState.BadLuck:
                // badLuck.SetText(TranslationManager.GetStringById("badLuckCounter"), GameManager.gm.badLuckCount);
                break;
            case GameState.Win:
                success.enabled = true;
                inGameWinStateButtons.SetActive(true);
                SelectMenuItem(inGameWinStateButtons.transform.Find("ToNextLevelBtn").gameObject);
                break;
            case GameState.Reset:
                success.enabled = false;
                inGameWinStateButtons.SetActive(false);
                break;
        }
    }

    private void OnUIStateChanged(UIState state)
    {
       
        switch (state)
        {
            case UIState.Tutorial:
                tutorialPanel.SetActive(true);
                tutorialText.SetText(TranslationManager.GetStringById("tutorialLv" + GameManager.gm.levelNumber));
                break;
            case UIState.Normal:
                tutorialPanel.SetActive(false);
                break;
        }
    
    }

    public void NextLevel()
    {
        GameManager.gm.UpdateGameState(GameState.NextLevel);
    }

    public void MainMenu()
    {
       
        GameManager.gm.UpdateGameState(GameState.MainMenu);
        SceneManager.LoadScene("Scenes/MainMenuScene", LoadSceneMode.Single);
    }

    public void ContinueGame()
    {
        PauseMenuToggle();
     
    }

    public void ExitGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Scenes/MainMenuScene", LoadSceneMode.Single);
      
    }

    // Update is called once per frame
    void Update()
    {


        var gamepad = Gamepad.current;

        timer.SetText("{0:00}:{1:00}:{2:000}", GameManager.gm.min, GameManager.gm.sec, GameManager.gm.millisec);

        keysCollected.SetText(GameManager.gm.keysCollected + " / " + (GameManager.gm.numberOfKeys -1));

        if (Input.GetKeyUp(KeyCode.Escape) || ((gamepad != null)?gamepad.startButton.wasPressedThisFrame:false))
        {
           
            PauseMenuToggle();
        }

        if (pauseMenuButtons.activeSelf)
        {
            if (gamepad != null)
            {
                if (gamepad.xButton.wasPressedThisFrame || gamepad.aButton.wasPressedThisFrame)
                {

                    if (EventSystem.current.currentSelectedGameObject.name == "Continue Button")
                    {
                        PauseMenuToggle();

                    }
                    else
                    {

                        ExitGame();
                    }

                }
            }
        }

    }

    private void PauseMenuToggle()
    {// oh no if else without early return. Cry about it

        if (GameManager.gm.State == GameState.GamePlay)
        {
            if (_pauseMenuVisible)
            {

                pauseMenuButtons.SetActive(false);
                SelectMenuItem(pauseMenuButtons.transform.Find("Continue Button").gameObject);
                Time.timeScale = 1f;
            }
            else
            {

                pauseMenuButtons.SetActive(true);
                Time.timeScale = 0f;
            }

            _pauseMenuVisible = !_pauseMenuVisible;
        }
    }
    private void OnDestroy()
    {
        GameManager.OnGameStateChanged -= OnGameStateChanged;
        GameManager.OnGUICall -= OnUIStateChanged;
    }

    private void SelectMenuItem(GameObject item)
    {
        // why you have to make it null to make it something else one wonders
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(item);

    }
}
