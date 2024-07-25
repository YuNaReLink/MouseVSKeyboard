using UnityEngine;
using UnityEngine.UI;

public class PushStart : MonoBehaviour
{
    private NextScene nextScene;

    [SerializeField]
    private Text startText = null;

    [SerializeField]
    private float overTime = 3;

    private float ResetTime = 0.8f;

    private float currentTime = 0;

    private bool active = false;

    private TitleAlpha titleAlpha;

    private bool activeTimer = false;

    [SerializeField]
    private bool Stop = false;

    private void Awake()
    {
        nextScene = GetComponent<NextScene>();

        startText = GetComponent<Text>();

        titleAlpha = GetComponent<TitleAlpha>();

        startText.enabled = active;
    }
    private void Update()
    {
        TitleTimerStop();
        ActiveTimer();
        if (activeTimer)
        {
             PushToStart();
            overTime = ResetTime;
        }
    }
    private void ActiveTimer( )   {
        if (!Stop)
        {
            currentTime += Time.deltaTime;
            startText.enabled = active;
            if (overTime < currentTime)
            {
                active = !active;
                currentTime = 0;
                activeTimer = true;
            }
        }
        
    }
    private void PushToStart()
    {
        if(Input.GetKeyUp(KeyCode.Escape)|| Input.GetKeyDown(KeyCode.Escape)|| Input.GetKey(KeyCode.Escape))
        {
        }
        else if (Input.anyKey && !Stop)
        {
            nextScene.GoScene();
        }
    }
    public void TitleTimerStop()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Stop = !Stop;
            if (Stop)
            {
                startText.enabled = false;
            }
            else if (!Stop)
            {
                startText.enabled = true;
            }
        }
    }
}
