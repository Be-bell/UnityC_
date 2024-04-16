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

    private float time = 60.0f; // ���� �ð��� 60�ʷ� �����Ѵ�. [�����Ҷ����� �ð� ����]
    private float maxTime = 0.0f;

    public int cardCount = 0;

    public int stateNum = 0;

    // count attemped to match card
    public int matchCount = 0;
    private float currentCount = 0.0f;

    public AudioClip audioClip;
    private AudioSource audioSource;

    public GameObject failTxt;

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
        time -= Time.deltaTime; //���ѽð��� ���� �����Ѵ� [�����Ҷ����� �ð� ����]
        timeText.text = time.ToString("N2");

        // matchCount put in matchTxt
        matchTxt.text = matchCount.ToString();
        
        if (time <= 10.0f) //���ѽð��� 10�� ������ �� [Ÿ�̸� �ð� ��� ���]
        {
            timeText.color = new Color32(255, 0, 0, 255); // ���� �ð��� ���������� ���� [Ÿ�̸� �ð� ��� ���]

            if (time <= 0.0f) //���ѽð��� 0�ʰ� �Ǿ��� �� [�����Ҷ����� �ð� ����]
            {
                time = 0.0f; //���ѽð��� 0�ʷ� ���� [�����Ҷ����� �ð� ����]

                //endText.SetActive(true);
                GetCurrentScore();
                OnResultPanel();
                Time.timeScale = 0.0f;
            }
        }
        
    }

    public void Matched()
    {
        if(firstCard.idx == secondCard.idx)
        {
            
            audioSource.PlayOneShot(audioClip);

            firstCard.DestoryCard();
            secondCard.DestoryCard();
            cardCount -= 2;

            if(cardCount == 0)
            {
                Time.timeScale = 0.0f;
                GetCurrentScore();
                OnResultPanel();
                //endText.SetActive(true);
            }
        }
        else
        {
            firstCard.CloseCard();
            secondCard.CloseCard();
            failTxt.SetActive(true);
            time -= 2.0f;//ī�� ��Ī�� Ʋ���� �� ���ѽð� 2�� �����Ѵ� [�����Ҷ����� �ð� ����]
        }
        firstCard = null;
        secondCard = null;
    }

    public void AddStateNum()
    {
        stateNum++;
    }
    public void ButtonContenue()
    {
        failTxt.SetActive(false);
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
        Chapter();
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

        DataManager.Instance.gameData.maxScore = maxTime;
        DataManager.Instance.SaveGameData();
    }
}
