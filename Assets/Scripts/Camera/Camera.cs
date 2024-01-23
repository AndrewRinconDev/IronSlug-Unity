using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    private GameObject player;
    public float timeSua;
    private Vector3 vel; 
    void Start()
    {
        vel = Vector3.zero;
        timeSua = 0.15f;
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void FixedUpdate()
    {
        if (player != null)
        {
            Vector3 Pospla = new Vector3(player.transform.position.x + 4, 0.18f, transform.position.z);

            transform.position = Vector3.SmoothDamp(transform.position, Pospla, ref vel, timeSua);
        }
    }
}
