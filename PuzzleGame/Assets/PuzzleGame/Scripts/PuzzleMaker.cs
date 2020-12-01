using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PuzzleMaker : MonoBehaviour
{
    public GameObject start;

    public void SetPuzzleImage(Image image)
    {
        for (int i = 1; i < 10; i++)
        {
            GameObject.Find(i.ToString("0")).transform.Find("Puzzle").GetComponent<SpriteRenderer>().sprite = image.sprite;
        }
        start.SetActive(false);
    }
}
