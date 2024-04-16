using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Card : MonoBehaviour
{
    public int idx = 0;

    public SpriteRenderer frontImg;
    public SpriteRenderer front;
    public SpriteRenderer back;
    public Animator anim;

    public Ease ease = Ease.Linear;

    public AudioClip clip;
    public AudioSource audioSource;

    private void Start()
    {
        frontImg.sprite = back.sprite;        
        frontImg = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
    }
    public void Setting(int number)
    {
        idx = number;
        front.sprite = Resources.Load<Sprite>($"rtan{idx}");
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
        Rotate();
        audioSource.PlayOneShot(clip);

        anim.SetBool("IsOpen", true);
        //front.SetActive(true);
        //back.SetActive(false);

        if (GameManager.Instance.firstCard == null)
        {
            GameManager.Instance.firstCard = this;


        }
        else
        {
            GameManager.Instance.secondCard = this;
            GameManager.Instance.Matched();

            // if Matching finished, matchCount++;
            GameManager.Instance.matchCount++;
        }

    }
    public void CloseCard()
    {
        Invoke("CloseCardInvoke", 1.0f);
    }

    private void CloseCardInvoke()
    {
        Rotate();
        anim.SetBool("IsOpen", false);
        //front.SetActive(false);
        //back.SetActive(true);
    }

    public void Rotate()
    {        
        var seq = DOTween.Sequence();
        seq.Append(this.transform.DORotate(this.transform.eulerAngles + new Vector3(0, 90, 0), 0.25f)).SetEase(ease);
        seq.AppendCallback(() => { frontImg.sprite = (this.transform.eulerAngles.y < 180) ? front.sprite : back.sprite;
            frontImg.flipX = (this.transform.eulerAngles.y < 180) ? true: false; });
        seq.Append(this.transform.DORotate(this.transform.eulerAngles + new Vector3(0, 180, 0), 0.25f)).SetEase(ease);
    }
}
