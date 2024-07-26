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
    protected Vector3 basePosition = Vector2.zero;
    public Vector3 GetBasePosition() { return basePosition; }
    [SerializeField]
    protected Vector3 baseRotate = Vector2.zero;
    public Vector3 GetBaseRotate() { return baseRotate; }

    [SerializeField]
    private Rigidbody2D rigidbody2D = null;

    [SerializeField]
    private float impactX = 500f;
    [SerializeField]
    private float impactY = 1000f;

    [SerializeField]
    protected MagicShot magicShot = null;
    public MagicShot GetMagicShot() { return magicShot; }

    [SerializeField]
    protected Transform magicCircle = null;

    [SerializeField]
    protected SpriteRenderer spriteRenderer = null;
    public SpriteRenderer GetSpriteRenderer() { return spriteRenderer; }
    [SerializeField]
    protected List<Sprite> sprites = new List<Sprite>();
    public List<Sprite> GetSprites() { return sprites; }
    [SerializeField]
    protected bool loseFlag = false;

    [SerializeField]
    protected GameManager.GameMode gameMode = GameManager.GameMode.Null;
    protected virtual void Awake()
    {
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        inputController = new InputController();
        magicShot = GetComponentInChildren<MagicShot>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        basePosition = transform.position;
        baseRotate = transform.rotation.eulerAngles;

        rigidbody2D = GetComponent<Rigidbody2D>();
    }
    protected virtual void Start()
    {

        
    }

    public void InitializePosition()
    {
        magicShot.Fire = false;
        spriteRenderer.sprite = sprites[0];
        transform.position = basePosition;
        transform.rotation = Quaternion.Euler(baseRotate);
        rigidbody2D.velocity = Vector2.zero;
        magicCircle.transform.localScale = Vector3.zero;
        loseFlag = false;
    }

    public void MagicCircleMakeItBigger(int _count)
    {
        Vector3 scale = magicCircle.localScale;
        float rangeDifference = 0.5f - 0;
        float add = rangeDifference / (gameController.GetMaxCount() - 1);
        scale.x = 0 + (_count * add);
        rangeDifference = 1f - 0;
        add = rangeDifference / (gameController.GetMaxCount() - 1);
        scale.y = 0 + (_count * add);
        scale.z = 0 + (_count * add);
        magicCircle.localScale = scale;
    }

    protected void ChangeAnimation()
    {
        if (loseFlag)
        {
            if (sprites[2] == null) { return; }
            spriteRenderer.sprite = sprites[2];
        }
        else if (magicShot.Fire)
        {
            if (sprites[1] == null) { return; }
            spriteRenderer.sprite = sprites[1];
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag != "Damage") { return; }
        Rigidbody2D rb2D = collision.GetComponent<Rigidbody2D>();
        rigidbody2D.AddForce(new Vector2(rb2D.velocity.x * impactX,impactY));
        loseFlag = true;
        gameController.GetGameSE().Damage();
        HitStopManager.instance.StartHitStop(0.5f);
    }
}
