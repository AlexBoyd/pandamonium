/*
	PreviewLabs.PlayerPrefs

	Public Domain
	
	To the extent possible under law, PreviewLabs has waived all copyright and related or neighboring rights to this document. This work is published from: Belgium.
	
	http://www.previewlabs.com
	
*/
using UnityEngine;
using System;
using System.Collections;
using System.IO;
using System.Text.RegularExpressions;
using System.Collections.Generic;

public static class SaveManager
{
	private const string CURRENT_SAVE_SLOT_KEY = "CURRENT_SAVE_SLOT";	
	
	//Per save Data
	public const string CHECKPOINT_ID = "CHECKPOINT_ID";
	public const string CHECKPOINT_LEVEL_KEY = "CHECKPOINT_LEVEL";
	public const string NUGGETS_KEY_PREFIX = "NUGGETS_";
	public const string NUGGETS_COUNT_KEY_PREFIX = "NUGGETS_COUNT";
	private const string UNLOCKED_LEVEL = "UNLOCKED_LEVEL";
	private const string ANGER_UNLOCKED = "ANGER_UNLOCKED";
	private const string HAPPY_UNLOCKED = "HAPPY_UNLOCKED";
	private const string COURAGE_UNLOCKED = "COURAGE_UNLOCKED";
	
	//private static List<string> saveSlots;

	
	public static KeyValuePair<string,string> curCheckpoint;
	public static Dictionary<string,List<string>> nuggetDict = new Dictionary<string,List<string>> ();
	
	private static Hashtable playerPrefsHashtable = new Hashtable ();
	private static Hashtable currentSaveHashtable = new Hashtable ();
	
	private static bool ppHashTableChanged = false;
	private static string ppSerializedOutput = "";
	private static string ppSerializedInput = "";
	
	private static bool saveHashTableChanged = false;
	private static string serializedOutput = "";
	private static string serializedInput = "";
	
	private const string PARAMETERS_SEPERATOR = ";";
	private const string KEY_VALUE_SEPERATOR = ":";
	
	private static readonly string fileName = Application.persistentDataPath + "/PlayerPrefs.txt";
	private static string curSaveSlotFileName;
	
	private static readonly string save1file = Application.persistentDataPath + "/save1.txt";
	private static readonly string save2file = Application.persistentDataPath + "/save2.txt";
	private static readonly string save3file = Application.persistentDataPath + "/save3.txt";
	
	private static readonly string debugFile = Application.persistentDataPath + "/debug.txt";

	
	//load save meta data 
	static SaveManager ()
	{
        
		//load previous settings
		StreamReader fileReader = null;

		if (File.Exists (fileName)) {
			fileReader = new StreamReader (fileName);

			ppSerializedInput = fileReader.ReadLine ();

			ppDeserialize ();

			fileReader.Close ();
			
		}
		
		if (hasKey (CURRENT_SAVE_SLOT_KEY)) 
		{
			curSaveSlotFileName = getString (CURRENT_SAVE_SLOT_KEY);
			LoadSaveFile();
		}
		else
		{
			switchSaveSlot(0);
			LoadSaveFile();
		}
	}

	
	//Save Meta Data Methods. These are used in the main menu before I save file has been loaded.
		
	public static void switchSaveSlot (int saveSlot)
	{
		switch (saveSlot) {
		case 0:
			curSaveSlotFileName = debugFile;
			break;
		case 1:
			curSaveSlotFileName = save1file;
			break;
		case 2:
			curSaveSlotFileName = save2file;
			break;
		case 3:
			curSaveSlotFileName = save3file;
			break;
		default: 
			throw new IndexOutOfRangeException ("Attempted to access saveSlot: " + saveSlot + ". Only 0 (Debug only), 1,2,3, are valid save slot indexes"); 
		}
		ppSetString(CURRENT_SAVE_SLOT_KEY, saveSlot.ToString());
	}
	
		
	public static void LoadSaveFile()
	{
		if(curSaveSlotFileName == null)
		{
			throw new FileLoadException("No current save file set. Must be set via switchSaveSlot the first time");	
		}
		StreamReader fileReader = null;

		if (File.Exists (curSaveSlotFileName)) {
			currentSaveHashtable.Clear();
			fileReader = new StreamReader (curSaveSlotFileName);

			serializedInput = fileReader.ReadLine ();

			deserialize ();

			fileReader.Close ();
			
		}
	}
	
	public static void ClearSaveSlot()
	{
		currentSaveHashtable.Clear();
	}
	
	//Saving Methods. All saving should be done through convience methods like these, not through directly editing the save data.
	
	public static void SaveCheckpointData (Checkpoint checkpoint)
	{
		string levelName = checkpoint.transform.parent.name;
		SaveRawCheckpointData (levelName, checkpoint.name);
	}

	//needed when saving a to a checkpoint in another level, such as exiting from a cutscene. 
	public static void SaveRawCheckpointData (string levelName, string checkpoint)
	{
		setString (CHECKPOINT_LEVEL_KEY, levelName);
		setString (CHECKPOINT_ID, checkpoint);
		SaveNuggets ();
		SaveManager.save ();
		curCheckpoint = new KeyValuePair<string, string> (levelName, checkpoint);
	}
	
	public static void SaveElevatorCheckpointData (Checkpoint checkpoint)
	{
		HashSet<string> unlockedLevels = new HashSet<string> (loadList (UNLOCKED_LEVEL));
		string levelName = checkpoint.transform.parent.name;
		//add checkpoint to the list of discovered elevator checkpoints
		unlockedLevels.Add (levelName);
		saveList (UNLOCKED_LEVEL, new List<string> (unlockedLevels));
	}
	
	public static void SaveNuggets ()
	{
		foreach (KeyValuePair<string, List<string>> kvp in nuggetDict) {
			if (hasKey (NUGGETS_KEY_PREFIX + kvp.Key)) {
				HashSet<string> nuggetList = new HashSet<string>(loadList (NUGGETS_KEY_PREFIX + kvp.Key));
				foreach(string nugget in(kvp.Value))
				{
					nuggetList.Add(nugget); 	
				}
				saveList ((NUGGETS_KEY_PREFIX + kvp.Key), new List<string>(nuggetList));
			} else {
				saveList ((NUGGETS_KEY_PREFIX + kvp.Key), kvp.Value);
			}
		}
	}
	
	public static void addHappinessAbility ()
	{
		setString (HAPPY_UNLOCKED, "true");	
	}

	public static void addAngerAbility ()
	{
		setString (ANGER_UNLOCKED, "true");	
	}

	public static void addCourageAbility ()
	{
		setString (COURAGE_UNLOCKED, "true");	
	}
	
	//Loading Methods
	
	public static List<string> GetUnlockedLevels ()
	{
		return loadList (UNLOCKED_LEVEL);	
	}
	
	public static KeyValuePair<string, string> LoadCheckpointData ()
	{
		if (hasKey (CHECKPOINT_LEVEL_KEY) && hasKey (CHECKPOINT_ID)) {
			MonoBehaviour.print (1);
			MonoBehaviour.print (curSaveSlotFileName);
			return new KeyValuePair<string, string> (getString (CHECKPOINT_LEVEL_KEY), getString (CHECKPOINT_ID));
		} else {
			MonoBehaviour.print (2);
			return new KeyValuePair<string, string> ("Tutorial", "GameStartCheckpoint");
		}
	}
	

	public static bool IsHappinessUnlocked ()
	{
		string h = getString (HAPPY_UNLOCKED);
		if (h == null) {
			return false;	
		} else {
			return h == "true";	
		}
	}

	public static bool IsAngerUnlocked ()
	{
		//return true;
		string h = getString (ANGER_UNLOCKED);
		if (h == null) {
			return false;	
		} else {
			return h == "true";	
		}
	}

	public static bool IsCourageUnlocked ()
	{
		//return true;
		string h = getString (COURAGE_UNLOCKED);
		if (h == null) {
			return false;	
		} else {
			return h == "true";	
		}
	}
	
	public static int GetGoldCount ()
	{
		int count = 0;
		foreach(string key in currentSaveHashtable.Keys)
		{
			if(key.Contains(NUGGETS_KEY_PREFIX))
			{
				List<string> gold = loadList(key);
				count += gold.Count -1;
			}
		}
		return count;
	}
	
	public static List<string> LoadGold(string levelName)
	{
		return SaveManager.loadList(SaveManager.NUGGETS_KEY_PREFIX + levelName);
	}
	
	
	
	//Internal methods used for saving and loading. 
	
	private static void saveList (string key, List<string> valueList)
	{
		setString(key, String.Join (",", valueList.ToArray ()));
	}

	private static List<string> loadList (string key)
	{
		if (getString (key) == null) {
			return new List<string> ();	
		}
		return new List<string> (getString(key).Split (','));
	}
	
	private static void ppSaveList(string key, List<string> valueList)
	{
		ppSetString(key, String.Join (",", valueList.ToArray ()));
	}

	private static List<string> ppLoadList(string key)
	{
		if (ppGetString (key) == null) {
			return new List<string> ();	
		}
		return new List<string> (ppGetString(key).Split (','));
	}

	private static bool hasKey (string key)
	{
		return currentSaveHashtable.ContainsKey (key);
	}
	
	private static bool ppHasKey (string key)
	{
		return playerPrefsHashtable.ContainsKey (key);
	}

	private static void setString (string key, string value)
	{
		if (!currentSaveHashtable.ContainsKey (key)) {
			currentSaveHashtable.Add (key, value);
		} else {
			currentSaveHashtable [key] = value;
		}

		saveHashTableChanged = true;
	}

	private static string getString (string key)
	{
		if (currentSaveHashtable.ContainsKey (key)) {
			return currentSaveHashtable [key].ToString ();
		}

		return null;
	}
	
	private static void ppSetString (string key, string value)
	{
		if (!playerPrefsHashtable.ContainsKey (key)) {
			playerPrefsHashtable.Add (key, value);
		} else {
			playerPrefsHashtable [key] = value;
		}

		ppHashTableChanged = true;
	}

	private static string ppGetString (string key)
	{
		if (playerPrefsHashtable.ContainsKey (key)) {
			return playerPrefsHashtable [key].ToString ();
		}

		return null;
	}

	private static void deleteKey (string key)
	{
		playerPrefsHashtable.Remove (key);
	}

	private static void deleteAll ()
	{
		playerPrefsHashtable.Clear ();
	}

	private static void ppSave ()
	{
		if (ppHashTableChanged) {
			ppSerialize ();

			StreamWriter fileWriter = null;
			fileWriter = File.CreateText (fileName);

			if (fileWriter == null) {
				Debug.LogWarning ("PlayerPrefs::Flush() opening file for writing failed: " + fileName);
			}
	
			fileWriter.WriteLine (ppSerializedOutput);

			fileWriter.Close ();

			ppSerializedOutput = "";
		}
	}
	
	//Especially don't touch these ones. 

	private static void save ()
	{
		if (saveHashTableChanged) {
			serialize();

			StreamWriter fileWriter = null;
			fileWriter = File.CreateText (curSaveSlotFileName);

			if (fileWriter == null) {
				Debug.LogWarning ("PlayerPrefs::Flush() opening file for writing failed: " + fileName);
			}
	
			fileWriter.WriteLine (serializedOutput);

			fileWriter.Close ();

			serializedOutput = "";
		}
	}
	
	private static void serialize ()
	{
		IDictionaryEnumerator myEnumerator = currentSaveHashtable.GetEnumerator ();

		while (myEnumerator.MoveNext()) {
			if (serializedOutput != "") {
				serializedOutput += " " + PARAMETERS_SEPERATOR + " ";
			}
			serializedOutput += escapeNonSeperators (myEnumerator.Key.ToString ()) + " " + KEY_VALUE_SEPERATOR + " " + escapeNonSeperators (myEnumerator.Value.ToString ()) + " " + KEY_VALUE_SEPERATOR + " " + myEnumerator.Value.GetType ();
		}
	}
	
	private static void deserialize ()
	{
		string[] parameters = serializedInput.Split (new string[] { " " + PARAMETERS_SEPERATOR + " " }, StringSplitOptions.None);

		foreach (string parameter in parameters) {
			string[] parameterContent = parameter.Split (new string[] { " " + KEY_VALUE_SEPERATOR + " " }, StringSplitOptions.None);
			
			currentSaveHashtable.Add (deEscapeNonSeperators (parameterContent [0]), getTypeValue (parameterContent [2], deEscapeNonSeperators (parameterContent [1])));

			if (parameterContent.Length > 3) {
				Debug.LogWarning ("PlayerPrefs::Deserialize() parameterContent has " + parameterContent.Length + " elements");
			}
		}
	}

	private static void ppSerialize ()
	{
		IDictionaryEnumerator myEnumerator = playerPrefsHashtable.GetEnumerator ();

		while (myEnumerator.MoveNext()) {
			if (ppSerializedOutput != "") {
				ppSerializedOutput += " " + PARAMETERS_SEPERATOR + " ";
			}
			ppSerializedOutput += escapeNonSeperators (myEnumerator.Key.ToString ()) + " " + KEY_VALUE_SEPERATOR + " " + escapeNonSeperators (myEnumerator.Value.ToString ()) + " " + KEY_VALUE_SEPERATOR + " " + myEnumerator.Value.GetType ();
		}
	}

	private static void ppDeserialize ()
	{
		string[] parameters = ppSerializedInput.Split (new string[] { " " + PARAMETERS_SEPERATOR + " " }, StringSplitOptions.None);

		foreach (string parameter in parameters) {
			string[] parameterContent = parameter.Split (new string[] { " " + KEY_VALUE_SEPERATOR + " " }, StringSplitOptions.None);
			
			playerPrefsHashtable.Add (deEscapeNonSeperators (parameterContent [0]), getTypeValue (parameterContent [2], deEscapeNonSeperators (parameterContent [1])));

			if (parameterContent.Length > 3) {
				Debug.LogWarning ("PlayerPrefs::Deserialize() parameterContent has " + parameterContent.Length + " elements");
			}
		}
	}

	private static string escapeNonSeperators (string inputToEscape)
	{
		inputToEscape = inputToEscape.Replace (KEY_VALUE_SEPERATOR, "\\" + KEY_VALUE_SEPERATOR);
		inputToEscape = inputToEscape.Replace (PARAMETERS_SEPERATOR, "\\" + PARAMETERS_SEPERATOR);
		return inputToEscape;
	}

	private static string deEscapeNonSeperators (string inputToDeEscape)
	{
		inputToDeEscape = inputToDeEscape.Replace ("\\" + KEY_VALUE_SEPERATOR, KEY_VALUE_SEPERATOR);
		inputToDeEscape = inputToDeEscape.Replace ("\\" + PARAMETERS_SEPERATOR, PARAMETERS_SEPERATOR);
		return inputToDeEscape;
	}

	private static object getTypeValue (string typeName, string value)
	{
		if (typeName == "System.String") {
			return (object)value.ToString ();
		} else {
			Debug.LogError ("Unsupported type: " + typeName);
		}
		return null;
	}
}
