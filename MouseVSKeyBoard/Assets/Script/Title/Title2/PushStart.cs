using UnityEngine;
using UnityEngine.UI;

public class PushStart : MonoBehaviour
{
    private NextScene nextScene;

    [SerializeField]
    private Text startText = null;

    [SerializeField]
    private PlaySE SE;

    [SerializeField]
    private float overTime = 3;

    private float ResetTime = 0.8f;

    private float currentTime = 0;

    private bool active = false;

    private bool activeTimer = false;

    private float sceneTime = 0;

    private float nextSceneTime = 0.3f;

    private bool nextSceneGo = false;

    private int soundCount = 0;

    private void Awake()
    {
        nextScene = GetComponent<NextScene>();

        startText = GetComponent<Text>();

        SE = GetComponent<PlaySE>();

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
        if (Input.GetKey(KeyCode.Tab) || Input.GetKeyDown(KeyCode.Tab) || Input.GetKeyUp(KeyCode.Tab)) 
        {
            nextScene.SceneChang();         
        }
        
        if (Input.anyKey)
        {
            if (soundCount == 0)
            {
                SE.PlaySound();
                soundCount++;
            }
            nextSceneGo = true;
        }
        if (nextSceneGo)
        {
            sceneTime += Time.deltaTime;
            if(nextSceneTime < sceneTime)
            {
                nextScene.GoScene();
            }
        }

    }

}
