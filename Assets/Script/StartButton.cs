using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartButton : MonoBehaviour
{
    public void GameStart()
    {
        SceneManager.LoadScene("MainScene");
    }
    public void GameStart_Hard()
    {
        SceneManager.LoadScene("MainScene");
        Board.stage = 1;
    }

}
