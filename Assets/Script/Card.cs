using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Card : MonoBehaviour
{
    public int idx = 0;

    public SpriteRenderer front;
    public SpriteRenderer back;
    public Animator anim;

    public SpriteRenderer frontImg;
    private SpriteRenderer backImg;      // spriterenderer 접근을 위해 선언

    private Ease ease = Ease.Linear;

    private bool isFlipped = false; // 카드가 뒤집혔는지 추적하기

    public AudioClip clip;
    public AudioSource audioSource;

    private void Start()
    {
        frontImg = GetComponent<SpriteRenderer>();
        frontImg.sprite = back.sprite;
        audioSource = GetComponent<AudioSource>();
        backImg = GetComponent<SpriteRenderer>();      // back에 있는 컴포넌트만 선언
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
        //Rotate();
        audioSource.PlayOneShot(clip);

        anim.SetBool("IsOpen", true);
        //front.SetActive(true);
        //back.SetActive(false);

        if(GameManager.Instance.firstCard == null)
        {
            GameManager.Instance.firstCard = this;
        }
       else
        {
            GameManager.Instance.secondCard = this;
            GameManager.Instance.Matched();
            StopCoroutine(FlipCard()); // 카드가 닫히면 코루틴 멈추기

            // if Matching finished, matchCount++;
            GameManager.Instance.matchCount++;
        }

    }
    private void OnMouseDown() // 마우스로 눌렀을 때 (*Collider 필수)
    {
        if (!isFlipped)
        {
            OpenCard();
            StartCoroutine(FlipCard()); // 안접혀있을 때 코루틴을 시작해서
        }
    }

    IEnumerator FlipCard()
    {
        //Debug.Log($"{idx} + 코루틴 도는 중!");
        isFlipped = true; // 뒤집이서
        //front.SetActive(true); // 앞면이 나타나고
        //back.SetActive(false); // 뒷면이 사라지면
        Rotate();
        yield return new WaitForSeconds(2.0f); // 5초동안 대기하고

        //front.SetActive(false); // 앞면이 사라지고
        //back.SetActive(true); // 뒷면이 나타나면서
        GameManager.Instance.firstCard = null;
        Rotate();
        isFlipped = false; // 뒤집기 전으로 회귀
        Debug.Log($"{idx} + 코루틴 끝!");
    }

    public void CloseCard()
    {
        StopCoroutine(FlipCard()); // 카드가 닫히면 코루틴 멈추기
        Invoke("CloseCardInvoke", 1.0f);
        backImg.color = new Color(139.0f / 255.0f, 139.0f / 255.0f, 139.0f / 255.0f, 255.0f / 255.0f); // 회색

    }

    private void CloseCardInvoke()
    {        
        anim.SetBool("IsOpen", false);
        //front.SetActive(false);
        //back.SetActive(true);
    }

    public void Rotate()
    {
        var seq = DOTween.Sequence();
        seq.Append(this.transform.DORotate(this.transform.eulerAngles + new Vector3(0, 90, 0), 0.25f)).SetEase(ease);
        seq.AppendCallback(() => {
            frontImg.sprite = (this.transform.eulerAngles.y < 180) ? front.sprite : back.sprite;
            frontImg.flipX = (this.transform.eulerAngles.y < 180) ? true : false;
        });
        seq.Append(this.transform.DORotate(this.transform.eulerAngles + new Vector3(0, 180, 0), 0.25f)).SetEase(ease);
    }

}
