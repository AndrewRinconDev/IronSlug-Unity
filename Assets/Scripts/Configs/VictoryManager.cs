using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VictoryManager : MonoBehaviour
{
    private Animator anim;
    private TimeManager timer;
    private UpdateScore updateScore;
    public GameObject txtFinalScore;
    public GameObject player;
    public bool isVictory;


    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        player= GameObject.Find("Player");
        timer = player.GetComponent<TimeManager>();
        updateScore = player.GetComponent<UpdateScore>();
        txtFinalScore = GameObject.FindGameObjectWithTag("VictoryFinalScore");
        isVictory = false;
    }

    // Update is called once per frame
    public void VictoryPlayer()
    {
        anim.SetTrigger("Victory");
        updateScore.addScore(timer.getTime() * 100);
        txtFinalScore.GetComponent<Text>().text = updateScore.getScore().ToString();
        player.GetComponent<DeadPlayerAnimation>().isGameOver = true;
        isVictory = true;
        timer.stopTime();
    }
}
