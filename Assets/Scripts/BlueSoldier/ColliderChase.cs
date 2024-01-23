using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderChase : MonoBehaviour
{
    void Start()
    {
        Physics2D.IgnoreLayerCollision(12, 14);//granade
        Physics2D.IgnoreLayerCollision(13, 14);//granade enemies
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            gameObject.transform.root.gameObject.GetComponent<MovimientoBlueSoldier>().isNear = true;//Chase
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            gameObject.transform.root.gameObject.GetComponent<MovimientoBlueSoldier>().isNear = false;//Chase
        }
    }
}
