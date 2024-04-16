using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Board : MonoBehaviour
{
    public GameObject card;
    public static int stage = 0;

    private void Start()
    {

        int[] arr = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 };
        //orderBy -> Shuffle 
        shuffle(arr);

        for (int i = 0; i < 16; i ++)
        {
            GameObject go = Instantiate(card);
            go.transform.parent = this.transform;

            // 카드 배치
            float x = (i % 4) * 1.4f - 2.1f;
            float y = (i / 4) * 1.4f - 3.0f;

            go.transform.position = new Vector2(x, y);
            go.GetComponent<Card>().Setting(arr[i]);
        }

        GameManager.Instance.cardCount = arr.Length;

    }

    // Shuffle method
    void shuffle(int[] arr)
    {
        for (int i = 0; i < arr.Length; i++)
        {
            int temp = arr[i];
            int randomIndex = Random.Range(0, arr.Length);
            arr[i] = arr[randomIndex];
            arr[randomIndex] = temp;
        }
    }
}
  