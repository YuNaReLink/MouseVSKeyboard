using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartMagicMouse : TitleController
{
    protected override void Awake()
    {
        base.Awake();
    }
    protected override void Start()
    {
        base.Start();
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0)|| Input.GetMouseButtonDown(1)||Input.GetMouseButtonDown(2))
        {
            magicShot.MagicFire(180);
        }
    }
}
