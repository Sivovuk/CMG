using UnityEngine;

public class LoadGame : MonoBehaviour
{
    public GameData LoadGameData()
    {
        if (PlayerPrefs.HasKey(SaveGame.SAVE_GAME_DATA_KEY))
        {
            string json = PlayerPrefs.GetString(SaveGame.SAVE_GAME_DATA_KEY);

            GameData data = JsonUtility.FromJson<GameData>(json);
            return data;
        }
        else
            return new GameData();
    }
}