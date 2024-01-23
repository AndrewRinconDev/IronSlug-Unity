using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GranadeEnemy : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Physics2D.IgnoreLayerCollision(10, 13);
        Physics2D.IgnoreLayerCollision(13, 13);
    }
}
