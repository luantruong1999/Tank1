using System;
using System.Collections;
using UnityEngine;

public  abstract class Tank : MonoBehaviour
{
    protected Animator animator;
    protected float speed;
    protected Rigidbody2D _rigidbody2D;
    protected BoxCollider2D _boxCollider2D;
    protected LayerMask layerPhysic2D;
    protected bool isMoving;
    protected GameObject bullet;
    protected float lastTime;
    protected float delayTime=0.5f;
    protected virtual void Awake()
    {
        animator = GetComponent<Animator>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _boxCollider2D = GetComponent<BoxCollider2D>();
    }

    protected void Rotation(Vector2 vector2)
    {
        float z = -90 * vector2.x + 90*(Mathf.Abs(vector2.y) - vector2.y);
        transform.rotation=Quaternion.Euler(0,0,z);
    }

    protected IEnumerator Move(Vector2 vector2)
    {
        isMoving = true;
        _rigidbody2D.position = new Vector2(Mathf.Round(_rigidbody2D.position.x), Mathf.Round(_rigidbody2D.position.y));
        float movefloat = 0;
        Vector2 endPos;
        while (movefloat<1)
        {
            movefloat += speed * Time.deltaTime;
            movefloat = Mathf.Clamp(movefloat, 0f, 1f);
            endPos = _rigidbody2D.position + vector2 * Time.deltaTime * speed;
            if (movefloat >= 1)
            {
                endPos = new Vector2(Mathf.Round(endPos.x), Mathf.Round(endPos.y));
            }
            _rigidbody2D.MovePosition(endPos);
            yield return new WaitForFixedUpdate();
        }
        isMoving = false;
    }

    protected virtual void Shot()
    {
        if(bullet.activeInHierarchy) return;
        if (Time.time - lastTime < delayTime) return;
        lastTime = Time.time;
    }
    protected abstract bool CheckVector(Vector2 vector2);
    public abstract void Die();
    
}   
