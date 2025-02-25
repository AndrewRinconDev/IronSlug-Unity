using UnityEngine;
using UnityEngine.UI;

public class TimeManager : MonoBehaviour
{
    private const int DEFAULT_TIME = 150;
    private float time;
    public Text txtTimer;
    private GameObject player;
    private GameObject gameOver;
    private GameObject resetButton;
    private bool isStopped;
    private DeadPlayerAnimation deadPlayer;

    // Start is called before the first frame update
    void Start()
    {
      time = DEFAULT_TIME;
      isStopped = false;
      player = GameObject.Find("Player");
      deadPlayer = player.GetComponent<DeadPlayerAnimation>();
      gameOver = GameObject.FindGameObjectWithTag("CanvasGameOver");
      resetButton = gameOver.GetComponent<GameOver>().resetButton;
      resetButton.gameObject.SetActive(false);
    }

    void Update()
    {
      if (isStopped || deadPlayer.isGameOver) return;

      time -= Time.deltaTime;
      txtTimer.text = time.ToString("f0");

      if (time <= 10) { 
        txtTimer.color = new Color(0.9F, 0.3F, 0.1F);
      }

      if (time <= 0) {
        isStopped = true; 
        initGameOver();
      }
    }

    public void restartTime()
    {
      time = DEFAULT_TIME;
      isStopped = false;
    }

    public int getTime()
    {
      return (int)time;
    }

    public void setTime(int newTime)
    {
      time = newTime;
    }

    public void addLevelTime()
    {
      time += DEFAULT_TIME;
      txtTimer.color = new Color(0.96F, 0.87F, 0.12F);
    }

    public void stopTime()
    {
      isStopped = true;
    }

    private void initGameOver()
    {
      DeadPlayerAnimation deadPlayer = player.GetComponent<DeadPlayerAnimation>();
      if (gameOver != null && !deadPlayer.isGameOver)
      {
          deadPlayer.isGameOver = true;
          deadPlayer.DeadBullet();
          gameOver.GetComponent<GameOver>().GameOverPlayer();
          resetButton.gameObject.SetActive(true);
      }
    }
}
