using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MousePlayer : MonoBehaviour
{
    [SerializeField] private Transform StartPosition;

    [SerializeField] private Transform CollisionPosition;

    [SerializeField] private float PlayerSpeed;

    private int RandomMouse;

    [SerializeField] private Transform MouseLeft;
    [SerializeField] private Transform MouseRight;
    [SerializeField] private Transform MouseWheel;
    private void Start()
    {
        this.gameObject.transform.position = StartPosition.position;

        RandomMouse = Random.Range(0,2);
    }
    private void Update()
    {
        MovePosition();
    }

    private void MovePosition()
    {
        if (Input.GetMouseButtonDown(0) && RandomMouse == 0)
        {
            MovePosition();
        }
        else if (Input.GetMouseButtonDown(1))
        {

        }
        else if (Input.GetMouseButtonDown(2))
        {

        }
    }
}
