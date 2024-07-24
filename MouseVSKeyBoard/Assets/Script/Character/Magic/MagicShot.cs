using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicShot : MonoBehaviour
{
    [SerializeField]
    private GameObject magicBall = null;

    [SerializeField]
    private float magicSpeed = 50f;

    [SerializeField]
    private bool fire = false;
    public bool Fire { get { return fire; } set { fire = value; } }

    [SerializeField]
    private int magicDirection = 1;

    public void MagicFire(float anglesY)
    {
        GameObject magic = Instantiate(magicBall, transform.position, Quaternion.Euler(transform.parent.eulerAngles.x, anglesY, 0));
        Rigidbody2D magicRb = magic.GetComponent<Rigidbody2D>();
        magicRb.AddForce((transform.right * magicDirection) * magicSpeed);
        Destroy(magic, 3.0f);
        fire = true;
    }
}
