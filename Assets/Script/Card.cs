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
    public SpriteRenderer backImg;      // spriterenderer ������ ���� ����

    private bool isFlipped = false; // ī�尡 ���������� �����ϱ�

    public AudioClip clip;
    public AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        backImg = back.GetComponent<SpriteRenderer>();      // back�� �ִ� ������Ʈ�� ����
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
        private void OnMouseDown() // ���콺�� ������ �� (*Collider �ʼ�)
    {
        if (!isFlipped)
        {
            StartCoroutine(FlipCard()); // ���������� �� �ڷ�ƾ�� �����ؼ�
        }
    }

    IEnumerator FlipCard()
    {
        isFlipped = true; // �����̼�
        front.SetActive(true); // �ո��� ��Ÿ����
        back.SetActive(false); // �޸��� �������

        yield return new WaitForSeconds(5.0f); // 5�ʵ��� ����ϰ�

        front.SetActive(false); // �ո��� �������
        back.SetActive(true); // �޸��� ��Ÿ���鼭
        isFlipped = false; // ������ ������ ȸ��

    }

    public void CloseCard()
    {
        Invoke("CloseCardInvoke", 1.0f);
        backImg.color = new Color(139.0f / 255.0f, 139.0f / 255.0f, 139.0f / 255.0f, 255.0f / 255.0f); // ȸ��
    }

    private void CloseCardInvoke()
    {
        anim.SetBool("IsOpen", false);
        front.SetActive(false);
        back.SetActive(true);
    }        
}
