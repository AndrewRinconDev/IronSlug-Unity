using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VictoryManager : MonoBehaviour
{
    public int TiME_BONUS_MULTIPLIER = 200;
    public int LIVES_BONUS_MULTIPLIER = 400;
    private Animator anim;
    private TimeManager timer;
    private UpdateScore updateScore;
    private DeadPlayerAnimation deadPlayer;
    public GameObject txtFinalScore;
    public GameObject txtBonusScore;
    public GameObject txtTotalScore;
    public GameObject player;
    public bool isVictory;


    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        player= GameObject.Find("Player");
        timer = player.GetComponent<TimeManager>();
        updateScore = player.GetComponent<UpdateScore>();
        deadPlayer = player.GetComponent<DeadPlayerAnimation>();
        txtFinalScore = GameObject.FindGameObjectWithTag("VictoryFinalScore");
        txtBonusScore = GameObject.FindGameObjectWithTag("VictoryBonusScore");
        txtTotalScore = GameObject.FindGameObjectWithTag("VictoryTotalScore");
        isVictory = false;
    }

    // Update is called once per frame
    public void VictoryPlayer()
    {
        anim.SetTrigger("Victory");
        float timeBonus = timer.getTime() * TiME_BONUS_MULTIPLIER;
        float livesBonus = deadPlayer.lives * LIVES_BONUS_MULTIPLIER;
        float totalBonus = timeBonus + livesBonus;
        float total = updateScore.getScore() + totalBonus;
        txtFinalScore.GetComponent<Text>().text = updateScore.getScore().ToString();
        txtBonusScore.GetComponent<Text>().text = totalBonus.ToString("f0");
        txtTotalScore.GetComponent<Text>().text = total.ToString("f0");
        deadPlayer.isGameOver = true;
        isVictory = true;
        timer.stopTime();
    }
}
