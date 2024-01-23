using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContactGranade : MonoBehaviour
{
    private Animator an;
    private Collider2D[] hitEnemies;
    private Rigidbody2D granadeRB;

    public Transform explosionCheck;
    public float explosionRadius;

    public GameObject audioExplosion;

    void Start()
    {
        an = GetComponent<Animator>();
        granadeRB = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        granadeRB.velocity = new Vector2(0,0);
        granadeRB.gravityScale = 0;
        emitirSonido(audioExplosion, 1);
        an.SetTrigger("Explosion");
        Destroy(gameObject, 0.75f);

        hitEnemies = Physics2D.OverlapCircleAll(explosionCheck.position, explosionRadius);
        foreach (Collider2D enemy in hitEnemies)
        {
            if (enemy.gameObject.tag == "Enemies" || enemy.gameObject.tag == "SuperEnemy")
            {
                enemy.gameObject.GetComponent<DeadAnimation>().DeadBurn();
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
