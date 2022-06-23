using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Rigidbody2D _rigidbody2D;
    private bool isSuperBullet;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    public void SetBullet(float speed,Quaternion quaternion,bool isSuperBullet)
    {
        transform.rotation = quaternion;
        _rigidbody2D.velocity = transform.up*speed;
        this.isSuperBullet = isSuperBullet;
        gameObject.SetActive(true);
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        var obj = col.gameObject;
        switch (obj.tag)
        {
            case "Bullet":
                //disable;
                return;
            case "Sat":
                if(isSuperBullet)obj.SetActive(false);
                return;
            case "Gach":
                obj.SetActive(false);
                return;
        }
    }
}
