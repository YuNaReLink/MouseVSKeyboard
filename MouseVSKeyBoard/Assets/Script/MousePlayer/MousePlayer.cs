using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class MousePlayer : MonoBehaviour
{
    [SerializeField] private Vector2 StartPosition;

    [SerializeField] private Vector2 CollisionPosition;

    [SerializeField] private float PlayerSpeed;

    private int RandomMouse;

    [SerializeField] private Vector2 PlayerPosition;

    private bool OnMove = false;

    [SerializeField] private Transform MouseLeft;
    [SerializeField] private Transform MouseRight;
    [SerializeField] private Transform MouseWheel;
    private void Start()
    {
        this.gameObject.transform.position = StartPosition;

        RandomMouse = Random.Range(0,2);

        Debug.Log(RandomMouse);
    }
    private void Update()
    {
        MovePosition();

        if (OnMove)
        {
            Move();
        }
    }

    private void MovePosition()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if(RandomMouse == 0)
            {
                OnMove = true;
            }
        }
        else if (Input.GetMouseButtonDown(1))
        {
            if (RandomMouse == 1)
            {
                OnMove= true;
            }
        }
        else if (Input.GetMouseButtonDown(2))
        {
            if (RandomMouse == 2)
            {
                OnMove = true;
            }
        }
    }
    private void Move()
    {
        transform.position = Vector2.Lerp(transform.position,CollisionPosition,10.0f * Time.deltaTime);
        //this.gameObject.transform.position = PlayerPosition;
    }


}
