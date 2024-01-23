using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateScore : MonoBehaviour
{
    private int score;
    public Text txtScore;

    // Start is called before the first frame update
    void Start()
    {
      score = 0;
    }

    void Update()
    {
      txtScore.text = score.ToString();
    }

    public void addScore(int scoreWon)
    {
        score += scoreWon;
    }

    public int getScore()
    {
        return score;
    }
}
