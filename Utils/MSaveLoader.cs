using UnityEngine;

namespace Assets.M.Scripts.Utils
{
	class MSaveLoader
	{
		public static string NAME_FIRST_TIME_PLAYING = "first_time_playing";
		public static string NUM_HEROES = "num_heroes";
		private static string _HERO_UID = "hero_uid";

		public static string LAST_SELECTED_LEVEL = "last_selected_level";
		public static string LAST_UNLOCKED_LEVEL = "last_unlocked_level";


		public static string HERO_UID(int id)
		{
			return _HERO_UID + "_" + id;
		}

		



		static public void _saveBool(string name, bool value)
		{
			PlayerPrefs.SetInt(name, value ? 1 : 0);
			PlayerPrefs.Save();
		}

		static public bool _getBool(string name, bool defaultValue = false)
		{
			return PlayerPrefs.GetInt(name, defaultValue ? 1 : 0) == 1 ? true : false;
		}

		static public void _saveInt(string name, int value)
		{
			PlayerPrefs.SetInt(name, value);
			PlayerPrefs.Save();
		}

		static public int _getInt(string name, int defaultValue = 0)
		{
			return PlayerPrefs.GetInt(name, defaultValue);
		}

		static public void _saveFloat(string name, float value)
		{
			PlayerPrefs.SetFloat(name, value);
			PlayerPrefs.Save();
		}

		static public float _getFloat(string name, float defaultValue = 0)
		{
			return PlayerPrefs.GetFloat(name, defaultValue);
		}

		static public void _saveString(string name, string value)
		{
			PlayerPrefs.SetString(name, value);
			PlayerPrefs.Save();
		}

		static public string _getString(string name, string defaultValue = "")
		{
			return PlayerPrefs.GetString(name, defaultValue);
		}


		static public void _clearSave()
		{
			PlayerPrefs.DeleteAll();
			PlayerPrefs.Save();
		}
	}
}
