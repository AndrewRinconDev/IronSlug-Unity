using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Movimiento : MonoBehaviour
{
    private float movX;
    private float fast;
    private float jump;
    private float speedPlayer;
    private float speedWalk;
    private float speedRun;
    private float jumpForce;
    private Rigidbody2D playerRB;
    private Animator an;
    private Collider2D[] hitEnemies;
    private bool isHitting;

    public Text txtGranades;

    public GameObject audioShoot;
    public GameObject audioHit;

    public Transform attackCheck;
    public float attackRadius;

    public Transform groundCheck;
    public float checkRadius;
    public LayerMask whatIsGround;

    public Animator anChest;
    public Animator anLegs;

    private bool grouded;
    private bool doubleJump;

    public Transform bulletSpawner;
    public GameObject bulletPrefab;

    public Transform granadeSpawner;
    public Transform granadeDirection;
    public GameObject granadePrefab;
    public float throwForce;
    private Vector2 dirGranade;
    public int countGranades;

    // Start is called before the first frame update
    void Start()
    {
        speedWalk = 5;
        speedRun = 8;
        jumpForce = 14;
        isHitting = false;
        playerRB = GetComponent<Rigidbody2D>();
        an = GetComponent<Animator>();
        dirGranade = (granadeDirection.position - granadeSpawner.position).normalized;
    }

    void FixedUpdate()
    {
        grouded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);

        if (isHitting)
        {
            disableAttack();
            hitEnemies = Physics2D.OverlapCircleAll(attackCheck.position, attackRadius);
            foreach (Collider2D enemy in hitEnemies)
            {
                if (enemy.gameObject.tag == "Enemies" || enemy.gameObject.tag == "SuperEnemy")
                {
                    enemy.gameObject.GetComponent<DeadAnimation>().DeadKnife();
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (grouded)
        {
            doubleJump = false;
        }

        if (!GetComponent<DeadPlayerAnimation>().isDead && !EventSystem.current.IsPointerOverGameObject(-1))
        {
            PlayerMovement();
            PlayerJump();
            PlayerHit();
            PlayerShot();
            ThrowGranade();
            txtGranades.text = countGranades.ToString();
        }
    }

    private void PlayerMovement()
    {
        movX = Input.GetAxisRaw("Horizontal");
        fast = Input.GetAxisRaw("Run");

        if (Input.GetButton("Run"))
            speedPlayer = speedRun;
        else
            speedPlayer = speedWalk;

        if (movX > 0)
        {
            playerRB.velocity = new Vector2(speedPlayer, playerRB.velocity.y);
            transform.localScale = new Vector3(1, 1, 1);
        }

        if (movX < 0)
        {
            playerRB.velocity = new Vector2(-speedPlayer, playerRB.velocity.y);
            transform.localScale = new Vector3(-1, 1, 1);
        }

        anChest.SetFloat("MovX", Mathf.Abs(playerRB.velocity.x));
        anLegs.SetFloat("MovX", Mathf.Abs(playerRB.velocity.x));
        anChest.SetFloat("Run", Mathf.Abs(fast));
        anLegs.SetFloat("Run", Mathf.Abs(fast));
    }

    private void PlayerJump()
    {
        jump = Input.GetAxisRaw("Jump");

        if (Input.GetButtonDown("Jump") && grouded)
        {
            playerRB.velocity = new Vector2(playerRB.velocity.x, jumpForce);

        }

        if (Input.GetButtonDown("Jump") && !grouded && !doubleJump)
        {
            playerRB.velocity = new Vector2(playerRB.velocity.x, jumpForce);
            doubleJump = true;
        }

        anChest.SetFloat("Jump", Mathf.Abs(playerRB.velocity.y));
        anLegs.SetFloat("Jump", Mathf.Abs(playerRB.velocity.y));
    }

    private void PlayerHit()
    {
        if (Input.GetButtonDown("Hit"))
        {
            isHitting = true;
            emitirSonido(audioHit, 1);
            anChest.SetTrigger("Hit");
            Invoke("disableAttack", 0.5f);
        }
    }

    private void disableAttack()
    {
        isHitting = false;
    }

    private void PlayerShot()
    {
        if (Input.GetButtonDown("Fire"))
        {
            emitirSonido(audioShoot, 1);
            anChest.SetTrigger("Shot");
            Instantiate(bulletPrefab, bulletSpawner.position, bulletSpawner.rotation);
        }
    }

    private void ThrowGranade()
    {
        if (countGranades > 0)
        {
            if (Input.GetButtonDown("Throw"))
            {
                anChest.SetTrigger("ThrowGranade");
                GameObject nuevaGranada = Instantiate(granadePrefab, granadeSpawner.position + granadeSpawner.forward, granadeSpawner.rotation);

                getPlayerDirection();
                nuevaGranada.GetComponent<Rigidbody2D>().AddForce(throwForce * dirGranade);
                countGranades--;
            }
        }
    }

    private void getPlayerDirection()
    {
        if (transform.localScale.x > 0)
        {
            if (dirGranade.x < 0)
            {
                dirGranade.x *= -1;
            }
        }
        else
        {
            if (dirGranade.x > 0)
            {
                dirGranade.x *= -1;
            }
        }
    }

    public void setReviewPosition()
    {
        transform.position = new Vector3(transform.position.x + 3f, transform.position.y + 5.5f, transform.position.z);
    }

    public void addGranades()
    {
        countGranades += 10;
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
