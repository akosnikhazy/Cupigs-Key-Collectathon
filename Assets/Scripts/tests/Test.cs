using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    GameSaveData save;

    // Start is called before the first frame update
    void Start()
    {
        save = new GameSaveData();
        save.lang = "lol";

        SaveManagerJSON.Save(save);
        SaveManagerJSON.Load();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
