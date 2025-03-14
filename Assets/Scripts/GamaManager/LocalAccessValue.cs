using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LocalAccessValue : MonoBehaviour {

    const string currentLevel               = "CURRENT_LEVEL";
    const string totalLevelUnlock           = "TOTAL_LEVEL_UNLOCK";

    // Constant items
    public const string bumerang           = "BUMERANG";
    public const string boom               = "BOOM";
    public const string rock               = "ROCK";
    public const string shoe_item          = "SHOE";
    public const string defense_item       = "ARMOR";
    public const string health_item        = "HEALTH";
    public const string bonus_item         = "BONUS_GOLD";
    public const string time_item          = "BONUS_TIME";

    // Gold and Score
    public const string gold               = "GOLD";
    public const string total_score        = "SCORE";

    // Function access by name data
	public static void SetValue(string name,int value)
    {
        PlayerPrefs.SetInt(name, value);
    }

    public int GetValue(string name)
    {
        if (PlayerPrefs.HasKey(name))
            return PlayerPrefs.GetInt(name);
        else return -1;
    }

    // Set current level playing
    public void SetCurrentLevel(int num)
    {
        PlayerPrefs.SetInt(currentLevel, num);
    }


    // Get current level playing
    public int GetCurrentLevel()
    {
        if (PlayerPrefs.HasKey(currentLevel))
            return PlayerPrefs.GetInt(currentLevel);
        else
            return -1;
    }

    // Set total level unlock
    public void SetTotalLeLevelUnlock(int num)
    {
        PlayerPrefs.SetInt(totalLevelUnlock, num);
    }

    // Get total level unlock
    public int GetTotalLevelUnlock()
    {
        if (PlayerPrefs.HasKey(totalLevelUnlock))
            return PlayerPrefs.GetInt(totalLevelUnlock);
        else
            return -1;
    }

    // Set start level
    public void SetStarLevel(int level,int num)
    {
        PlayerPrefs.SetInt("Level " + level.ToString() , num);
    }

    // Get star level
    public int GetStar(int level)
    {
        if (PlayerPrefs.HasKey("Level " + level.ToString()))
            return PlayerPrefs.GetInt("Level "+level.ToString());
        else
            return -1;
    }

    
}
