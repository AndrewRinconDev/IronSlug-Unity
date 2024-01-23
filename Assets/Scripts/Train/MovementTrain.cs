using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MovementTrain : MonoBehaviour
{
    private Rigidbody2D trainRB;
    private int hitsTemp;
    public int damage;

    public Animator anBody;
    public Animator anWheels;
    public Animator anWheels2;
    public float trainSpeed;
    public int hits;

    public bool isStop;
    public bool stopFinally;
    private GameObject gameOver;
    private GameObject player;
    private GameObject resetButton;
    private string ultimateName;
    public GameObject audioTrain;

    private void Awake()
    {
        trainRB = GetComponent<Rigidbody2D>();
        //Physics2D.IgnoreLayerCollision(11, 14);
    }

    private void Start()
    {
        isStop = false;
        stopFinally = false;
        hitsTemp = hits;
        damage = 0;
        ultimateName = string.Empty;
        player = GameObject.Find("Player");
        gameOver = GameObject.FindGameObjectWithTag("CanvasGameOver");
        resetButton = gameOver.GetComponent<GameOver>().resetButton;
        resetButton.gameObject.SetActive(false);
    }
    void Update()
    {
        if (!isStop && !stopFinally)
        {
            road();
            emitirSonido(audioTrain);
        }
    }

    public void stop()
    {
        anWheels.SetBool("stop", true);
        anWheels2.SetBool("stop", true);
        isStop = true;
        //DetenerSonido(audioTrain);
    }

    public void reRoad()
    {
        if (damage < 5 && !stopFinally)
        {
            anWheels.SetBool("stop", false);
            anWheels2.SetBool("stop", false);
            isStop = false;
            ultimateName = string.Empty;
            emitirSonido(audioTrain);
        }
    }

    private void road()
    {
        trainRB.velocity = new Vector2(trainSpeed, trainRB.velocity.y);
        transform.localScale = new Vector3(1, 1, 1);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemies")
        {
            collision.gameObject.GetComponent<DeadAnimation>().DeadKnife();
            if (!ultimateName.Equals(collision.gameObject.name))
            {
                ReviewDamage();
                ultimateName = collision.gameObject.name;
            }
        }
        else if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<DeadPlayerAnimation>().DeadBullet();
            stop();
            if (!ultimateName.Equals(collision.gameObject.name))
            {
                ReviewDamage();
                ultimateName = collision.gameObject.name;
            }
        }
    }

    public void ReviewDamage()
    {
        hitsTemp--;
        if (hitsTemp == 0)
        {
            damage++;
            hitsTemp = hits;
            anBody.SetInteger("damage", damage);
        }

        if (damage == 5)
        {
            stop();
            anWheels.SetBool("stop", true);
            anWheels2.SetBool("stop", true);
            initGameOver();
        }
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

    public void emitirSonido(GameObject prefab)
    {
        Instantiate(prefab, transform.position, Quaternion.identity);

    }

    //public void DetenerSonido(GameObject prefab)
    //{
    //    prefab.GetComponent<AudioSource>().volume = 0;
    //}
}
