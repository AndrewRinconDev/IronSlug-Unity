using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerUtilGranades : MonoBehaviour
{
    private bool setGranades = false;

    public GameObject audioGranade;
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
            if (!setGranades)
            {
                collision.gameObject.GetComponent<Movimiento>().addGranades();
                emitirSonido(audioGranade, 1);
                Destroy(gameObject);
                setGranades = true;
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
