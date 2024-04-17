using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Unity.VisualScripting;

public class Board : MonoBehaviour
{
    public GameObject card;
    public static int stage = 0;

    private void Start()
    {
        int[] Harr = { 0, 1, 2, 3, 4, 5,6,7, 8, 9, 10, 11, 12, 13,14,15 };
        int[] arr = { 0, 1, 2, 3, 4, 5, 8, 9, 10, 11, 12, 13 };

        List<int> Barr = new List<int>();

        if (stage == 0)
        {
            Barr.AddRange(arr); 
        }
        else
            Barr.AddRange(Harr);

        //orderBy -> Shuffle 
        shuffle(Barr);

        for (int i = 0; i < Barr.Count; i ++)
        {
            GameObject go = Instantiate(card);
            go.transform.parent = this.transform;

            // 카드 배치
            float x = (i % 4) * 1.4f - 2.1f;
            float y = (i / 4) * -1.4f + 1.2f;

            go.transform.position = new Vector2(x, y);
            go.GetComponent<Card>().Setting(Barr[i]);
        }

        GameManager.Instance.cardCount = Barr.Count;

    }

    // Shuffle method
    void shuffle(List<int> arr)
    {
        for (int i = 0; i < arr.Count; i++)
        {
            int temp = arr[i];
            int randomIndex = Random.Range(0, arr.Count);
            arr[i] = arr[randomIndex];
            arr[randomIndex] = temp;
        }
    }
}
  