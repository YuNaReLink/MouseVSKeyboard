using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyBoardTexture : MonoBehaviour
{
    [SerializeField]
    private Image keyBoardImage = null;

    [SerializeField]
    private List<Sprite> spriteArray = new List<Sprite>();

    [SerializeField]
    private List<Sprite> inputSpriteArray = new List<Sprite>();

    public void Initialize()
    {
        keyBoardImage = GetComponent<Image>();
    }

    public void ChangeTexture(int _num)
    {
        RectTransform rectTransform = GetComponent<RectTransform>();
        rectTransform.sizeDelta = new Vector2(500, 500);
        keyBoardImage.sprite = spriteArray[_num];
    }

    public void PushChangeTexture(KeyCode _key)
    {
        int num = 0;
        switch (_key)
        {
            case KeyCode.A:
                num = 0;
                break;
            case KeyCode.S:
                num = 1;
                break;
            case KeyCode.D:
                num = 2;
                break;
        }
        RectTransform rectTransform = GetComponent<RectTransform>();
        rectTransform.sizeDelta = new Vector2(450, 450);
        keyBoardImage.sprite = inputSpriteArray[num];
    }
}
