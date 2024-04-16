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
        string toJsonData = JsonUtility.ToJson(gameData, true); // Data -> Json 형식으로 변경  JsonUtility.ToJson("Json으로 변경할 데이터", 가독성 여부)
        string filePath = Application.persistentDataPath + "/" + gameDataFileName;

        File.WriteAllText(filePath, toJsonData); // 이미 저장된 파일이 있는 경우 덮어쓰기

        Debug.Log("Data Save Complete");
    }
}
