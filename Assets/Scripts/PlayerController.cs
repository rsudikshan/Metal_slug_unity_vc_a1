using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEditor;
using UnityEngine;

public class PlayerController : MonoBehaviour
{


    [Header("Body Properties")]
    private Rigidbody2D rb2D;
    private BoxCollider2D boxCollider;

    [Header("GameObject")]
    public GameObject top;
    public GameObject bottom;
    public GameObject deadBody;

    [Header("Animator")]
    public Animator topAnimator;
    public Animator bottomAnimator;
    public Animator deadAnimator;

    [Header("GameManager")]
    public GameManager gameManager;

    
    private float horizontal;
    private float vertical;

    [Header("Values")]
    public float moveSpeed;
    public float jumpForce;

    [Header("Bool Checker")]
    public bool isGrounded = false;
    bool facingRight = true;

    [Header("Health System")]
    public HealthBar healthBar;
    public int maxHealth = 100;
    public int currentHealth;
    public int healItemAmount;

    [Header("Damage System")]
    public int damage = 20;

    private void Awake()
    {
        deadBody.SetActive(false);
        
    }

    private void Start()
    {
        topAnimator = top.GetComponent<Animator>();
        bottomAnimator = bottom.GetComponent<Animator>();
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        facingRight = true;   
        rb2D = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        gameManager = GameObject.FindGameObjectWithTag("GameManager"). GetComponent<GameManager>();


        topAnimator.Play("IDLE");
        bottomAnimator.Play("Idle");

    }

    public void Update()
    {
        

        transform.position = new Vector2(
            Mathf.Clamp(transform.position.x, -15f, 70f),
            Mathf.Clamp(transform.position.y, -4.5f, 4.5f)); ;
        
            horizontal = Input.GetAxisRaw("Horizontal");

        if (horizontal != 0f)
        {
                Movement();
                bottomAnimator.SetBool("Running", true);
        }
        else
        {
                bottomAnimator.SetBool("Running", false);
        }
        if (Input.GetButtonDown("Jump") && isGrounded == true && horizontal == 0f)
        {
            Jump();
            bottomAnimator.SetBool("IdleJump", true);
        }
        else if(Input.GetButtonDown("Jump") && isGrounded == true && horizontal != 0f)
        {
            Jump();
            bottomAnimator.SetBool("LongJump", true) ;
        }

        else
        {
            bottomAnimator.SetBool("IdleJump", false);
            bottomAnimator.SetBool("LongJump", false);
        }


        if (!facingRight && horizontal > 0f)
            {

                Flip();
            }
        else if (facingRight && horizontal < 0f)
            {

                Flip();
            }



        if (Input.GetMouseButtonDown(0))
        {
                Shoot();

        }
        else
        {
            topAnimator.SetBool("isShooting", false);
        }

        if(Input.GetKeyDown(KeyCode.R))
        {
            topAnimator.SetBool("Reloading", true);

        }
        else
        {
            topAnimator.SetBool("Reloading", false);
        }

        
        

          

    }


    private void Movement()
    {

        transform.position += new Vector3(horizontal * moveSpeed * Time.deltaTime, 0f, 0f);
        
    }
    

    private void Jump()
    {
        rb2D.velocity = new Vector2(0f, jumpForce);
    }

    private void Flip()
    {
       
            facingRight = !facingRight;

            transform.Rotate(0f, 180f, 0f);

        
    }

    public void Shoot()
    {
        topAnimator.SetBool("isShooting", true);
    }

    
    
    

    private void OnCollisionEnter2D(Collision2D collision)
    {
        

            if (collision.gameObject.CompareTag("Ground"))
            {
                isGrounded = true;
            }

        //if(collision.gameObject.CompareTag("Enemy"))
        //{
        //    DamageTaken();
        //}




    }




    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }

    public void Health()
    {
        currentHealth += healItemAmount;
        healthBar.SetHealth(currentHealth);
        if (currentHealth >= maxHealth)
        {
            currentHealth = maxHealth;

        }



    }

    public void DamageTaken()
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            this.enabled = false;
            top.SetActive(false);
            bottom.SetActive(false);
            deadBody.SetActive(true);
            deadAnimator.SetBool("Dead", true);
            gameManager.GameOver();
            GameOver.instance.ShowGameOver();
        }
    }

    
}
