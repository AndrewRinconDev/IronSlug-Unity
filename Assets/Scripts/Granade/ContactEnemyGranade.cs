using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContactEnemyGranade : MonoBehaviour
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
        if (granadeRB != null)
        {
            granadeRB.velocity = new Vector2(0, 0);
            granadeRB.gravityScale = 0;
            emitirSonido(audioExplosion, 1);
            an.SetTrigger("Explosion");
            Destroy(gameObject, 1);

            hitEnemies = Physics2D.OverlapCircleAll(explosionCheck.position, explosionRadius);
            foreach (Collider2D enemy in hitEnemies)
            {
                if (enemy.gameObject.tag == "Player")
                {
                    enemy.gameObject.GetComponent<DeadPlayerAnimation>().DeadBurn();
                }

                if (enemy.gameObject.tag == "Train")
                {
                    enemy.gameObject.GetComponent<MovementTrain>().ReviewDamage();
                }
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
