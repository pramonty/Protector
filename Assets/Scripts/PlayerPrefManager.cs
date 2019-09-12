using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrefManager {


	public static void ClearAllMYPreferences ()
	{
		PlayerPrefs.DeleteAll();
	}

	public static void SetFirstMessageShown(bool val){
		PlayerPrefs.SetInt("FIRSTMESSAGE",(val?1:0));
	}


	public static bool GetFirstMessageShown ()
	{
		return ((PlayerPrefs.GetInt("FIRSTMESSAGE")==1)?true:false);
	}

	public static void SetSoundOff (bool val)
	{
		PlayerPrefs.SetInt("SOUNDOFF",(val?1:0));
	}

	public static bool GetSoundOff(){
		return ((PlayerPrefs.GetInt("SOUNDOFF")==1)?true:false);
	}

	public static bool GetLevelUnlock (string level)
	{
		return ((PlayerPrefs.GetInt(level.ToUpper()))==1?true:false);
	}

	public static void SetLevelUnlocked(string level,bool val){
		PlayerPrefs.SetInt(level.ToUpper(),(val?1:0));
	}

	public static void SetPlayerCoins(int val){
		PlayerPrefs.SetInt("PLAYERCOINS",val);	
	}

	public static int GetPlayerCoins(){
		return PlayerPrefs.GetInt("PLAYERCOINS");
	}

	public static string GetNextLevel ()
	{
		return PlayerPrefs.GetString("NEXT_LEVEL_TO_LOAD");
	}

	public static void SetNextLevel(string nextLevel){
		PlayerPrefs.SetString("NEXT_LEVEL_TO_LOAD",nextLevel);
	}
}
