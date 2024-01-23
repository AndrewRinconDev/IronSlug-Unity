using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderShottingSuper : MonoBehaviour
{
    void Start()
    {
        Physics2D.IgnoreLayerCollision(12, 17);//granade
        Physics2D.IgnoreLayerCollision(13, 17);//granade enemies
        Physics2D.IgnoreLayerCollision(17, 17);//Bullet
        GameObject.FindGameObjectWithTag("ColliderPreNextLevel").GetComponent<Collider2D>().enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            gameObject.transform.root.gameObject.GetComponent<MovimientoSuperSoldier>().isNear= true;
            gameObject.transform.root.gameObject.GetComponent<ActionSSoldier>().isSuper = true;
            GameObject.FindGameObjectWithTag("ColliderPreNextLevel").GetComponent<Collider2D>().enabled = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            gameObject.transform.root.gameObject.GetComponent<MovimientoSuperSoldier>().isNear = false; ;
            
        }
    }
}
