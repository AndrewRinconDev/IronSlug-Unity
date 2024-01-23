using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderAttack : MonoBehaviour
{
    bool throwG = false;
    bool shootB = false;
    void Start()
    {
        Physics2D.IgnoreLayerCollision(12, 14);
        Physics2D.IgnoreLayerCollision(13, 14);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Invoke("hitEnemy", 0.6f);
        }
    }

    private void hitEnemy()
    {
        gameObject.transform.root.gameObject.GetComponent<ActionSSoldier>().PlayerHit();
    }
}
