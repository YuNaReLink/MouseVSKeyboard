using UnityEngine;
using UnityEngine.UI;

public class TitleAlpha : MonoBehaviour
{
    [SerializeField]
    private float increaseAlpha = 0.003f;
    

    [SerializeField]
    private float alpha = 0;

    private float maxAlpha = 0.01f;
    
    [SerializeField]
    private float maxColor = 1;


    private void Start()
    {
        this.GetComponent<SpriteRenderer>().color += new Color(maxColor, maxColor, maxColor, alpha);
    }

    private void Update()
    {
        if (alpha < maxAlpha)
        {
            alpha += increaseAlpha * Time.deltaTime;
            this.GetComponent<SpriteRenderer>().color += new Color(maxColor, maxColor, maxColor, alpha);
        }
    }
}
