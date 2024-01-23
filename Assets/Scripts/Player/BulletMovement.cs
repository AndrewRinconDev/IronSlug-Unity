using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class BulletMovement : MonoBehaviour
{
    private Rigidbody2D bulletRB;

    private Transform playerTransform;
    private Animator an;
    public float bulletSpeed;
    public float bulletLife;
    private bool isContinue;

    private void Awake()
    {
        bulletRB = GetComponent<Rigidbody2D>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        an = GetComponent<Animator>();
        Physics2D.IgnoreLayerCollision(11, 14);
        Physics2D.IgnoreLayerCollision(11, 11);
    }

    void Start()
    {
        bulletRB.velocity = new Vector2(bulletSpeed * playerTransform.localScale.x, bulletRB.velocity.y);
        transform.localScale = playerTransform.localScale;
        isContinue = true;
    }

    void Update()
    {
        Destroy(gameObject, bulletLife);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.gameObject.tag == "Enemies" || collision.gameObject.tag == "SuperEnemy") && isContinue)
        {
            isContinue = collision.gameObject.GetComponent<DeadAnimation>().DeadBullet();
        }
        else
        {
            isContinue = false;
        }

        if (!isContinue)
        {
            an.SetTrigger("explosion");
            bulletRB.velocity = new Vector2(0,0);
            Invoke("DestroyBullet", 0.5f);
        }
    }

    private void DestroyBullet()
    {
        Destroy(gameObject);
    }
}
