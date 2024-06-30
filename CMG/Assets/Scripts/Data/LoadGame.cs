using UnityEngine;

public class LoadGame : MonoBehaviour
{
    public GameData LoadGameData()
    {
        if (PlayerPrefs.HasKey(SaveGame.SaveGameKey))
        {
            string json = PlayerPrefs.GetString(SaveGame.SaveGameKey);

            GameData data = JsonUtility.FromJson<GameData>(json);
            return data;
        }
        else
        {
            return new GameData();
        }
    }
}