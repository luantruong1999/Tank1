using System.Collections;
using System.Collections.Generic;
using ScriptTableObject;
using UnityEngine;

public class ArmorEnemy : Enemies
{
    private SpriteRenderer _spriteRenderer;
    protected override void Awake()
    {
        base.Awake();
        enemy = Enemy.ArmorTank;
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public override void TakenDame()
    {
        base.TakenDame();
        switch (health)
        {
            case 3:
                _spriteRenderer.color=Color.yellow;
                break;
            case 2:
                _spriteRenderer.color = Color.magenta;
                break;
            case 1:
                _spriteRenderer.color = Color.white;
                break;
        }
    }
}
