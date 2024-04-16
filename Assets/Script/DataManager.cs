using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static DataManager Instance;

    private string gameDataFileName = "GameData.json";
    private GameData gameData;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else if(Instance != this)
        { 
            DontDestroyOnLoad(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    /// <summary>
    /// Load Game Data 
    /// </summary>
    public void LoadGameData()
    {
        string filePath = Application.persistentDataPath + "/" + gameDataFileName;

        if(File.Exists(filePath))
        {
            string fromJsonData = File.ReadAllText(filePath);
            gameData = JsonUtility.FromJson<GameData>(fromJsonData);

            Debug.Log("Data Load Complete");
        }
    }

    /// <summary>
    /// Save Game Data
    /// </summary>
    public void SaveGameData() 
    {   
        string toJsonData = JsonUtility.ToJson(gameData, true); // Data -> Json �������� ����  JsonUtility.ToJson("Json���� ������ ������", ������ ����)
        string filePath = Application.persistentDataPath + "/" + gameDataFileName;

        File.WriteAllText(filePath, toJsonData); // �̹� ����� ������ �ִ� ��� �����

        Debug.Log("Data Save Complete");
    }
}
