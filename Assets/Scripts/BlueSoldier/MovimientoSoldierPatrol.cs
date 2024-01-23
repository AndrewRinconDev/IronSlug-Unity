using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoSoldierPatrol : MonoBehaviour
{
    private Rigidbody2D soldierRB;
    private Animations an;
    public float speedWalk;

    private GameObject player;
    public bool isNear;

    public Transform groundCheck;
    public float checkRadius;
    public LayerMask whatIsGround;
    private bool grounded;

    void Start()
    {
        speedWalk = 3;
        isNear = false;
        an = GetComponent<Animations>();
        player = GameObject.Find("Player");
        soldierRB = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (!GetComponent<DeadAnimation>().isDead)
        {
            if (!grounded)
                ReverseDirection();

            soldierRB.velocity = GetDirection();
            an.PlayerWalk();
        }
    }

    private void FixedUpdate()
    {
        grounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);
    }

    public Vector2 GetDirection()
    {
        Vector2 direction;
        direction = new Vector2(speedWalk * transform.localScale.x *-1, soldierRB.velocity.y);
        return direction;
    }
    public void ReverseDirection()
    {
        soldierRB.velocity = new Vector2(speedWalk * -1f, soldierRB.velocity.y);
        transform.localScale = new Vector3(transform.localScale.x * -1f, 1f, 1f);
    }
}