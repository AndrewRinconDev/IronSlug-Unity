using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoSuperSoldier : MonoBehaviour
{
    private Rigidbody2D soldierRB;
    private Animations an;
    private float speedWalk;

    public float moveRate;
    private float moveCount;
    private bool isChase;

    private GameObject player;
    public bool isNear;

    void Start()
    {
        speedWalk = 3f;
        isNear = false;
        moveCount = 0;
        an = GetComponent<Animations>();
        player = GameObject.Find("Player");
        soldierRB = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (isNear && player != null)
        {
            if (!GetComponent<DeadAnimation>().isDead)
            {
                if (!player.GetComponent<DeadPlayerAnimation>().isDead)//Chase
                {
                    moveCount += Time.deltaTime;
                    if (moveCount >= moveRate)
                    {
                        isChase = !isChase;
                        GetComponent<ActionSSoldier>().isSuper = isChase;
                        moveCount = 0;
                    }
                    if (!isChase)
                    {
                        PlayerChase();
                    }
                    else
                    {
                        PlayerMovement(0);
                    }
                }
                else
                {
                    PlayerMovement(0);
                }
            }
        }
        else
        {
            an.PlayerIdle();
        }
    }

    private void PlayerChase()
    {
        float directionX = player.transform.position.x - transform.position.x;
        PlayerMovement(directionX);
    }

    private void PlayerMovement(float movX)
    {
        if (movX > 0.1)
        {
            soldierRB.velocity = new Vector2(speedWalk, soldierRB.velocity.y);
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (movX < -0.1)
        {
            soldierRB.velocity = new Vector2(-speedWalk, soldierRB.velocity.y);
            transform.localScale = new Vector3(1, 1, 1);
        }
        else
        {
            soldierRB.velocity = new Vector2(0, 0);
            //transform.localScale = new Vector3(1, 1, 1);
        }

        an.PlayerWalk();
    }
}