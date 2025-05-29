using System.IO;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public static SaveManager Instance;
    private string savePath;
    public PlayerSaveData saveData;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            savePath = Application.persistentDataPath + "/saveData.json";
            Load();
            //ResetData();
            if (saveData.playerData.currentLevel>30) saveData.playerData.currentLevel = 1;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void Save()
    {
        string json = JsonUtility.ToJson(saveData, true);
        File.WriteAllText(savePath, json);
    }

    public void Load()
    {
        if (File.Exists(savePath))
        {
            string json = File.ReadAllText(savePath);
            saveData = JsonUtility.FromJson<PlayerSaveData>(json);
            //File.Delete(savePath); // Yüklədikdən sonra faylı silirik
        }
        else
        {
            Debug.Log("Save file not found. Creating a new one.");
            saveData = new PlayerSaveData(); // Default dəyərlərlə
            saveData.playerData.health = 4;
            saveData.playerData.coins = 0;
            saveData.playerData.currentLevel = 1;
            Save(); // İlk dəfə yaradıb saxlayırıq
        }
    }

    public void ResetData()
    {
        File.Delete(savePath);
        Load();
    }
}
