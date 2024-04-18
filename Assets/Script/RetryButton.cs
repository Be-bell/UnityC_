using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class RetryButton : MonoBehaviour
{
    private string sName;

    public void Retry()
    {
        SceneManager.LoadScene("MainScene");
        GameManager.Instance.Chapter();
    }

    public void GameSceneButton(int type)
    {
        switch(type)
        {
            case 0:
                sName = "MainScene";
                break;
            case 1:
                sName = "StartScene";
                break;
        }

        SceneManager.LoadScene(sName);
        GameManager.Instance.Chapter();
    }
}
