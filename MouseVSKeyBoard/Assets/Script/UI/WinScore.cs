using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinScore : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> scoreObjectArray = null;
    public List<GameObject> GetScoreObjectArray() {  return scoreObjectArray; }

    [SerializeField]
    private List<Image> imageArray = null;
    public List <Image> GetImageArray() { return imageArray; }

    private void Start()
    {
        int count = transform.childCount;
        for (int i = 0; i < count; i++)
        {
            scoreObjectArray.Add(transform.GetChild(i).gameObject);
            Image image = scoreObjectArray[i].GetComponent<Image>();
            imageArray.Add(image);
        }
        for(int i = 0;i < imageArray.Count; i++)
        {
            imageArray[i].color = new Color32(255, 255, 255, 50);
        }
    }

    public void ViewWinScore(VictoryPlayer player)
    {
        switch (player)
        {
            case VictoryPlayer.KeyBoard:
                KeyBoardScore();
                break;
            case VictoryPlayer.Mouse:
                MouseScore();
                break;
        }
    }

    private void KeyBoardScore()
    {
        for(int i = 0; i < imageArray.Count; i++)
        {
            if(imageArray[i].color.a >= 1.0f) { continue; }
            imageArray[i].color = new Color32(255,255,255,255);
            break;
        }
    }
    private void MouseScore()
    {
        for (int i = 2; i >= 0; i--)
        {
            if (imageArray[i].color.a >= 1.0f) { continue; }
            imageArray[i].color = new Color32(255, 255, 255, 255);
            break;
        }
    }

}
