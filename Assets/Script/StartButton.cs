using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartButton : MonoBehaviour
{
    public void Start()
    {
        DataManager.Instance.LoadGameData();
    }
    public void GameStart()
    {
        SceneManager.LoadScene("MainScene");
    }
    public void GameStart_Hard()
    {
        if (DataManager.Instance.gameData.HardOn)
        {
            SceneManager.LoadScene("MainScene");
            Board.stage = 1;
        }
    }
}
