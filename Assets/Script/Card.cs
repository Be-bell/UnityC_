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
    private SpriteRenderer backImg;      // spriterenderer ������ ���� ����

    private Ease ease = Ease.Linear;

    private bool isFlipped = false; // ī�尡 ���������� �����ϱ�

    public AudioClip clip;
    public AudioSource audioSource;

    private void Start()
    {
        frontImg = GetComponent<SpriteRenderer>();
        frontImg.sprite = back.sprite;
        audioSource = GetComponent<AudioSource>();
        backImg = GetComponent<SpriteRenderer>();      // back�� �ִ� ������Ʈ�� ����
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
            StopCoroutine(FlipCard()); // ī�尡 ������ �ڷ�ƾ ���߱�

            // if Matching finished, matchCount++;
            GameManager.Instance.matchCount++;
        }

    }
    private void OnMouseDown() // ���콺�� ������ �� (*Collider �ʼ�)
    {
        if (!isFlipped)
        {
            OpenCard();
            StartCoroutine(FlipCard()); // ���������� �� �ڷ�ƾ�� �����ؼ�
        }
    }

    IEnumerator FlipCard()
    {
        //Debug.Log($"{idx} + �ڷ�ƾ ���� ��!");
        isFlipped = true; // �����̼�
        //front.SetActive(true); // �ո��� ��Ÿ����
        //back.SetActive(false); // �޸��� �������
        Rotate();
        yield return new WaitForSeconds(2.0f); // 5�ʵ��� ����ϰ�

        //front.SetActive(false); // �ո��� �������
        //back.SetActive(true); // �޸��� ��Ÿ���鼭
        GameManager.Instance.firstCard = null;
        Rotate();
        isFlipped = false; // ������ ������ ȸ��
        Debug.Log($"{idx} + �ڷ�ƾ ��!");
    }

    public void CloseCard()
    {
        StopCoroutine(FlipCard()); // ī�尡 ������ �ڷ�ƾ ���߱�
        Invoke("CloseCardInvoke", 1.0f);
        backImg.color = new Color(139.0f / 255.0f, 139.0f / 255.0f, 139.0f / 255.0f, 255.0f / 255.0f); // ȸ��

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
