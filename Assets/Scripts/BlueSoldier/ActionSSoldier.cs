using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionSSoldier : MonoBehaviour
{
    public Transform attackCheck;
    public float attackRadius;

    private Animations an;
    private Collider2D[] hitEnemies;

    private bool isHitting;

    public Transform granadeSpawner;
    public Transform granadeDirection;
    public GameObject granadePrefab;

    public Transform BulletSpawner;
    public GameObject BulletPrefab;

    public float throwForce;
    private Vector2 dirGranade;
    private GameObject player;
    private MovimientoSoldierPatrol movSoldierPatrol;

    public bool isShoot;
    public bool isThrow;
    public float shootingRate;
    private float shootingCount;

    public bool isSuper;
    public bool isSuper2;

    public GameObject audioShoot;
    public GameObject audioAttack;

    void Start()
    {
        isShoot = false;
        isThrow = false;
        isHitting = false;
        shootingCount = 0;
        an = GetComponent<Animations>();
        player = GameObject.Find("Player");
        Physics2D.IgnoreLayerCollision(9, 10);
        Physics2D.IgnoreLayerCollision(10, 10);
        dirGranade = (granadeDirection.position - granadeSpawner.position).normalized;
    }

    private void Update()
    {
        Console.WriteLine(shootingCount);
        if (isShoot)
        {
            shootingCount += Time.deltaTime;
            if (shootingCount >= shootingRate)
            {
                Shooting();
                shootingCount = 0;
            }
        }

        if (isThrow)
        {
            shootingCount += Time.deltaTime;
            if (shootingCount >= shootingRate)
            {
                ThrowGranade();
                shootingCount = 0;
            }
        }

        if (isSuper)
        {
            shootingCount += Time.deltaTime;
            if (shootingCount >= shootingRate)
            {
                SuperShooting();
                shootingCount = 0;
            }
        }
    }

    void FixedUpdate()
    {
        if (isHitting)
        {
            hitEnemies = Physics2D.OverlapCircleAll(attackCheck.position, attackRadius);
            foreach (Collider2D enemy in hitEnemies)
            {
                if (enemy.gameObject.tag == "Player")
                {
                    if (isSuper2)
                    {
                        enemy.gameObject.GetComponent<DeadPlayerAnimation>().DeadElectro();
                    }
                    else
                    {
                        enemy.gameObject.GetComponent<DeadPlayerAnimation>().DeadBullet();
                    }
                }
            }
        }
    }

    public void PlayerHit()
    {
        if (!GetComponent<DeadAnimation>().isDead)
        {
            isHitting = true;
            an.PlayerHit();
            emitirSonido(audioAttack, 1);
            Invoke("disableHit", 0.5f);
        }
    }

    private void disableHit()
    {
        isHitting = false;
    }

    public void ThrowGranade()
    {
        bool throwG = pointToPlayer();
        if (throwG && !GetComponent<DeadAnimation>().isDead)
        {
            an.ThrowGranade();
            movSoldierPatrol = GetComponent<MovimientoSoldierPatrol>();
            if (movSoldierPatrol != null)
            {
                movSoldierPatrol.speedWalk = 2;
            }
            getPlayerDirection();
            Invoke("appearGranade", 0.7f);
        }
    }

    public void Shooting()
    {
        if (pointToPlayer() && !GetComponent<DeadAnimation>().isDead)
        {
            an.ShootBullet();
            Invoke("appearBullet", 0.6f);
        }
    }

    public void SuperShooting()
    {
        if (pointToPlayer() && !GetComponent<DeadAnimation>().isDead)
        {
            an.ShootBullet();
            appearBullet();
            Invoke("appearBullet", 1f);
        }
    }

    private bool pointToPlayer()
    {
        bool shoot = false;
        if (player != null && !GetComponent<DeadAnimation>().isDead)
        {
            float directionX = player.transform.position.x - transform.position.x;

            if (directionX > 1)
            {
                transform.localScale = new Vector3(-1, 1, 1);
                shoot = true;
            }

            if (directionX < -1)
            {
                transform.localScale = new Vector3(1, 1, 1);
                shoot = true;
            }
        }

        return shoot;
    }

    private void getPlayerDirection()
    {
        if (transform.localScale.x > 0)
        {
            if (dirGranade.x > 0)
            {
                dirGranade.x *= -1;
            }
        }
        else
        {
            if (dirGranade.x < 0)
            {
                dirGranade.x *= -1;
            }
        }
    }

    private void appearGranade()
    {
        if (!GetComponent<DeadAnimation>().isDead)
        {
            GameObject nuevaGranada = Instantiate(granadePrefab, granadeSpawner.position + granadeSpawner.forward, granadeSpawner.rotation);
            if (movSoldierPatrol != null)
            {
                movSoldierPatrol.speedWalk = 3;
            }
            nuevaGranada.GetComponent<Rigidbody2D>().AddForce(throwForce * dirGranade);
        }
    }

    private void appearBullet()
    {
        if (!GetComponent<DeadAnimation>().isDead && pointToPlayer())
        {
            emitirSonido(audioShoot, 1);
            Instantiate(BulletPrefab, BulletSpawner.position, BulletSpawner.rotation);
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
