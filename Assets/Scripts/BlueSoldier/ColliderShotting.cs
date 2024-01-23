using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderShotting : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Physics2D.IgnoreLayerCollision(12, 17);//granade
        Physics2D.IgnoreLayerCollision(13, 17);//granade enemies
        Physics2D.IgnoreLayerCollision(17, 17);//Bullet
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            gameObject.transform.root.gameObject.GetComponent<ActionSSoldier>().isShoot = true;
            //gameObject.transform.root.gameObject.GetComponent<ActionSSoldier>().Shooting();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            gameObject.transform.root.gameObject.GetComponent<ActionSSoldier>().isShoot = false;
        }
    }
}
