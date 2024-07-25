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

    private bool activeTimer = false;

    private void Awake()
    {
        nextScene = GetComponent<NextScene>();

        startText = GetComponent<Text>();

        startText.enabled = active;
    }
    private void Update()
    {
        ActiveTimer();
        if (activeTimer)
        {
             PushToStart();
            overTime = ResetTime;
        }
    }
    private void ActiveTimer( )   {
        currentTime += Time.deltaTime;
        startText.enabled = active;
        if (overTime < currentTime)
        {
            active = !active;   
            currentTime = 0;
            activeTimer = true;
        }
        
        
    }
    private void PushToStart()
    {
        if (Input.anyKey)
        {
            nextScene.GoScene();
        }
    }

}
