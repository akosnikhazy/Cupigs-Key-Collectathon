using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsManager
{

	private const string SOUND_KEY = "settings_sound";
	private const string MUSIC_KEY = "settings_music";

	private const string LANG_KEY = "settings_language";

	public void Save(SettingsData data)
	{

		PlayerPrefs.SetInt(SOUND_KEY, data.sound);
		PlayerPrefs.SetInt(MUSIC_KEY, data.music);
		PlayerPrefs.SetString(LANG_KEY, data.lang);


		PlayerPrefs.Save();
	}

	public SettingsData Load()
	{
		SettingsData data = new SettingsData();

		int sound = PlayerPrefs.GetInt(SOUND_KEY, data.sound);
		int music = PlayerPrefs.GetInt(MUSIC_KEY, data.music);

		string lang = PlayerPrefs.GetString(LANG_KEY, data.lang);

		// check if values are valid

		if (sound < 0 || sound > 100) sound = 80;
		if (music < 0 || music > 100) music = 80;




		data.sound = sound;
		data.music = music;

		data.lang = lang;


		return data;
	}

}
