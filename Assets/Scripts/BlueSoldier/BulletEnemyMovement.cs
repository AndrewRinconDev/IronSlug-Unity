using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletEnemyMovement : MonoBehaviour
{
    private Rigidbody2D bulletRB;

    private Animator an;
    private Transform playerTransform;
    public float bulletSpeed;
    public float bulletLife;
    private float direction;

    public GameObject audioExplosion;

    public int DeathType;

    private void Awake()
    {
        bulletRB = GetComponent<Rigidbody2D>();
        an = GetComponent<Animator>();
        
        if (GameObject.FindGameObjectWithTag("Player") != null)
        {
            playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        }
        pointToPlayer();
        Physics2D.IgnoreLayerCollision(10, 17);
        Physics2D.IgnoreLayerCollision(11, 17);
        Physics2D.IgnoreLayerCollision(14, 17);
        Physics2D.IgnoreLayerCollision(15, 17);
    }

    void Start()
    {
        bulletRB.velocity = new Vector2(bulletSpeed * direction, bulletRB.velocity.y);
        transform.localScale = new Vector3(direction,1);
    }

    void Update()
    {
        Destroy(gameObject, bulletLife);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (DeathType == 1)
            {
                collision.gameObject.GetComponent<DeadPlayerAnimation>().DeadBullet();
            }
            else if(DeathType == 2)
            {
                collision.gameObject.GetComponent<DeadPlayerAnimation>().DeadBurn();
                emitirSonido(audioExplosion, 1);
            }
            else if (DeathType == 3)
            {
                collision.gameObject.GetComponent<DeadPlayerAnimation>().DeadElectro();
            }
        }

        if (collision.gameObject.tag == "Train")
        {
            collision.gameObject.GetComponent<MovementTrain>().ReviewDamage();
        }

        an.SetTrigger("explosion");
        bulletRB.velocity = new Vector2(0, 0);
        Invoke("DestoyBullet", 0.5f);
    }

    private void DestoyBullet()
    {
        Destroy(gameObject);
    }

    private void pointToPlayer()
    {
        if (playerTransform != null)
        {
            float directionX = playerTransform.position.x - transform.position.x;
            if (directionX > 1)
            {
                direction = 1;
            }
            else if (directionX < 1)
            {
                direction = -1;
            }
        }
        else
        {
            direction = 0;
        }
    }

    public void emitirSonido(GameObject prefab, float timeDestroy)
    {
        Instantiate(prefab, transform.position, Quaternion.identity);
        if (transform != null)
        {
            //Destroy(prefab, timeDestroy);
        }
    }
}
