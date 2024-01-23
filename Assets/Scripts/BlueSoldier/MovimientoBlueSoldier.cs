using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoBlueSoldier : MonoBehaviour
{
    private Rigidbody2D soldierRB;
    private Animations an;
    private float speedWalk;

    private GameObject player;
    public bool isNear;

    void Start()
    {
        speedWalk = 2.5f;
        isNear = false;
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
                    PlayerChase();
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
        }else if (movX < -0.1)
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
