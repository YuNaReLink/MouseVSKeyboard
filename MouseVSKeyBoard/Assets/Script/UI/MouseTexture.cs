using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MouseTexture : MonoBehaviour
{
    [SerializeField]
    private Image mouseImage = null;

    [SerializeField]
    private List<Sprite> spriteArray = new List<Sprite>();

    [SerializeField]
    private List<Sprite> inputSpriteArray = new List<Sprite>();

    public void Initialize()
    {
        mouseImage = GetComponent<Image>();
    }

    public void ChangeTexture(int _num)
    {
        RectTransform rectTransform = GetComponent<RectTransform>();
        rectTransform.sizeDelta = new Vector2(500, 500);
        mouseImage.sprite = spriteArray[_num];
    }

    public void PushChangeTexture(int _num)
    {
        RectTransform rectTransform = GetComponent<RectTransform>();
        rectTransform.sizeDelta = new Vector2(450, 450);
        mouseImage.sprite = inputSpriteArray[_num];
    }

}
