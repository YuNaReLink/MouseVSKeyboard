using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackTitle : MonoBehaviour
{
    [SerializeField]
    private NextScene title;

    [SerializeField]
    private PlaySE select;

    private int count = 0;

    private bool sceneNext = false;

    private float time = 0;
    private float timeOver = 0.3f;
    private void Awake()
    {
        title = GetComponent<NextScene>();

        select = GetComponent<PlaySE>();
    }
    private void Update()
    {
        TitleScene();
        if (sceneNext)
        {
            time += Time.deltaTime;
            if (time > timeOver)
            {
                title.GoScene();
            }
        }
    }

    private void TitleScene()
    {
        if (Input.anyKey)
        {
            if (count == 0)
            {
                count++;
                select.PlaySound();
            }
            sceneNext = true;

        }
    }
}
