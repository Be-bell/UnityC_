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

    private bool isFlipped = false; // 카드가 뒤집혔는지 추적하기

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

            // if Matching finished, matchCount++;
            GameManager.Instance.matchCount++;
        }

    }
        private void OnMouseDown() // 마우스로 눌렀을 때 (*Collider 필수)
    {
        if (!isFlipped)
        {
            StartCoroutine(FlipCard()); // 안접혀있을 때 코루틴을 시작해서
        }
    }

    IEnumerator FlipCard()
    {
        isFlipped = true; // 뒤집이서
        front.SetActive(true); // 앞면이 나타나고
        back.SetActive(false); // 뒷면이 사라지면

        yield return new WaitForSeconds(5.0f); // 5초동안 대기하고

        front.SetActive(false); // 앞면이 사라지고
        back.SetActive(true); // 뒷면이 나타나면서
        isFlipped = false; // 뒤집기 전으로 회귀

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
