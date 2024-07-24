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
    public MagicShot GetMagicShot() { return magicShot; }
    protected virtual void Awake()
    {
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        inputController = new InputController();
        magicShot = GetComponentInChildren<MagicShot>();
    }
    protected virtual void Start()
    {

        basePosition = transform.position;
        victryPosition = new Vector2(0, basePosition.y);
    }
}
