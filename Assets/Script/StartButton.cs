using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartButton : MonoBehaviour
{
    //public void Start()
    //{
    //    DataManager.Instance.LoadGameData();
    //}
    public void GameStart()
    {
        SceneManager.LoadScene("MainScene");
    }
    public void GameStart_Hard()
    {
        //if (DataManager.Instance.gameData.HardOn)
        //{
        //    SceneManager.LoadScene("MainScene");
        //    Board.stage = 1;
        //}

        if (DataManager.Instance.gameData.StageInfor[0].HardOn)
        {
            SceneManager.LoadScene("MainScene");
            //Board.stage = 1;
            //GameManager.Instance.StageNum = 1;
            GameManager.stageNumber = 1;
        }
    }
}
