using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;

public class SaveManager
{
    private const string LEVEL_KEY = "level";
    private const string LANG_KEY = "language";

    private const string LV1SCR_KEY = "level1score";
    private const string LV2SCR_KEY = "level2score";
    private const string LV3SCR_KEY = "level3score";
    private const string LV4SCR_KEY = "level4score";
    private const string LV5SCR_KEY = "level5score";
    private const string LV6SCR_KEY = "level6score";
    private const string LV7SCR_KEY = "level7score";
    private const string LV8SCR_KEY = "level8score";
    private const string LV9SCR_KEY = "level9score";
    private const string LV10SCR_KEY = "level10score";
    private const string LV11SCR_KEY = "level11score";
    private const string LV12SCR_KEY = "level12score";
    private const string LV13SCR_KEY = "level13score";
    private const string LV14SCR_KEY = "level14score";
    private const string LV15SCR_KEY = "level15score";
    private const string LV16SCR_KEY = "level16score";
    private const string LV17SCR_KEY = "level17score";
    private const string LV18SCR_KEY = "level18score";
    private const string LV19SCR_KEY = "level19score";
    private const string LV20SCR_KEY = "level20score";
    private const string LV21SCR_KEY = "level21score";
    private const string LV22SCR_KEY = "level22score";
    private const string LV23SCR_KEY = "level23score";
    private const string LV24SCR_KEY = "level24score";
    private const string LV25SCR_KEY = "level25score";
    private const string LV26SCR_KEY = "level26score";
    private const string LV27SCR_KEY = "level27score";
    private const string LV28SCR_KEY = "level28score";
    private const string LV29SCR_KEY = "level29score";
    private const string LV30SCR_KEY = "level30score";
    private const string HASH_KEY = "hash";
    private const string SALT_KEY = "salt";


    public void Save(GameSaveData data)
    {
     
        /* I just use the GenerateHash to make a salt */
        string salt = GenerateHash(DateTimeOffset.UtcNow.ToUnixTimeMilliseconds().ToString(), DateTimeOffset.UtcNow.ToUnixTimeMilliseconds().ToString());
        // string hashedData = GenerateHash($"{data.level}:{data.lang}:{data.lv1scr}:{data.lv2scr}:{data.lv3scr}:{data.lv4scr}:{data.lv5scr}:{data.lv6scr}:{data.lv7scr}:{data.lv8scr}:{data.lv9scr}:{data.lv10scr}::{data.lv11scr}:{data.lv12scr}:{data.lv13scr}:{data.lv14scr}:{data.lv15scr}:{data.lv16scr}:{data.lv17scr}:{data.lv18scr}:{data.lv19scr}:{data.lv20scr}:{data.lv21scr}:{data.lv22scr}:{data.lv23scr}:{data.lv24scr}:{data.lv25scr}:{data.lv26scr}:{data.lv27scr}:{data.lv28scr}:{data.lv29scr}:{data.lv30scr}", salt);
       //  string hashedData = GenerateHash($"{data.level}:{data.lang}:{data.scoreNameDictionary["lv1scr"]}:{data.scoreNameDictionary["lv2scr"]}:{data.scoreNameDictionary["lv3scr"]}:{data.scoreNameDictionary["lv4scr"]}:{data.scoreNameDictionary["lv5scr"]}:{data.scoreNameDictionary["lv6scr"]}:{data.scoreNameDictionary["lv7scr"]}:{data.scoreNameDictionary["lv8scr"]}:{data.scoreNameDictionary["lv9scr"]}:{data.scoreNameDictionary["lv10scr"]}::{data.scoreNameDictionary["lv11scr"]}:{data.scoreNameDictionary["lv12scr"]}:{data.scoreNameDictionary["lv13scr"]}:{data.scoreNameDictionary["lv14scr"]}:{data.scoreNameDictionary["lv15scr"]}:{data.scoreNameDictionary["lv16scr"]}:{data.scoreNameDictionary["lv17scr"]}:{data.scoreNameDictionary["lv18scr"]}:{data.scoreNameDictionary["lv19scr"]}:{data.scoreNameDictionary["lv20scr"]}:{data.scoreNameDictionary["lv21scr"]}:{data.scoreNameDictionary["lv22scr"]}:{data.scoreNameDictionary["lv23scr"]}:{data.scoreNameDictionary["lv24scr"]}:{data.scoreNameDictionary["lv25scr"]}:{data.scoreNameDictionary["lv26scr"]}:{data.scoreNameDictionary["lv27scr"]}:{data.scoreNameDictionary["lv28scr"]}:{data.scoreNameDictionary["lv29scr"]}:{data.scoreNameDictionary["lv30scr"]}", salt);

        PlayerPrefs.SetInt(LEVEL_KEY, data.level);
        PlayerPrefs.SetString(LANG_KEY, data.lang);
        /*
        PlayerPrefs.SetFloat(LV1SCR_KEY, data.scoreNameDictionary["lv1scr"]);
        PlayerPrefs.SetFloat(LV2SCR_KEY, data.scoreNameDictionary["lv2scr"]);
        PlayerPrefs.SetFloat(LV3SCR_KEY, data.scoreNameDictionary["lv3scr"]);
        PlayerPrefs.SetFloat(LV4SCR_KEY, data.scoreNameDictionary["lv4scr"]);
        PlayerPrefs.SetFloat(LV5SCR_KEY, data.scoreNameDictionary["lv5scr"]);
        PlayerPrefs.SetFloat(LV6SCR_KEY, data.scoreNameDictionary["lv6scr"]);
        PlayerPrefs.SetFloat(LV7SCR_KEY, data.scoreNameDictionary["lv7scr"]);
        PlayerPrefs.SetFloat(LV8SCR_KEY, data.scoreNameDictionary["lv8scr"]);
        PlayerPrefs.SetFloat(LV9SCR_KEY, data.scoreNameDictionary["lv9scr"]);
        PlayerPrefs.SetFloat(LV10SCR_KEY, data.scoreNameDictionary["lv10scr"]);
        PlayerPrefs.SetFloat(LV11SCR_KEY, data.scoreNameDictionary["lv11scr"]);
        PlayerPrefs.SetFloat(LV12SCR_KEY, data.scoreNameDictionary["lv12scr"]);
        PlayerPrefs.SetFloat(LV13SCR_KEY, data.scoreNameDictionary["lv13scr"]);
        PlayerPrefs.SetFloat(LV14SCR_KEY, data.scoreNameDictionary["lv14scr"]);
        PlayerPrefs.SetFloat(LV15SCR_KEY, data.scoreNameDictionary["lv15scr"]);
        PlayerPrefs.SetFloat(LV16SCR_KEY, data.scoreNameDictionary["lv16scr"]);
        PlayerPrefs.SetFloat(LV17SCR_KEY, data.scoreNameDictionary["lv17scr"]);
        PlayerPrefs.SetFloat(LV18SCR_KEY, data.scoreNameDictionary["lv18scr"]);
        PlayerPrefs.SetFloat(LV19SCR_KEY, data.scoreNameDictionary["lv19scr"]);
        PlayerPrefs.SetFloat(LV20SCR_KEY, data.scoreNameDictionary["lv20scr"]);
        PlayerPrefs.SetFloat(LV21SCR_KEY, data.scoreNameDictionary["lv21scr"]);
        PlayerPrefs.SetFloat(LV22SCR_KEY, data.scoreNameDictionary["lv22scr"]);
        PlayerPrefs.SetFloat(LV23SCR_KEY, data.scoreNameDictionary["lv23scr"]);
        PlayerPrefs.SetFloat(LV24SCR_KEY, data.scoreNameDictionary["lv24scr"]);
        PlayerPrefs.SetFloat(LV25SCR_KEY, data.scoreNameDictionary["lv25scr"]);
        PlayerPrefs.SetFloat(LV26SCR_KEY, data.scoreNameDictionary["lv26scr"]);
        PlayerPrefs.SetFloat(LV27SCR_KEY, data.scoreNameDictionary["lv27scr"]);
        PlayerPrefs.SetFloat(LV28SCR_KEY, data.scoreNameDictionary["lv28scr"]);
        PlayerPrefs.SetFloat(LV29SCR_KEY, data.scoreNameDictionary["lv29scr"]);
        PlayerPrefs.SetFloat(LV30SCR_KEY, data.scoreNameDictionary["lv30scr"]);

        /*
        PlayerPrefs.SetFloat(LV1SCR_KEY, data.lv1scr);
        PlayerPrefs.SetFloat(LV2SCR_KEY, data.lv2scr);
        PlayerPrefs.SetFloat(LV3SCR_KEY, data.lv3scr);
        PlayerPrefs.SetFloat(LV4SCR_KEY, data.lv4scr);
        PlayerPrefs.SetFloat(LV5SCR_KEY, data.lv5scr);
        PlayerPrefs.SetFloat(LV6SCR_KEY, data.lv6scr);
        PlayerPrefs.SetFloat(LV7SCR_KEY, data.lv7scr);
        PlayerPrefs.SetFloat(LV8SCR_KEY, data.lv8scr);
        PlayerPrefs.SetFloat(LV9SCR_KEY, data.lv9scr);
        PlayerPrefs.SetFloat(LV10SCR_KEY, data.lv10scr);
        PlayerPrefs.SetFloat(LV11SCR_KEY, data.lv11scr);
        PlayerPrefs.SetFloat(LV12SCR_KEY, data.lv12scr);
        PlayerPrefs.SetFloat(LV13SCR_KEY, data.lv13scr);
        PlayerPrefs.SetFloat(LV14SCR_KEY, data.lv14scr);
        PlayerPrefs.SetFloat(LV15SCR_KEY, data.lv15scr);
        PlayerPrefs.SetFloat(LV16SCR_KEY, data.lv16scr);
        PlayerPrefs.SetFloat(LV17SCR_KEY, data.lv17scr);
        PlayerPrefs.SetFloat(LV18SCR_KEY, data.lv18scr);
        PlayerPrefs.SetFloat(LV19SCR_KEY, data.lv19scr);
        PlayerPrefs.SetFloat(LV20SCR_KEY, data.lv20scr);
        PlayerPrefs.SetFloat(LV21SCR_KEY, data.lv21scr);
        PlayerPrefs.SetFloat(LV22SCR_KEY, data.lv22scr);
        PlayerPrefs.SetFloat(LV23SCR_KEY, data.lv23scr);
        PlayerPrefs.SetFloat(LV24SCR_KEY, data.lv24scr);
        PlayerPrefs.SetFloat(LV25SCR_KEY, data.lv25scr);
        PlayerPrefs.SetFloat(LV26SCR_KEY, data.lv26scr);
        PlayerPrefs.SetFloat(LV27SCR_KEY, data.lv27scr);
        PlayerPrefs.SetFloat(LV28SCR_KEY, data.lv28scr);
        PlayerPrefs.SetFloat(LV29SCR_KEY, data.lv29scr);
        PlayerPrefs.SetFloat(LV30SCR_KEY, data.lv30scr);
        */
       // PlayerPrefs.SetString(HASH_KEY, hashedData);
        PlayerPrefs.SetString(SALT_KEY, salt);

        PlayerPrefs.Save();
    }

    public GameSaveData Load()
    {
        GameSaveData data = new GameSaveData();

        int level = PlayerPrefs.GetInt(LEVEL_KEY, data.level);
        string lang = PlayerPrefs.GetString(LANG_KEY, data.lang);

        float lv1scr = PlayerPrefs.GetFloat(LV1SCR_KEY, data.lv1scr);
        float lv2scr = PlayerPrefs.GetFloat(LV2SCR_KEY, data.lv2scr);
        float lv3scr = PlayerPrefs.GetFloat(LV3SCR_KEY, data.lv3scr);
        float lv4scr = PlayerPrefs.GetFloat(LV4SCR_KEY, data.lv4scr);
        float lv5scr = PlayerPrefs.GetFloat(LV5SCR_KEY, data.lv5scr);
        float lv6scr = PlayerPrefs.GetFloat(LV6SCR_KEY, data.lv6scr);
        float lv7scr = PlayerPrefs.GetFloat(LV7SCR_KEY, data.lv7scr);
        float lv8scr = PlayerPrefs.GetFloat(LV8SCR_KEY, data.lv8scr);
        float lv9scr = PlayerPrefs.GetFloat(LV9SCR_KEY, data.lv9scr);
        float lv10scr = PlayerPrefs.GetFloat(LV10SCR_KEY, data.lv10scr);
        float lv11scr = PlayerPrefs.GetFloat(LV11SCR_KEY, data.lv11scr);
        float lv12scr = PlayerPrefs.GetFloat(LV12SCR_KEY, data.lv12scr);
        float lv13scr = PlayerPrefs.GetFloat(LV13SCR_KEY, data.lv13scr);
        float lv14scr = PlayerPrefs.GetFloat(LV14SCR_KEY, data.lv14scr);
        float lv15scr = PlayerPrefs.GetFloat(LV15SCR_KEY, data.lv15scr);
        float lv16scr = PlayerPrefs.GetFloat(LV16SCR_KEY, data.lv16scr);
        float lv17scr = PlayerPrefs.GetFloat(LV17SCR_KEY, data.lv17scr);
        float lv18scr = PlayerPrefs.GetFloat(LV18SCR_KEY, data.lv18scr);
        float lv19scr = PlayerPrefs.GetFloat(LV19SCR_KEY, data.lv19scr);
        float lv20scr = PlayerPrefs.GetFloat(LV20SCR_KEY, data.lv20scr);
        float lv21scr = PlayerPrefs.GetFloat(LV21SCR_KEY, data.lv21scr);
        float lv22scr = PlayerPrefs.GetFloat(LV22SCR_KEY, data.lv22scr);
        float lv23scr = PlayerPrefs.GetFloat(LV23SCR_KEY, data.lv23scr);
        float lv24scr = PlayerPrefs.GetFloat(LV24SCR_KEY, data.lv24scr);
        float lv25scr = PlayerPrefs.GetFloat(LV25SCR_KEY, data.lv25scr);
        float lv26scr = PlayerPrefs.GetFloat(LV26SCR_KEY, data.lv26scr);
        float lv27scr = PlayerPrefs.GetFloat(LV27SCR_KEY, data.lv27scr);
        float lv28scr = PlayerPrefs.GetFloat(LV28SCR_KEY, data.lv28scr);
        float lv29scr = PlayerPrefs.GetFloat(LV29SCR_KEY, data.lv29scr);
        float lv30scr = PlayerPrefs.GetFloat(LV30SCR_KEY, data.lv30scr);

        string savedHash = PlayerPrefs.GetString(HASH_KEY, string.Empty);
        string savedSalt = PlayerPrefs.GetString(SALT_KEY, string.Empty);


        string dataString = $"{level}:{lang}:{lv1scr}:{lv2scr}:{lv3scr}:{lv4scr}:{lv5scr}:{lv6scr}:{lv7scr}:{lv8scr}:{lv9scr}:{lv10scr}::{lv11scr}:{lv12scr}:{lv13scr}:{lv14scr}:{lv15scr}:{lv16scr}:{lv17scr}:{lv18scr}:{lv19scr}:{lv20scr}:{lv21scr}:{lv22scr}:{lv23scr}:{lv24scr}:{lv25scr}:{lv26scr}:{lv27scr}:{lv28scr}:{lv29scr}:{lv30scr}";

        string expectedHash = GenerateHash(dataString, savedSalt);

        if (savedHash == expectedHash)
        {
            data.level = level;
            data.lang = lang;
            data.lv1scr = lv1scr;
            data.lv2scr = lv2scr;
            data.lv3scr = lv3scr;
            data.lv4scr = lv4scr;
            data.lv5scr = lv5scr;
            data.lv6scr = lv6scr;
            data.lv7scr = lv7scr;
            data.lv8scr = lv8scr;
            data.lv9scr = lv9scr;
            data.lv10scr = lv10scr;
            data.lv11scr = lv11scr;
            data.lv12scr = lv12scr;
            data.lv13scr = lv13scr;
            data.lv14scr = lv14scr;
            data.lv15scr = lv15scr;
            data.lv16scr = lv16scr;
            data.lv17scr = lv17scr;
            data.lv18scr = lv18scr;
            data.lv19scr = lv19scr;
            data.lv20scr = lv20scr;
            data.lv21scr = lv21scr;
            data.lv22scr = lv22scr;
            data.lv23scr = lv23scr;
            data.lv24scr = lv24scr;
            data.lv25scr = lv25scr;
            data.lv26scr = lv26scr;
            data.lv27scr = lv27scr;
            data.lv28scr = lv28scr;
            data.lv29scr = lv29scr;
            data.lv30scr = lv30scr;
            /*
            data.scoreNameDictionary = new Dictionary<string, float>();
            data.scoreNameDictionary.Add("lv1scr", data.lv1scr);
            data.scoreNameDictionary.Add("lv2scr", data.lv2scr);
            data.scoreNameDictionary.Add("lv3scr", data.lv3scr);
            data.scoreNameDictionary.Add("lv4scr", data.lv4scr);
            data.scoreNameDictionary.Add("lv5scr", data.lv5scr);
            data.scoreNameDictionary.Add("lv6scr", data.lv6scr);
            data.scoreNameDictionary.Add("lv7scr", data.lv7scr);
            data.scoreNameDictionary.Add("lv8scr", data.lv8scr);
            data.scoreNameDictionary.Add("lv9scr", data.lv9scr);
            data.scoreNameDictionary.Add("lv10scr", data.lv10scr);
            data.scoreNameDictionary.Add("lv11scr", data.lv11scr);
            data.scoreNameDictionary.Add("lv12scr", data.lv12scr);
            data.scoreNameDictionary.Add("lv13scr", data.lv13scr);
            data.scoreNameDictionary.Add("lv14scr", data.lv14scr);
            data.scoreNameDictionary.Add("lv15scr", data.lv15scr);
            data.scoreNameDictionary.Add("lv16scr", data.lv16scr);
            data.scoreNameDictionary.Add("lv17scr", data.lv17scr);
            data.scoreNameDictionary.Add("lv18scr", data.lv18scr);
            data.scoreNameDictionary.Add("lv19scr", data.lv19scr);
            data.scoreNameDictionary.Add("lv20scr", data.lv20scr);
            data.scoreNameDictionary.Add("lv21scr", data.lv21scr);
            data.scoreNameDictionary.Add("lv22scr", data.lv22scr);
            data.scoreNameDictionary.Add("lv23scr", data.lv23scr);
            data.scoreNameDictionary.Add("lv24scr", data.lv24scr);
            data.scoreNameDictionary.Add("lv25scr", data.lv25scr);
            data.scoreNameDictionary.Add("lv26scr", data.lv26scr);
            data.scoreNameDictionary.Add("lv27scr", data.lv27scr);
            data.scoreNameDictionary.Add("lv28scr", data.lv28scr);
            data.scoreNameDictionary.Add("lv29scr", data.lv29scr);
            data.scoreNameDictionary.Add("lv30scr", data.lv30scr);*/
        }
        else
        {
            dataString = $"{data.level}:{data.lang}:{data.lv1scr}:{data.lv2scr}:{data.lv3scr}:{data.lv4scr}:{data.lv5scr}:{data.lv6scr}:{data.lv7scr}:{data.lv8scr}:{data.lv9scr}:{data.lv10scr}::{data.lv11scr}:{data.lv12scr}:{data.lv13scr}:{data.lv14scr}:{data.lv15scr}:{data.lv16scr}:{data.lv17scr}:{data.lv18scr}:{data.lv19scr}:{data.lv20scr}:{data.lv21scr}:{data.lv22scr}:{data.lv23scr}:{data.lv24scr}:{data.lv25scr}:{data.lv26scr}:{data.lv27scr}:{data.lv28scr}:{data.lv29scr}:{data.lv30scr}";

            expectedHash = GenerateHash(dataString, savedSalt);

            PlayerPrefs.SetString(HASH_KEY, expectedHash);
            PlayerPrefs.SetString(SALT_KEY, savedSalt);

            PlayerPrefs.Save();

        }

        return data;
    }

    private string GenerateHash(string data, string salt)
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