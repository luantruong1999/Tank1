using System;
using System.Collections;
using System.Collections.Generic;
using ScriptTableObject;
using UnityEngine;
using Random = UnityEngine.Random;

public abstract class Enemies : Tank
{
    protected Enemy enemy;
    protected int health;
    private int point;
    private Sprite spritePonit;
    private Vector2[] directions = new Vector2[4] {Vector2.up, Vector2.right, Vector2.down, Vector2.left};
    [SerializeField]private Vector2 curDirection;

    protected override void Awake()
    {
        base.Awake();
        layerPhysic2D=LayerMask.GetMask("Player","Enemy","Enviroment","Water");
    }
    
    private void Start()
    {
        var data = Data.instance.EnemyData.Data[enemy];
        speed = data.speed;
        health = data.health;
        point = data.points;
        spritePonit = data.spritePoints;
        InvokeRepeating("RandomDirection",0,Random.Range(1f,4f));
    }

    private void FixedUpdate()
    {
        Rotation(curDirection);
        if (CheckVector(curDirection))
        {
            animator.SetBool("Move",true);
            if (!isMoving) StartCoroutine(Move(curDirection));
        }
        else
        {
            isMoving = false;
            StopAllCoroutines();
            animator.SetBool("Move",false); 
        }
    }

    public virtual void TakenDame()
    {
        health--;
        if (health <= 0)
        {
            Die();
        }
    }

    public override void Die()
    {
        Debug.Log("1");
    }

    protected override bool CheckVector(Vector2 vector2)
    {
        Vector2 origin = new Vector2(
            _boxCollider2D.bounds.center.x + vector2.x * (_boxCollider2D.bounds.size.x/2+0.21f),
            _boxCollider2D.bounds.center.y + vector2.y * (_boxCollider2D.bounds.size.y/2+0.21f));
        Vector2 size = new Vector2(vector2.y != 0 ? _boxCollider2D.bounds.size.x : 0.2f,
            vector2.x != 0 ? _boxCollider2D.bounds.size.y : 0.2f);
        RaycastHit2D hit = Physics2D.BoxCast(origin, size, 0f, Vector2.zero, 0f, layerPhysic2D);
        return !hit.collider;
    }

    private void RandomDirection()
    {
        curDirection = directions[Random.Range(0, directions.Length)];
    }

    
}

