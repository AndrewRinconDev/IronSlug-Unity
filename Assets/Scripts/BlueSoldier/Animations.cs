using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animations : MonoBehaviour
{
    private Rigidbody2D soldierRB;
    private Animator an;

    void Start()
    {
        an = GetComponent<Animator>();
        soldierRB = GetComponent<Rigidbody2D>();
    }

    public void PlayerIdle()
    {
        an.SetFloat("MovX",0);
    }

    public void PlayerWalk()
    {
        an.SetFloat("MovX", Mathf.Abs(soldierRB.velocity.x));
    }

    public void PlayerHit()
    {
        an.SetTrigger("Hit");
    }

    public void ThrowGranade()
    {
        an.SetTrigger("ThrowGranade");
    }

    public void ShootBullet()
    {
        an.SetTrigger("ShootBullet");
    }
}
