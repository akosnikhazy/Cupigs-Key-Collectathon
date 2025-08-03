using UnityEngine;

public static class SteamManager
{

    static SteamManager()
    {
        try
        {
            Steamworks.SteamClient.Init(0);

        }
        catch (System.Exception e)
        {
            // lol nothing, just play
        }
    }
    // Start is called before the first frame update

    /// <summary>
    /// Returns the user's name
    /// </summary>
    /// <returns>string</returns>
    public static string GetSteamName() {
        return Steamworks.SteamClient.Name;
    }

    // thank you for this tutorial: https://www.youtube.com/watch?v=s554p28MTxY
    /// <summary>
    /// Checks if achievement by given id is unlocked
    /// </summary>
    /// <returns>bool</returns>
    public static bool IsAchievementUnlocked(string id)
    {
        var achievement = new Steamworks.Data.Achievement(id);
        return achievement.State;
    }

    /// <summary>
    /// Unlocks achievement. You can call this even if it is already unlocked
    /// </summary>
    /// <returns></returns>
    public static void UnlockAchievement(string id)
    {
        var achievement = new Steamworks.Data.Achievement(id);
        achievement.Trigger();
    }

    /// <summary>
    /// Locks achievement. If you need it.
    /// </summary>
    /// <returns></returns>
    public static void ClearAchievement(string id)
    {
        var achievement = new Steamworks.Data.Achievement(id);
        achievement.Clear();
    }


}
