using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.Events;

public class MainMenu : MonoBehaviour
{
    [Header("Event System")]
    public GameObject mainMenuFirstSelected;
    public GameObject levelSelectMenuFirstSelected;
    public GameObject settingsMenuFirstSelected;
    public GameObject mainMenuFirstSelectedSettingsClosed;
    public GameObject mainMenuFirstSelectedLevelSelectorClosed;


    [Header("Main Menu Buttons")]
    public Button levelSelectButton;
    public Button playButton;
    public Button settingsButton;
    public Button exitButton;

    [Header("Level Select Menu Parts")]
    public Button levelSelectCloseButton;
    public Button startSelectedLevel;
    public GameObject levelButtonHolder;
    public GameObject lv1btn;
    public int totalButtons = 100;
    public float yOffset = -116.80001f;

    [Header("Settings Menu Buttons")]
    public Slider SFXVolume;
    public Slider musicVolume;
    public TMP_Dropdown languageSelect;
    public Button settingsMenuCloseButton;
    public Button creditsPanelButton;
    public Button creditsCloseButton;


    [Header("Main Menu Dialogs")]
    public GameObject settingsMenu;
    public GameObject levelSelectMenu;
    public GameObject dialog;
    public GameObject creditsPanel;
  
    [Header("Text")]
    public TMP_Text settingsTitleText;
    public TMP_Text levelTime;
    public TMP_Text SFXVolumeText;
    public TMP_Text musicVolumeText;
    public TMP_Text languageSelectText;
    public TMP_Text languageSelectReloadPlease;
    public TMP_Text helloUserText;
    public TMP_Text credits;
    public TMP_Text createdBy;
    public TMP_Text musicCredit;
    public TMP_Text translations;
    public TMP_Text german;
    public TMP_Text ukranian;
    public TMP_Text additionalAssets;

    [Header("URLs")]
    public Button germanLink;
    public Button ukranianLink;
    public Button steamLink;

    [Header("Helpers")]
    public RawImage tick;

    [Header("Sound")]
    public AudioClip buttonHover;
    public AudioClip menuMusic;



    private const string LANG_KEY = "language";

    // private SaveManager sgm = new SaveManager();

    private GameSaveData _sdata;

    private int _selectedLevel = 1;
    private bool _levelSelectorOpen = false;

    private AudioSource _buttonSounds;
    private AudioSource _music;

    private string _langNow;

    private GameObject _openedPanel;

    // public PassData dataHolder;
    private void Awake() {
    
         
        
        _langNow = PlayerPrefs.GetString(LANG_KEY);

        if (string.IsNullOrEmpty(_langNow)) _langNow = "eng";
       

        TranslationManager.SetLanguage(_langNow);

        /* 
         * Should find a better way for this. For example TranslationManager could automatically hook in every text by name?
         * Thoughs for later
        */
        levelSelectButton.GetComponentInChildren<TextMeshProUGUI>().SetText(TranslationManager.GetStringById("btnLevelSelect"));
        playButton.GetComponentInChildren<TextMeshProUGUI>().SetText(TranslationManager.GetStringById("btnPlayGame"));
        settingsButton.GetComponentInChildren<TextMeshProUGUI>().SetText(TranslationManager.GetStringById("btnSettings"));
        exitButton.GetComponentInChildren<TextMeshProUGUI>().SetText(TranslationManager.GetStringById("btnExitGame"));

        settingsMenu.SetActive(false);
        levelSelectMenu.SetActive(false);
        dialog.SetActive(false);
        creditsPanel.SetActive(false);
        tick.gameObject.SetActive(false);
        languageSelectReloadPlease.gameObject.SetActive(false);
      
        SFXVolume.value = AudioManager.GetSFXVol();
        musicVolume.value = AudioManager.GetMusicVol();

        settingsTitleText.SetText(TranslationManager.GetStringById("settingsTitleText"));
        languageSelectReloadPlease.SetText(TranslationManager.GetStringById("settingsLangSelectReloadMessage"));
        levelTime.SetText(TranslationManager.GetStringById("levelTime"), '0');
        SFXVolumeText.SetText(TranslationManager.GetStringById("SFXVolumeText", Math.Round(SFXVolume.value * 100).ToString()));
        musicVolumeText.SetText(TranslationManager.GetStringById("musicVolumeText", Math.Round(musicVolume.value * 100).ToString()));
        languageSelectText.SetText(TranslationManager.GetStringById("languageSelectText"));
        creditsPanelButton.GetComponentInChildren<TextMeshProUGUI>().SetText(TranslationManager.GetStringById("credits"));
        credits.SetText(TranslationManager.GetStringById("credits"));
        createdBy.SetText(TranslationManager.GetStringById("createdby","Ákos Nikházy"));
        musicCredit.SetText(TranslationManager.GetStringById("music", "kenney.nl"));
        translations.SetText(TranslationManager.GetStringById("languageSelectText"));
        german.SetText(TranslationManager.GetStringById("germanTranslation", "niiamin"));
        ukranian.SetText(TranslationManager.GetStringById("ukranianTranslation", "Shorokhova Valentyna"));
        additionalAssets.SetText(TranslationManager.GetStringById("additionalAssets", "Ovani Sound, Mixit sounds, Wiggly Curves Font by Niskala Huruf"));

        _buttonSounds = GetComponent<AudioSource>();
      

        languageSelect.ClearOptions(); // why whe have to do this?
        languageSelect.AddOptions(new List<string> { 
                                    TranslationManager.GetStringById("englishLanguageName"),
                                    TranslationManager.GetStringById("hungarianLanguageName"),
                                    TranslationManager.GetStringById("ukrainianLanguageName"),
                                    TranslationManager.GetStringById("germanLanguageName"),
                                    /*TranslationManager.GetStringById("spanishLanguageName"),
                                    TranslationManager.GetStringById("mandarinLanguageName"),
                                    TranslationManager.GetStringById("koreanLanguageName"),
                                    TranslationManager.GetStringById("japaneseLanguageName")*/
                                    }); 

       
       
        helloUserText.text = TranslationManager.GetStringById("hello") + "";
    }
    void Start()
    {
        AudioManager.PlayMusic(_buttonSounds, menuMusic);
    
        // _sdata = sgm.Load();
        _sdata = SaveManagerJSON.Load();

        // Reset the game for testing
        // _sdata.level = 10;

        // sgm.Save(_sdata);
        SaveManagerJSON.Save(_sdata);

        // _sdata = sgm.Load();
        _sdata = SaveManagerJSON.Load();

       // Debug.Log(_sdata);

        if (_sdata.level == 1) levelSelectButton.gameObject.SetActive(false);

        settingsButton.onClick.AddListener(OpenSettings);
        levelSelectButton.onClick.AddListener(OpenLevelSelect);
        creditsPanelButton.onClick.AddListener(OpenCreditsPanel);
        playButton.onClick.AddListener(StartNewGame);
        levelSelectCloseButton.onClick.AddListener(CloseLevelSelect);
        settingsMenuCloseButton.onClick.AddListener(CloseMenuPanel);
        creditsCloseButton.onClick.AddListener(CloseCreditsPanel);
        SFXVolume.onValueChanged.AddListener(OnSFXVolumeChanged);
        musicVolume.onValueChanged.AddListener(OnMusicVolumeChanged);
        exitButton.onClick.AddListener(ExitGame);
        germanLink.onClick.AddListener(OpenGermanWebsite);
        ukranianLink.onClick.AddListener(OpenUkranianWebsite);
        steamLink.onClick.AddListener(OpenSteamPage);

        int basicSelectedLanguage = 0;
        
        switch (_langNow)
        {
            case "eng": // English
                basicSelectedLanguage = 0;
                break;
            case "hun": // Hungarian
                basicSelectedLanguage = 1;
                break;
            case "ukr": // Ukrainian
                basicSelectedLanguage = 2;
                break;

            case "deu": // German
                basicSelectedLanguage = 3;
                break;

            /*
            case "spa": // Spanish
                basicSelectedLanguage = 4;
                break;

            case "zho": // Mandarin
                basicSelectedLanguage = 5;
                break;

            case "jpn": // Japanese
                basicSelectedLanguage = 6;
                break;

            case "kor": // Korean
                basicSelectedLanguage = 7;
                break;
            */
            

        }

        languageSelect.value = basicSelectedLanguage;

        languageSelect.onValueChanged.AddListener(delegate {
            DropdownValueChanged(languageSelect);
        });

        for (int i = 2; i <= totalButtons; i++)
        {
            GameObject newBtn = Instantiate(lv1btn, lv1btn.transform.parent);
            newBtn.name = $"lv{i}btn";

            // Set new position with offset
            Vector3 newPosition = lv1btn.transform.localPosition;
            newPosition.y += yOffset * (i - 1); // Offset based on the button index
            newBtn.transform.localPosition = newPosition;

            TextMeshProUGUI tmpText = newBtn.GetComponentInChildren<TextMeshProUGUI>();
            if (tmpText != null)
            {

                tmpText.GetComponentInChildren<TextMeshProUGUI>().SetText(TranslationManager.GetStringById("level"), i);
            }
            else
            {
                Debug.LogWarning($"TextMeshProUGUI not found in {newBtn.name}");
            }
        }

    }

    // at this point I am this lazy
    private void OpenGermanWebsite()
    {
        Application.OpenURL("https://instagram.com/niiamin_cos");
       
    }

    private void OpenUkranianWebsite()
    {
        Application.OpenURL("https://instagram.com/cassepupa");

    }

    private void OpenSteamPage()
    {
        Application.OpenURL("https://store.steampowered.com/app/2999220/Cupigs_Key_Collectathon/");
    }



    private void OpenSettings(){
        _openedPanel = settingsMenu;
        settingsMenu.SetActive(true);
        SelectMenuItem(settingsMenuFirstSelected);

    }

    private void ExitGame()
    {
        Application.Quit();
    }
   
    private void OpenLevelSelect() {
       
        levelSelectMenu.SetActive(true);
        startSelectedLevel.gameObject.SetActive(false);

        _openedPanel = levelSelectMenu;
        
            for (var i = 1; i <= 100; i++)
            {
            
                if (i <= _sdata.level) {
                    int thisLevelBtnNumber = i;
                    GameObject thisLevelBtn = GameObject.Find("lv" + i + "btn");
                    thisLevelBtn.GetComponent<Button>().onClick.AddListener(() => SetupLevelToStart(thisLevelBtnNumber));
                    thisLevelBtn.GetComponentInChildren<TextMeshProUGUI>().SetText(TranslationManager.GetStringById("level"),i);
                    continue;
                }

            
                if (_levelSelectMenuFirstOpen)
                {
                    Debug.Log(i);
                    GameObject.Find("lv" + i + "btn").gameObject.SetActive(false);
                }
            }

        
        SelectMenuItem(GameObject.Find("lv"+ _selectedLevel + "btn"));
        SetupLevelToStart(_selectedLevel);

        _levelSelectorOpen = true;
        _levelSelectorCanMove = true;
        _levelSelectMenuFirstOpen = false;

    }

    private void OpenCreditsPanel()
    {
        creditsPanel.SetActive(true);
    }
    
    private void CloseLevelSelect()
    {
        _openedPanel = null;
        levelSelectMenu.SetActive(false);
        SelectMenuItem(mainMenuFirstSelectedLevelSelectorClosed);
    }

    private void SetupLevelToStart(int lv)
    {
        string levelName = string.Format("lv{0}scr", lv);
        FieldInfo fieldInfo = typeof(GameSaveData).GetField(levelName);


        if (fieldInfo != null)
        {
            startSelectedLevel.gameObject.SetActive(true);

           

            levelTime.SetText(TranslationManager.GetStringById("levelTime", fieldInfo.GetValue(_sdata).ToString()));
           
        }

        startSelectedLevel.GetComponent<Button>().onClick.RemoveAllListeners();
        startSelectedLevel.GetComponent<Button>().onClick.AddListener(() => StartSelectedLevel(lv));
       // _levelSelectorOpen = false;
    }

    private void StartSelectedLevel(int lv)
    {
      

        PlayerPrefs.SetInt("SelectedLevel", lv);
        PlayerPrefs.Save();
        SceneManager.LoadScene("Scenes/GameScene", LoadSceneMode.Single);
    }

    private void StartNewGame() {
        if (_sdata.level > 1)
        {
            dialog.SetActive(true);
        }
        
        PlayerPrefs.SetInt("SelectedLevel", 0);
        SceneManager.LoadScene("Scenes/GameScene", LoadSceneMode.Single);

    }


    private void OnSFXVolumeChanged(float value)
    {
        AudioManager.SetSFXVol(value);
        SFXVolumeText.SetText(TranslationManager.GetStringById("SFXVolumeText", Math.Round(SFXVolume.value * 100).ToString()));
      
    }

    private void OnMusicVolumeChanged(float value)
    {
        AudioManager.SetMusicVol(value);
        musicVolumeText.SetText(TranslationManager.GetStringById("musicVolumeText", Math.Round(musicVolume.value*100).ToString()));

    }
    
    private void CloseMenuPanel()
    {
        // so this is lazy but closes any menu
        // without further code
        // levelSelectMenu.SetActive(false);
        _openedPanel = null;
        settingsMenu.SetActive(false);
        SelectMenuItem(mainMenuFirstSelectedSettingsClosed);
    }
   
    private void CloseCreditsPanel()
    {
        // so this is lazy but closes any menu
        // without further code
        // levelSelectMenu.SetActive(false);
        _openedPanel = null;
        creditsPanel.SetActive(false);
        
    }

    
    void DropdownValueChanged(TMP_Dropdown dropdown)
    {
       
        /*
         * "englishLanguageName": "English" ,
	     *   "hungarianLanguageName": "Hungarian",
	     *   "ukranianLanguageName":  "Ukranian",
	     *   "spanishLanguageName": "Spanish",
	     *   "mandarinLanguageName": "Mandarin",
	     *   "japaneseLanguageName": "Japanese",
	     *   "koreanLanguageName": "Korean",
         * 
         */
        
        switch (dropdown.value)
        {
            case 0: // English
                PlayerPrefs.SetString(LANG_KEY, "eng");
                break;
            case 1: // Hungarian
                PlayerPrefs.SetString(LANG_KEY, "hun");
                break;
            case 2: // Ukrainian
                PlayerPrefs.SetString(LANG_KEY, "ukr");
                break;

            case 3: // German
                PlayerPrefs.SetString(LANG_KEY, "deu");
                break;
            /*
            case 4: // Spanish
                PlayerPrefs.SetString(LANG_KEY, "spa");
                break;
           
            case 5: // Mandarin
                PlayerPrefs.SetString(LANG_KEY, "zho");
                break;
           
            case 6: // Japanese
                PlayerPrefs.SetString(LANG_KEY, "jpn");
                break;
           
            case 7: // Korean
                PlayerPrefs.SetString(LANG_KEY, "kor");
                break;
           */
        }

        PlayerPrefs.Save();
        languageSelectReloadPlease.gameObject.SetActive(true); // do not have to turn this off, as it is important info for the player.
        ShowTick();

    }

   
    private void SelectMenuItem(GameObject item) 
    {
        // why you have to make it null to make it something else one wonders
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(item);
    
    }
    private bool _levelSelectorCanMove = true;
    private bool _levelSelectMenuFirstOpen;

    private void Update()
    {
        var gamepad = Gamepad.current;

        var _y = Input.GetAxisRaw("Vertical");

        // this rewirtes _y if mouse is scrolled in this frame
        if (Input.mouseScrollDelta.y != 0) 
            _y = Input.mouseScrollDelta.y;

        // if we are on the level select menu, _y has value and we can move
        if (_levelSelectorOpen && _y != 0 && _levelSelectorCanMove)
        {
            
            _levelSelectorCanMove = false;


            if ((_y > 0 && _selectedLevel > 1) || (_y < 0 && _selectedLevel < _sdata.level))
            {
                Vector3 newPosition = levelButtonHolder.transform.position + new Vector3(0, -_y * 116.8f, 0); // should calculate this instead of hard coded float
                levelButtonHolder.transform.position = newPosition;

                if (_y < 0) _selectedLevel++;
                if (_y > 0) _selectedLevel--;

                SelectMenuItem(GameObject.Find("lv"+_selectedLevel+"btn"));
                SetupLevelToStart(_selectedLevel);
                AudioManager.PlaySFX(_buttonSounds, buttonHover);

            }

        }

        // this way we allow only one move per button press
        if (_y == 0)
        {
            _levelSelectorCanMove = true;
        }

        if (Input.GetKeyUp(KeyCode.Escape) || ((gamepad != null) ? gamepad.startButton.wasPressedThisFrame | gamepad.bButton.wasPressedThisFrame | gamepad.circleButton.wasPressedThisFrame : false))
        {
           
            if (_openedPanel != null)
            {
                _levelSelectorOpen = false;
                _openedPanel.SetActive(false);
                SelectMenuItem(mainMenuFirstSelected);

            }
        }


    }

   
    private void ShowTick()
    { // might chage this to fit any gameobject if needed
        tick.gameObject.SetActive(true);
        StartCoroutine(OffTick(3f));

    }
    IEnumerator OffTick(float delay)
    {
        yield return new WaitForSeconds(delay);
        tick.gameObject.SetActive(false);
    }
}
