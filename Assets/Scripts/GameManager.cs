using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;


public class GameManager : MonoBehaviour
{

    [Header("Every Object Controlled by GM")]
    public GameObject platform;
    public GameObject platformHolder;
    public GameObject fixedPlatformHolder;
    public GameObject environmentHolder;
    public GameObject player;
    public GameObject cam;
    public GameObject poo;
    public GameObject meteorSpawner;

    [Header("INI")]
    public int levelNumber = 0;
    public float stepTime = 0.25f;
    public float pooTimeLimit = 60f;
    public int pooCountLimit = 100;

    [Header("For other classes")]
    public int numberOfKeys = 0;
    public int keysCollected = 0;

    [Header("shouldn't be public tho")]
    public bool firstRun = true;


    [Header("Background Color Palette")]
    [SerializeField] public Color[] worldOneColors;
    [SerializeField] public Color[] worldTwoColors;
    [SerializeField] public Color[] worldThreeColors;
    [SerializeField] public Color[] worldFourColors;

    public Color[] backgroundColorsNow;

    [Header("Sound")]
    public AudioClip buttonHover;
    public AudioClip world1Music;
    public AudioClip world2Music;
    public AudioClip world3Music;

    private AudioSource _buttonSounds;
    
    // teleport setup. Wasteful on levels without one.
    private LineRenderer _lineRenderer; 
    private GameObject _tA = null;
    private GameObject _tB = null;

    // the pig poos when you don't move
    private float _pooTimer = 0;
    private int _pooCount  = 0;


    // Everything that deos not disappear or do not need to destroy to win goes in the platformHolder.
    // listing them here so it is faster to decide
    private int[] _fixedPlatforms = { 2, 4, 8, 9, 10, 11 };

    // all the levels loaded at once, this is unreasonable but too late to change logic.
    private List<int[,]> _levels = LevelDataStructure.GetLevelList();

    public GameState State;
    public UIState GUIState;
    public static event Action<GameState> OnGameStateChanged;
    public static event Action<UIState> OnGUICall;

    public int badLuckCount = 0;

    public static GameManager gm;
    GameSaveData save;

    private bool _levelHasTeleportPair = false;

    //  private SaveManager sgm = new SaveManager();
    public int sec { get; private set; }
    public int min { get; private set; }
    public int millisec { get; private set; }

    private List<GameObject> _allPlatforms = new List<GameObject>();
    private List<GameObject> _winPlatforms = new List<GameObject>();
 
    GameObject[] _gems;

    private float _timer = 0f;

    private int[] _startCoordinate;

    private float[] _playerStartingRotation;

    private void Awake()
    {
    
        gm = this;

        save = SaveManagerJSON.Load();

        TranslationManager.SetLanguage(PlayerPrefs.GetString("language"));

        levelNumber = save.level;

        if (levelNumber > 100) levelNumber = 100;

        int isSelectedLevel = PlayerPrefs.GetInt("SelectedLevel");

        if (isSelectedLevel > 0)
        {
            levelNumber = isSelectedLevel;
        }

        player.GetComponent<PlayerController>().pipe.SetActive(false);

        _buttonSounds = GetComponent<AudioSource>();
        
        /*
        save.level = 1;
        SaveManagerJSON.Save(save);
        */
    }

  

    public void ResetPooTimer() {
        _pooTimer = 0f;
    }

 
    public bool IsWin()
    {

        foreach (GameObject winPlatform in _winPlatforms) 
        {
            // find active means it is a fail
            if (winPlatform.activeInHierarchy)  return false; 

        }
       
        UpdateGameState(GameState.Win);

        return true; // if every destroyable platform is inactive (except the finish platform) the player won.
    }

    public void SaveTime()
    {

        if (levelNumber == save.level)
        {   // here we open new levels if needed
            save.level = levelNumber + 1;
           /*
            if( save.level >= 34 )
            {
                SteamManager.UnlockAchievement("HOLD_BREATH");
            }
            else
            if( save.level >= 50)
            {
             SteamManager.UnlockAchievement("HALF_THERE");
             }
            else
             if( save.level >= 67)
            {
             SteamManager.UnlockAchievement("SPACE");
             }
            else
             if( save.level >=100 )
             {
                   SteamManager.UnlockAchievement("DONE");
             
             }*/
        }

        if (save.level > 100) save.level = 100;

        string levelName = string.Format("lv{0}scr", levelNumber);

        FieldInfo fieldInfo = typeof(GameSaveData).GetField(levelName);

        if (fieldInfo != null)
        {
            if ((float)fieldInfo.GetValue(save) > _timer || (float)fieldInfo.GetValue(save) == 0)
            {
                fieldInfo.SetValue(save, _timer);
            }
        }
        
        SaveManagerJSON.Save(save);

    }
  
    public void Ini(bool newLevel = false)
    {
        // a frame of bad luck so we tell the UI to show the count
        UpdateGameState(GameState.BadLuck);

        string now;

        /*
         *  the player starts from there every time. Reset the level
         */
        player.transform.eulerAngles = new Vector3(
            _playerStartingRotation[0],
            _playerStartingRotation[1],
            _playerStartingRotation[2]
        );

        




        if (newLevel) 
        {
            
            foreach (Transform child in fixedPlatformHolder.transform)
            {
                GameObject.Destroy(child.gameObject);
            }

            foreach (Transform child in platformHolder.transform)
            {
                GameObject.Destroy(child.gameObject);
            }

            levelNumber += 1;

            if (levelNumber > save.level)
            {
                save.level = levelNumber;
                SaveManagerJSON.Save(save);
            }

            firstRun = true;
            _levelHasTeleportPair = false;

            numberOfKeys = 0;
           
            _timer = 0;
            sec = min = millisec =  0;
           
            badLuckCount = 0;

            _winPlatforms.Clear(); 
        }

        if (firstRun)
        {   
            /*
             *  Build level
             */
            int teleportCounter = 0;
            for (int z = 0; z < _levels[levelNumber-1].GetLength(0); z++)
            {// looping throught the level 2D array grid

                for (int x = 0; x < _levels[levelNumber - 1].GetLength(1); x++)
                {

                    if (_levels[levelNumber - 1][z, x] > 0)
                    {

                        // create the level

                        switch (_levels[levelNumber - 1][z, x])
                        {
                            case 1:
                                now = "Start";
                                break;
                            case 2:
                                now = "Finish";
                                break;
                            case 3:
                                now = "Normal";
                                break;
                            case 4:
                                now = "Wall";
                                break;
                            case 5:
                                now = "Counter";
                                break;
                            case 6:
                                now = "KillOthers";
                                break;
                            case 7:
                                now = "KillOthersUp";
                                break;
                            case 8:
                                now = "Teleport";
                                break;
                            case 9:
                                now = "Forever";
                                break;
                            case 10:
                                now = "Canon";
                                break;
                            case 11:
                                now = "Shark Generator";
                                break;
                            default:
                                now = "Normal";
                                break;
                        }

                        GameObject pNow = findChild(now, platform);

                        if (pNow != null)
                        {// null reference doesn't kill it but this is nicer
                            GameObject platforms = Instantiate(pNow, new Vector3(z, 0, x), Quaternion.identity);

                            platforms.name = "p_" + x + '_' + z;

                            _allPlatforms.Add(platforms);

                            if (_levels[levelNumber - 1][z, x] == 8)
                            {

                                if (teleportCounter++ == 1)
                                {

                                    platforms.name = "TeleportA";
                                    _tA = platforms;

                                }
                                else
                                {

                                    platforms.name = "TeleportB";
                                    _tB = platforms;

                                }

                                _levelHasTeleportPair = true;

                            }

                            // set the starting platform
                            if (_levels[levelNumber - 1][z, x] == 1) 
                            {

                                _startCoordinate = new int[] { x, z };

                            }

                            // Everything that does not disappear or do not need to destroy to win goes in the platformHolder.
                            if (!_fixedPlatforms.Contains(_levels[levelNumber - 1][z, x]))
                            {

                                platforms.transform.parent = platformHolder.transform;
                                _winPlatforms.Add(platforms);
                                numberOfKeys++;

                            }
                            else
                            {

                                platforms.transform.parent = fixedPlatformHolder.transform;

                            }
                        }
                    }
                }

                _gems = GameObject.FindGameObjectsWithTag("Gem");

            }


            var tutorialLevel = new int[] { 1, 2, 6, 34, 67 }; // this will turn on tutorial text. The text based on the level number in Translation manager.
            
            if (tutorialLevel.Contains(levelNumber))
            {
                UpdateGUI(UIState.Tutorial);
            }
            else
            {
                UpdateGUI(UIState.Normal);
            }

                PlayerController p = player.GetComponent<PlayerController>();

                int envNumber = 0;

                p.pipe.SetActive(false);
                p.backpack.SetActive(false);
                p.spaceBubble.SetActive(false);

                void SetupWorld( int env, AudioClip music, Color[] colors, bool pipe, bool backpack, bool bubble)
                {
                    envNumber = env;
                
                    AudioManager.PlayMusic(_buttonSounds, music);
                
                    backgroundColorsNow = colors;

                    p.pipe.SetActive(pipe);
                    p.backpack.SetActive(backpack);
                    p.spaceBubble.SetActive(bubble);
                }

                if (levelNumber <= 33)
                {
                    SetupWorld(1, world1Music, worldOneColors, false, false, false);
                } else

                if(levelNumber > 33 && levelNumber <= 66)
                {
                    SetupWorld( 2, world2Music, worldTwoColors, true, false, false);
                } else

                if(levelNumber > 66)
                {
                    SetupWorld( 3,  world3Music, worldThreeColors, false, true, true);
                }
           
            foreach (Transform env in environmentHolder.transform)
            {
                // Set each child's active state to false
                env.gameObject.SetActive(false);
            }
           
            environmentHolder.transform.Find("env-" + envNumber).gameObject.SetActive(true);

            firstRun = !firstRun;
        } // if firts run
        
    }
    // Start is called before the first frame update
    void Start()
    {
        /*
        if (save.level >= 34)
        {
            SteamManager.UnlockAchievement("HOLD_BREATH");
        }
      
        if (save.level >= 50)
        {
            SteamManager.UnlockAchievement("HALF_THERE");
        }
       
        if (save.level >= 67)
        {
            SteamManager.UnlockAchievement("SPACE");
        }
       
        if (save.level >= 100)
        {
            SteamManager.UnlockAchievement("DONE");

        }
        */

        // saving the starting rotation of the player
        _playerStartingRotation = new float[] { player.transform.eulerAngles.x, player.transform.eulerAngles.y, player.transform.eulerAngles.z };

        _lineRenderer = gameObject.GetComponent<LineRenderer>();

        UpdateGameState(GameState.GamePlay);

        // starting the game
        Ini();

        

       

       

        if (_levelHasTeleportPair && _lineRenderer != null && _tA != null && _tB != null)
        {

            _lineRenderer.enabled = false;
            _lineRenderer.SetPosition(0, new Vector3(_tA.transform.position.x, _tA.transform.position.y + 0.5f, _tA.transform.position.z));
            _lineRenderer.SetPosition(1, new Vector3(_tB.transform.position.x, _tB.transform.position.y + 0.5f, _tB.transform.position.z));

        }

    }

    void FixedUpdate()
    {
        
        if (_levelHasTeleportPair && _lineRenderer != null && _tA != null && _tB != null)
        {
            
            if (_tA.gameObject.GetComponent<PlatformTeleport>().playerIsClose || _tB.gameObject.GetComponent<PlatformTeleport>().playerIsClose)
            {

                _lineRenderer.enabled = true;
                
            }
            else
            {

                _lineRenderer.enabled = false;

            }
        }

        if (GameState.GamePlay == State) {
            
            _timer += Time.deltaTime;
            _pooTimer += Time.deltaTime;

            if (_pooTimer > pooTimeLimit)
            { // very important code, imagine making four variables for the pig to take a shit
                if (_pooCount < pooCountLimit)
                {
                    Instantiate(poo, player.transform.Find("Chocolate Starfish").gameObject.transform.position, poo.transform.rotation);
                    _pooCount++;
                    ResetPooTimer();
                    // SteamManager.UnlockAchievement("SMELLY");
                }
            }

            // Convert the elapsed time to a TimeSpan
            TimeSpan elapsed = TimeSpan.FromSeconds(_timer);

            // Extract the minutes, seconds, and milliseconds from the elapsed time
            min = elapsed.Minutes;
            sec = elapsed.Seconds;
            millisec = elapsed.Milliseconds;
        }
       
        if (GameState.BadLuck == State)
        {
            badLuckCount++;

            player.transform.position = new Vector3(_startCoordinate[1], 0.55f, _startCoordinate[0]);
            UpdateGameState(GameState.Reset);
            return;
        }

        if (GameState.Reset == State)
        {
           
            /* on restart reactivate every platform */
            foreach (GameObject winPlatform in _winPlatforms)
            {
                winPlatform.SetActive(true);
                
            }
         
            /* clanky but works well you can not
             * make them appear if they disappeard on their own
             */
            foreach (GameObject gem in _gems)
            {
              
                gem.SetActive(true);

            }

            keysCollected = 0;
          
            /* on restart reset the counters */
            GameObject[] counters = GameObject.FindGameObjectsWithTag("Counter");

            if (counters.Length > 0)
            {
                foreach (GameObject c in counters)
                {
                    PlatformCounter platformCounter = c.GetComponent<PlatformCounter>();
                    if (platformCounter != null)
                    {

                        platformCounter.touchCount = 3;
                        platformCounter.counterNumber.SetText(platformCounter.touchCount.ToString());
                    }
                }
            }

            UpdateGameState(GameState.GamePlay);

        }

        if (GameState.NextLevel == State)
        {
           
            Ini(true);

        }
    }

    public void UpdateGameState(GameState newState)
    {
        State = newState;

        switch (newState)
        {
            case GameState.MainMenu:
                break;
            case GameState.SelectLevel:
                break;
            case GameState.GamePlay:
                break;
            case GameState.BadLuck:
                break;
            case GameState.Reset:
                break;
            case GameState.NextLevel:
                break;
            case GameState.ExitPrompt:
                break;
            case GameState.Win:
                break;
        }

        OnGameStateChanged?.Invoke(newState);

    }
    
    public void UpdateGUI(UIState newState)
    {
        GUIState = newState;

        switch (newState)
        {
           
            case UIState.Tutorial:
                break;
            case UIState.Normal:
                break;
        }

        OnGUICall?.Invoke(newState);

    }
    GameObject findChild(string name, GameObject platform)
    {

        foreach (Transform child in platform.transform)
        {
            if (child.name == name)
            {
                return child.gameObject;
            }
        }
        return null;
    }

}

public enum GameState
{
    MainMenu,
    SelectLevel,
    GamePlay,
    BadLuck,
    Reset,
    NextLevel,
    ExitPrompt,
    Win
}

public enum UIState
{ 
    Normal,
    Tutorial
    
}

public enum LanguageCodes
{ 
    eng,
    hun
}
   
