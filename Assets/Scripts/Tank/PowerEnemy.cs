using System.Collections;
using System.Collections.Generic;
using ScriptTableObject;
using UnityEngine;

public class PowerEnemy : Enemies
{
    protected override void Awake()
    {
        base.Awake();
        enemy = Enemy.PowerTank;
    }
}
