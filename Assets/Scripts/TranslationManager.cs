using System.IO;
using UnityEngine;
using Newtonsoft.Json.Linq;
using TMPro;

public static class TranslationManager { 

    private static readonly string _translationsFolderName = "Translations";
    private static string _translationsFolderPath;

    private static JObject _languageData;

    static TranslationManager()
    {
        
        _translationsFolderPath = Path.Combine(Application.persistentDataPath, _translationsFolderName);
        if (!Directory.Exists(_translationsFolderPath))
        {
            Directory.CreateDirectory(_translationsFolderPath);
        }

        TextAsset[] files = Resources.LoadAll<TextAsset>("Translations");

        foreach (TextAsset file in files)
        {

            string destinationPath = Path.Combine(Application.persistentDataPath, _translationsFolderPath, file.name + ".json");
     
            File.WriteAllText(destinationPath, file.text);
        }
    }

    public static void SetLanguage(string lang = "eng")
    {

        if (lang == "")
        {
            lang = "eng";
        }

        _languageData = JObject.Parse(
            File.ReadAllText(
                Path.Combine(
                    Application.persistentDataPath, _translationsFolderPath,lang + ".json"
                    )
                )
            );

    }

    public static string GetStringById(string stringID)
    {

        if (_languageData[stringID] == null) return "derp";

        return _languageData[stringID].Value<string>();
  
    }

    public static string GetStringById(string stringID, string value)
    {

        if (_languageData[stringID] == null) return "derp";

        return string.Format(_languageData[stringID].Value<string>(),value);

    }

}
