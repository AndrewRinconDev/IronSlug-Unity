using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadAnimation : MonoBehaviour
{
    private Animator an;
    public bool isDead;
    bool isContinue;
    public int hits;

    private GameObject ResetVictory;

    public GameObject audioBullet;
    public GameObject audioBurn;
    private UpdateScore updateScore;

    void Start()
    {
        an = GetComponent<Animator>();
        isDead = false;
        isContinue = true;
        ResetVictory = GameObject.FindGameObjectWithTag("ResetButtonVictory");
        updateScore = GameObject.Find("Player").GetComponent<UpdateScore>();
    }

    public void DeadKnife()
    {
        if (!isDead && hits == 1)
        {
            isDead = true;
            emitirSonido(audioBullet, 1);
            Destroy(gameObject, 1);
            an.SetTrigger("DeadKnife");
            initVictoryGame();
        }
        hits--;
    }

    public void DeadBurn()
    {
        if (!isDead && hits == 1)
        {
            isDead = true;
            emitirSonido(audioBurn, 1);
            Destroy(gameObject, 1.5f);
            an.SetTrigger("DeadBurn");
            initVictoryGame();
        }
        hits--;
    }

    public bool DeadBullet()
    {
        if (!isDead)
        {
            isContinue = false;
            if (hits == 1)
            {
                isDead = true;
                emitirSonido(audioBullet, 1);
                Destroy(gameObject, 0.75f);
                an.SetTrigger("DeadBullet");
                initVictoryGame();
            }
            hits--;
        }
        else
        {
            isContinue = true;
        }

        return isContinue;
    }

    public void initVictoryGame()
    {
        if (gameObject.tag.Equals("SuperEnemy"))
        {
            if (gameObject.GetComponent<ActionSSoldier>().isSuper2)
            {
                updateScore.addScore(10000);
                GameObject victory = GameObject.FindGameObjectWithTag("CanvasVictory");
                victory.GetComponent<VictoryManager>().VictoryPlayer();
            }
            else
            {
                updateScore.addScore(5000);
                GameObject.FindGameObjectWithTag("ColliderNextLevel").GetComponent<Collider2D>().enabled = false;
                GameObject.FindGameObjectWithTag("ColliderNextLevel").GetComponent<Animator>().SetBool("go",true);
            }
        }else{
            updateScore.addScore(1000);
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
