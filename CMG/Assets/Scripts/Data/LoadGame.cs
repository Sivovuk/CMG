using UnityEngine;

public class LoadGame : MonoBehaviour
{
    public SaveGameData LoadGameData()
    {
        if (PlayerPrefs.HasKey(SaveGame.SaveGameKey))
        {
            string json = PlayerPrefs.GetString(SaveGame.SaveGameKey);

            SaveGameData data = JsonUtility.FromJson<SaveGameData>(json);
            return data;
        }
        else
        {
            return new SaveGameData();
        }
    }
}