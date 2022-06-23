using System;
using System.Collections;
using System.Collections.Generic;
using ScriptTableObject;
using UnityEngine;

public class FastEnemy : Enemies
{
    protected override void Awake()
    {
        base.Awake();
        enemy = Enemy.FastTank;
    }
}
