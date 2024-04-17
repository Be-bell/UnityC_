using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public Card firstCard;
    public Card secondCard;

    public Text timeText;
    public Text maxText;
    public Text resultText;
    //public GameObject endText;

    public GameObject resultPanel;

    // matchTxt caching
    public Text matchTxt;

    private float time = 60.0f; // 제한 시간을 60초로 설정한다. [실패할때마다 시간 감소]
    private float maxTime = 0.0f;

    public int cardCount = 0;

    public int stateNum = 0;

    // count attemped to match card
    public int matchCount = 0;
    private float currentCount = 0.0f;

    public AudioClip audioClip;
    private AudioSource audioSource;

    public Text nameTxt;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;

        //resultPanel.SetActive(false);
    }
    private void Start()
    {
        DataManager.Instance.LoadGameData();
        Time.timeScale = 1.0f;
        audioSource = GetComponent<AudioSource>();
    }
    private void Update()
    {
        time -= Time.deltaTime; //제한시간이 점점 감소한다 [실패할때마다 시간 감소]
        timeText.text = time.ToString("N2");

        // matchCount put in matchTxt
        matchTxt.text = matchCount.ToString();
        
        if (time <= 10.0f) //제한시간이 10초 남았을 시 [타이머 시간 경고 기능]
        {
            timeText.color = new Color32(255, 0, 0, 255); // 제한 시간을 빨간색으로 강조 [타이머 시간 경고 기능]

            if (time <= 0.0f) //제한시간이 0초가 되었을 시 [실패할때마다 시간 감소]
            {
                time = 0.0f; //제한시간을 0초로 고정 [실패할때마다 시간 감소]

                //endText.SetActive(true);
                GetCurrentScore();
                OnResultPanel();
                Time.timeScale = 0.0f;
            }
        }
        
    }

    public void Matched()
    {
        if(firstCard.idx == secondCard.idx +8 || firstCard.idx +8 == secondCard.idx)
        {
            
            audioSource.PlayOneShot(audioClip);

            firstCard.DestoryCard();
            secondCard.DestoryCard();
            cardCount -= 2;

            switch (firstCard.idx)
            {
                case 0:
                case 8:
                    nameTxt.text = "김종화";
                    nameTxt.color = Color.black;
                    break;
                case 1:
                case 9:
                    nameTxt.text = "김진영";
                    nameTxt.color = Color.black;
                    break;
                case 2:
                case 10:
                    nameTxt.text = "김경찬";
                    nameTxt.color = Color.black;
                    break;
                case 3:
                case 11:
                    nameTxt.text = "최윤화";
                    nameTxt.color = Color.black;
                    break;
                case 4:
                case 12:
                    nameTxt.text = "곽상원";
                    nameTxt.color = Color.black;
                    break;
                case 5:
                case 13:
                    nameTxt.text = "서범진";
                    nameTxt.color = Color.black;
                    break;
                case 6:
                case 14:
                    nameTxt.text = "지우";
                    nameTxt.color = Color.black;
                    break;
                case 7:
                case 15:
                    nameTxt.text = "웅";
                    nameTxt.color = Color.black;
                    break;
            }


            if (cardCount == 0)
            {
                Time.timeScale = 0.0f;
                GetCurrentScore();
                OnResultPanel();
                Chapter();
                //endText.SetActive(true);
            }
        }
        else
        {
            firstCard.CloseCard();
            secondCard.CloseCard();
            nameTxt.text = "실패";
            nameTxt.color = Color.red;
            time -= 2.0f;//카드 매칭이 틀렸을 시 제한시간 2초 차감한다 [실패할때마다 시간 감소]
        }
        firstCard = null;
        secondCard = null;
    }

    public void AddStateNum()
    {
        stateNum++;
    }
    private void GetCurrentScore()
    {
        currentCount = matchCount;
    }

    private void OnResultPanel()
    {
        resultPanel.SetActive(true);
        resultText.text = currentCount.ToString();
    }
    private void OnApplicationQuit()
    {
        //Chapter();
        //DataManager.Instance.SaveGameData();
    }

    public void Chapter()
    {
        var maxScore = DataManager.Instance.gameData.maxScore;

        DataManager.Instance.gameData.stageLevel = stateNum; 

        if(maxScore < currentCount)
        {
            maxTime = currentCount;
        }

        if(time >0)
            DataManager.Instance.gameData.HardOn = true;
        

        DataManager.Instance.gameData.maxScore = maxTime;
        DataManager.Instance.SaveGameData();
    }
}
