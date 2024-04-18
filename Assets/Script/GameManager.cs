using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
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
    private float executeTime = 0.0f;
    private float waitingTime = 0.03f;

    public int cardCount = 0;

    public int stateNum = 0;

    public GameObject board;

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

        BoardState(false);
        //resultPanel.SetActive(false);
    }
    private void Start()
    {
        DataManager.Instance.LoadGameData();
        //Time.timeScale = 1.0f;
        maxText.text = DataManager.Instance.gameData.maxScore.ToString();
        audioSource = GetComponent<AudioSource>();
    }

    public void BoardState(bool state)
    {
        board.SetActive(state);

        Time.timeScale = state ? 1.0f : 0.0f;
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
            bool check = (int)time % 2 == 1;//if문을 반복하기 위한 bool값 형성 [제한 시간 경고] 

            if (check == true) //bool값이 true일때 아래 if문을 실행 [제한 시간 경고]
            {
                executeTime += Time.deltaTime; //실행 시간에 시간을 누적 [제한 시간 경고]
                if (executeTime >= waitingTime) //실행 시간이 지연 시간 값을 넘을 경우 아래 if문을 실행 [제한 시간 경고]
                {
                    timeText.fontSize += 1; // 폰트 사이즈를 +1 만큼 올린다. [제한 시간 경고]
                    executeTime = 0; //실행 시간 값을 초기화 [제한 시간 경고]
                }
            }
            else if (check == false) //bool값이 false일때 아래 if문을 실행 [제한 시간 경고]
            {
                executeTime += Time.deltaTime; //실행 시간에 시간을 누적 [제한 시간 경고]
                if (executeTime >= waitingTime) //실행 시간이 지연 시간 값을 넘을 경우 아래 if문을 실행 [제한 시간 경고]
                {
                    timeText.fontSize -= 1; // 폰트 사이즈를 -1 만큼 내린다. [제한 시간 경고]
                    executeTime = 0; //실행 시간 값을 초기화 [제한 시간 경고]
                }
            }
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
        int num;
        if(firstCard.idx == secondCard.idx +8 || firstCard.idx +8 == secondCard.idx)
        {
            
            audioSource.PlayOneShot(audioClip);

            firstCard.DestoryCard();
            secondCard.DestoryCard();
            cardCount -= 2;

            if(firstCard.idx < secondCard.idx)
            {
                num = firstCard.idx;
            }
            else
            {
                num = secondCard.idx;
            }
            switch (num)
            {
                case 0:
                    nameTxt.text = "김종화";
                    nameTxt.color = Color.black;
                    break;
                case 1:
                    nameTxt.text = "김진영";
                    nameTxt.color = Color.black;
                    break;
                case 2:
                    nameTxt.text = "김경찬";
                    nameTxt.color = Color.black;
                    break;
                case 3:
                    nameTxt.text = "최윤화";
                    nameTxt.color = Color.black;
                    break;
                case 4:
                    nameTxt.text = "곽상원";
                    nameTxt.color = Color.black;
                    break;
                case 5:
                    nameTxt.text = "서범진";
                    nameTxt.color = Color.black;
                    break;
                case 6:
                    nameTxt.text = "지우";
                    nameTxt.color = Color.black;
                    break;
                case 7:
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
        //firstCard = null;
        //secondCard = null;
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
