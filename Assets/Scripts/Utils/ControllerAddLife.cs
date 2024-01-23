using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerAddLife : MonoBehaviour
{
    private bool setLife = false;
    public GameObject AudioOk;
    void Start()
    {
        Physics2D.IgnoreLayerCollision(10, 15);
        Physics2D.IgnoreLayerCollision(11, 15);
        Physics2D.IgnoreLayerCollision(12, 15);
        Physics2D.IgnoreLayerCollision(13, 15);
        Physics2D.IgnoreLayerCollision(14, 15);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (!setLife)
            {
                collision.gameObject.GetComponent<DeadPlayerAnimation>().addLife();
                emitirSonido(AudioOk, 0.8f);
                Destroy(gameObject);
                setLife = true;
            }
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
