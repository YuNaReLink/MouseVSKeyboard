using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    [SerializeField]
    protected GameController gameController = null;
    protected InputController inputController = null;
    public InputController GetInputController() { return inputController; }

    [SerializeField]
    protected Vector2 basePosition = Vector2.zero;
    [SerializeField]
    protected Vector2 victryPosition = Vector2.zero;
    [SerializeField]
    protected bool victoryFlag = false;
    public bool IsVictory() { return victoryFlag; }

    [SerializeField]
    protected MagicShot magicShot = null;
    protected virtual void Start()
    {
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        inputController = new InputController();

        basePosition = transform.position;
        victryPosition = new Vector2(0, basePosition.y);

        magicShot = GetComponentInChildren<MagicShot>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
