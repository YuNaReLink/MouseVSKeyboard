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

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag != "Damage") { return; }
        other.isTrigger = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag != "Damage") { return; }
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.AddForce(collision.rigidbody.velocity);
    }
}
