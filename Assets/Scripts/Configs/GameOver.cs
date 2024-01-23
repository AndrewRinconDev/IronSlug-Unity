using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    private Animator anim;
    //public GameObject player;
    public bool isGameOver;
    public GameObject resetButton;
    public GameObject player;
    public GameObject txtFinalScore;


    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        player = GameObject.Find("Player");
        txtFinalScore = GameObject.FindGameObjectWithTag("FinalScore");
        isGameOver = false;
    }

    // Update is called once per frame
    public void GameOverPlayer()
    {
        if (!isGameOver) {
            player.GetComponent<DeadPlayerAnimation>().isGameOver = true;
            txtFinalScore.GetComponent<Text>().text = player.GetComponent<UpdateScore>().getScore().ToString();
            anim.SetTrigger("GameOver");
            isGameOver = true;
        }
    }
}
