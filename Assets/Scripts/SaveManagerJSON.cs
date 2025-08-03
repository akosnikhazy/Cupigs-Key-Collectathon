using System.IO;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;
using Newtonsoft.Json.Linq;
using System.Reflection;
using System;

public static class SaveManagerJSON 
{
    private static readonly string _saveFolderName = "Save";
    private static string _saveFolderPath;
    private static JObject _saveData;

   
    static SaveManagerJSON()
    {
        _saveFolderPath = Path.Combine(Application.persistentDataPath, _saveFolderName);
        if (!Directory.Exists(_saveFolderPath))
        {

            Directory.CreateDirectory(_saveFolderPath);
            File.WriteAllText(Path.Combine(Application.persistentDataPath, _saveFolderPath, "savestate.json"), JsonUtility.ToJson(new GameSaveData()));
        }
       
    }

   
    public static void Save(GameSaveData data)
    {
       
        File.WriteAllText(Path.Combine(Application.persistentDataPath, _saveFolderPath, "savestate.json"), JsonUtility.ToJson(data));

    }

  
    public static GameSaveData Load()
    {
        GameSaveData data = new GameSaveData();
        _saveData = JObject.Parse(
            File.ReadAllText(
                Path.Combine(
                    Application.persistentDataPath, _saveFolderPath, "savestate.json"
                    )
                )
            );

        FieldInfo[] fields = typeof(GameSaveData).GetFields(BindingFlags.Public | BindingFlags.Instance);
        
        foreach (var field in fields)
        {
          
            if (_saveData[field.Name] != null)
            {

                object value = Convert.ChangeType(_saveData[field.Name], field.FieldType);

                
                field.SetValue(data, value);
                
            }
        }
        
        return data;

    }

  
    private static string GenerateHash(string data, string salt)
    {

        using (SHA256 sha256Hash = SHA256.Create())
        {

            byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(data + salt));
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < bytes.Length; i++)
            {

                builder.Append(bytes[i].ToString("x2"));

            }
            return builder.ToString();

        }
    }
}
