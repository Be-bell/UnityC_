using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public Card firstCard;
    public Card secondCard;

    public Text timeText;
    public GameObject endText;

    private float time = 60.0f;
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
        Time.timeScale = 1.0f;
        audioSource = GetComponent<AudioSource>();
    }
    private void Update()
    {
        time -= Time.deltaTime;
        timeText.text = time.ToString("N2");

        if(time < 0.0f)
        {
            time = 0.0f;

            endText.SetActive(true);
            Time.timeScale = 0.0f;
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
            time -= 2.0f;
        }
        firstCard = null;
        secondCard = null;
    }

    public void AddStateNum()
    {
        stateNum++;
    }
}
