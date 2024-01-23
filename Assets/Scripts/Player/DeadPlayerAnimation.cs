using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeadPlayerAnimation : MonoBehaviour
{
    private Animator an;
    public Animator anChest;
    public Animator anLegs;
    public bool isDead;
    public int lifes;
    public bool isGameOver;
    public Text txtLifes;
    public GameObject ResetGameOver;

    public GameObject audioBullet;
    public GameObject audioBurn;
    public GameObject audioElectro;
    public GameObject audioAppear;


    // Start is called before the first frame update
    void Start()
    {
        an = GetComponent<Animator>();
        ResetGameOver.gameObject.SetActive(false);
        emitirSonido(audioAppear, 1);
        isDead = false;
        isGameOver = false;
    }

    private void Update()
    {
        txtLifes.text = lifes.ToString();
    }

    public void addLife()
    {
        lifes++;
    }

    public void DeadElectro()
    {
        if (!isDead)
        {
            isDead = true;
            emitirSonido(audioElectro, 1);
            emitirSonido(audioBullet, 0.2f);
            if (lifes > 1)
            {
                Invoke("revivir", 2);
            }
            else
            {
                initGameOver();
            }
            anChest.SetTrigger("Dead");
            anLegs.SetTrigger("Dead");
            an.SetTrigger("DeadElectro");
        }
    }

    public void DeadBurn()
    {
        if (!isDead)
        {
            isDead = true;
            emitirSonido(audioBurn, 1);
            if (lifes > 1)
            {
                Invoke("revivir", 2);
            }
            else
            {
                initGameOver();
            }
            anChest.SetTrigger("Dead");
            anLegs.SetTrigger("Dead");
            an.SetTrigger("DeadBurn");
        }
    }

    public bool DeadBullet()
    {
        bool isFirst = false;
        if (!isDead)
        {
            isFirst = isDead = true;
            emitirSonido(audioBullet, 1);
            if (lifes > 1)
            {
                Invoke("revivir", 2);
            }
            else
            {
                initGameOver();
            }

            anChest.SetTrigger("Dead");
            anLegs.SetTrigger("Dead");
            an.SetTrigger("Dead");
        }
        return isFirst;
    }

    private void revivir()
    {
        if (isGameOver) return;

        lifes--;
        gameObject.GetComponent<Movimiento>().setReviewPosition();
        emitirSonido(audioAppear, 1);
        anChest.SetTrigger("isReviving");
        anLegs.SetTrigger("isReviving");
        an.SetTrigger("isReviving");
        isDead = false;
        GameObject train = GameObject.FindGameObjectWithTag("Train");
        if (train != null)
        {
            train.GetComponent<MovementTrain>().reRoad();
        }
    }
    public void emitirSonido(GameObject prefab, float timeDestroy)
    {
        Instantiate(prefab, transform.position, Quaternion.identity);
        if (!isDead && transform != null)
        {
            //Destroy(prefab, timeDestroy);
        }
    }

    private void initGameOver()
    {
        if (isGameOver) return;

        lifes = 0;
        isGameOver = true;
        Destroy(gameObject, 1);
        GameObject gameOver = GameObject.FindGameObjectWithTag("CanvasGameOver");
        gameOver.GetComponent<GameOver>().GameOverPlayer();
        ResetGameOver.gameObject.SetActive(true);
    }


}
