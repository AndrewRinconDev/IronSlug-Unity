using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderStopTrain : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Train"))
        {
            collision.gameObject.GetComponent<MovementTrain>().stopFinally = true;
            collision.gameObject.GetComponent<MovementTrain>().stop();
        }
    }
}
