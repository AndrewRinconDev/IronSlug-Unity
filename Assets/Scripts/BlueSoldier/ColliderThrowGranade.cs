using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderThrowGranade : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Physics2D.IgnoreLayerCollision(12, 14);//granade
        Physics2D.IgnoreLayerCollision(13, 14);//granade enemies
        Physics2D.IgnoreLayerCollision(17, 14);//Bullet
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            gameObject.transform.root.gameObject.GetComponent<ActionSSoldier>().ThrowGranade();
            gameObject.transform.root.gameObject.GetComponent<ActionSSoldier>().isThrow = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        gameObject.transform.root.gameObject.GetComponent<ActionSSoldier>().isThrow = false;
    }
}
