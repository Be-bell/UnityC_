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

    private float time = 60.0f; // ���� �ð��� 60�ʷ� �����Ѵ�. [�����Ҷ����� �ð� ����]
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
        time -= Time.deltaTime; //���ѽð��� ���� �����Ѵ� [�����Ҷ����� �ð� ����]
        timeText.text = time.ToString("N2");

        // matchCount put in matchTxt
        matchTxt.text = matchCount.ToString();
        
        if (time <= 10.0f) //���ѽð��� 10�� ������ �� [Ÿ�̸� �ð� ��� ���]
        {
            timeText.color = new Color32(255, 0, 0, 255); // ���� �ð��� ���������� ���� [Ÿ�̸� �ð� ��� ���]
            bool check = (int)time % 2 == 1;//if���� �ݺ��ϱ� ���� bool�� ���� [���� �ð� ���] 

            if (check == true) //bool���� true�϶� �Ʒ� if���� ���� [���� �ð� ���]
            {
                executeTime += Time.deltaTime; //���� �ð��� �ð��� ���� [���� �ð� ���]
                if (executeTime >= waitingTime) //���� �ð��� ���� �ð� ���� ���� ��� �Ʒ� if���� ���� [���� �ð� ���]
                {
                    timeText.fontSize += 1; // ��Ʈ ����� +1 ��ŭ �ø���. [���� �ð� ���]
                    executeTime = 0; //���� �ð� ���� �ʱ�ȭ [���� �ð� ���]
                }
            }
            else if (check == false) //bool���� false�϶� �Ʒ� if���� ���� [���� �ð� ���]
            {
                executeTime += Time.deltaTime; //���� �ð��� �ð��� ���� [���� �ð� ���]
                if (executeTime >= waitingTime) //���� �ð��� ���� �ð� ���� ���� ��� �Ʒ� if���� ���� [���� �ð� ���]
                {
                    timeText.fontSize -= 1; // ��Ʈ ����� -1 ��ŭ ������. [���� �ð� ���]
                    executeTime = 0; //���� �ð� ���� �ʱ�ȭ [���� �ð� ���]
                }
            }
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
                    nameTxt.text = "����ȭ";
                    nameTxt.color = Color.black;
                    break;
                case 1:
                    nameTxt.text = "������";
                    nameTxt.color = Color.black;
                    break;
                case 2:
                    nameTxt.text = "�����";
                    nameTxt.color = Color.black;
                    break;
                case 3:
                    nameTxt.text = "����ȭ";
                    nameTxt.color = Color.black;
                    break;
                case 4:
                    nameTxt.text = "�����";
                    nameTxt.color = Color.black;
                    break;
                case 5:
                    nameTxt.text = "������";
                    nameTxt.color = Color.black;
                    break;
                case 6:
                    nameTxt.text = "����";
                    nameTxt.color = Color.black;
                    break;
                case 7:
                    nameTxt.text = "��";
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
            nameTxt.text = "����";
            nameTxt.color = Color.red;
            time -= 2.0f;//ī�� ��Ī�� Ʋ���� �� ���ѽð� 2�� �����Ѵ� [�����Ҷ����� �ð� ����]
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
