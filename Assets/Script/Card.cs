using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    public int idx = 0;

    public GameObject front;
    public GameObject back;
    public Animator anim;
    public SpriteRenderer frontImg;
    public SpriteRenderer backImg;      // spriterenderer 접근을 위해 선언

    public AudioClip clip;
    public AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        backImg = back.GetComponent<SpriteRenderer>();      // back에 있는 컴포넌트만 선언
    }
    public void Setting(int number)
    {
        idx = number;
        frontImg.sprite = Resources.Load<Sprite>($"rtan{idx}");
    }
    public void DestoryCard()
    {
        Invoke("DestoryCardInvoke", 1.0f);
    }

    private void DestoryCardInvoke()
    {
        Destroy(gameObject);
    }
    public void OpenCard()
    {
        Debug.Log(idx);
        audioSource.PlayOneShot(clip);

        anim.SetBool("IsOpen", true);
        front.SetActive(true);
        back.SetActive(false);

       if(GameManager.Instance.firstCard == null)
        {
            GameManager.Instance.firstCard = this;
        }
       else
        {
            GameManager.Instance.secondCard = this;
            GameManager.Instance.Matched();
        }
        
    }

    public void CloseCard()
    {
        Invoke("CloseCardInvoke", 1.0f);
        backImg.color = new Color(139.0f / 255.0f, 139.0f / 255.0f, 139.0f / 255.0f, 255.0f / 255.0f); // 회색
    }

    private void CloseCardInvoke()
    {
        anim.SetBool("IsOpen", false);
        front.SetActive(false);
        back.SetActive(true);
    }
}
