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
    public GameObject endText;

    private float time = 60.0f; // ���� �ð��� 60�ʷ� �����Ѵ�. [�����Ҷ����� �ð� ����]
    private float maxTime = 0.0f;

    public int cardCount = 0;

    public int stateNum = 0;

    public AudioClip audioClip;
    private AudioSource audioSource;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }
    private void Start()
    {
        DataManager.Instance.LoadGameData();

        Time.timeScale = 1.0f;
        audioSource = GetComponent<AudioSource>();
        


        maxText.text = maxTime.ToString();
    }
    private void Update()
    {
        time -= Time.deltaTime; //���ѽð��� ���� �����Ѵ� [�����Ҷ����� �ð� ����]
        timeText.text = time.ToString("N2");
        
        if (time <= 10.0f) //���ѽð��� 10�� ������ �� [Ÿ�̸� �ð� ��� ���]
        {
            timeText.color = new Color32(255, 0, 0, 255); // ���� �ð��� ���������� ���� [Ÿ�̸� �ð� ��� ���]

            if (time <= 0.0f) //���ѽð��� 0�ʰ� �Ǿ��� �� [�����Ҷ����� �ð� ����]
            {
                time = 0.0f; //���ѽð��� 0�ʷ� ���� [�����Ҷ����� �ð� ����]

                endText.SetActive(true);
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
                endText.SetActive(true);
            }
        }
        else
        {
            firstCard.CloseCard();
            secondCard.CloseCard();
            time -= 2.0f;//ī�� ��Ī�� Ʋ���� �� ���ѽð� 2�� �����Ѵ� [�����Ҷ����� �ð� ����]
        }
        firstCard = null;
        secondCard = null;
    }

    public void AddStateNum()
    {
        stateNum++;
    }



    /// <summary>
    /// Game Quit - Save Data
    /// </summary>
    private void OnApplicationQuit()
    {
        DataManager.Instance.SaveGameData();
    }
}
