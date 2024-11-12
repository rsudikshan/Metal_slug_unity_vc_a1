using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;


public class EnemyController : MonoBehaviour
{

    public DropItem1 dropItem1;

    public Transform player;
    public SpriteRenderer spriteRenderer;
    public float speed;
    public bool flip;
    public float lineOfSight;
    public float attackSight;

    public Color hitColor = Color.red;
    public float hitDuration;
    public float duration;
    public Renderer enemyRenderer;
    public ScoreScript scoreScript;
    public GameManager gameManager;

    public EnemyHealthBarScript enemyHealthBarScript;

    private Color originalColor;
    private bool isHit = false;


    private Rigidbody2D ebody;
    private BoxCollider2D ecollider;
    private Animator soldierAnim;

    public int maxHealth = 100;
    public int currenthealth;

    
    private int damage = 10;
    private float animDuration = 0.7f;

    Animator anim;
    public float attackTime = 3f;
    float time;

    PlayerController playerController;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        currenthealth = maxHealth;
        //enemyHealthBarScript.SetMaxHealth(maxHealth);
        originalColor = enemyRenderer.material.color;
        ebody = GetComponent<Rigidbody2D>();
        ecollider = GetComponent<BoxCollider2D>();
        soldierAnim = GetComponent<Animator>();
        soldierAnim.SetBool("isDead", false);
        playerController = player.GetComponent<PlayerController>();
    }

    private void Update()
    {
        
        if (isHit)
        {
            hitDuration -= Time.deltaTime;
            if (hitDuration <= 0f)
            {
                enemyRenderer.material.color = originalColor;
                isHit = false;
            }
        }

        float distanceFromPlayer = Mathf.Abs(Vector2.Distance(player.position, transform.position));
        Vector3 scale = transform.localScale;

        if (player.transform.position.x > transform.position.x)
        {
            spriteRenderer.flipX = true;
            if (distanceFromPlayer < attackSight)
            {
                anim.SetBool("Walking", false);
                time += Time.deltaTime;
                if (time >= attackTime)
                {
                    time = 0f;
                    Attack();
                }
            }
            else if (distanceFromPlayer < lineOfSight && distanceFromPlayer >= attackSight)
            {

                    transform.Translate(speed * Time.deltaTime, 0, 0);
                    anim.SetBool("Walking", true);
            }
            else
            {
                anim.SetBool("Walking", false);
            }
        }
        else
        {
            spriteRenderer.flipX = false;
            if (distanceFromPlayer < attackSight)
            {
                anim.SetBool("Walking", false);
                time += Time.deltaTime;
                if (time >= attackTime)
                {
                    time = 0f;
                    Attack();
                }
            }
            else if (distanceFromPlayer < lineOfSight && distanceFromPlayer >= attackSight)
            {
               
                    transform.Translate(speed * Time.deltaTime * -1, 0, 0);
                    anim.SetBool("Walking", true);
            }
            else
            {
                anim.SetBool("Walking", false);
            }
        }

        transform.localScale = scale;
    }

    public void Attack()
    {
        StartCoroutine(Co_Attack());       
    }

    IEnumerator Co_Attack()
    {
        anim.SetTrigger("Attack");
        yield return new WaitForSeconds(0.15f);
        playerController.DamageTaken();
    }
    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            enemyRenderer.material.color = hitColor;
            hitDuration = duration;
            isHit = true;
            currenthealth -= damage;
            //enemyHealthBarScript.SetHealth(currenthealth);
            if (currenthealth <= 0)
            {
                ScoreScript.instance.IncreaseScore();
                //Increase Score

                WaveSpawner.instance.EnemyKilled();

                dropItem1.DropItem();
                soldierAnim.SetBool("isDead", true);
                Destroy(this.gameObject, animDuration);
                this.gameObject.SetActive(false);
            }
        }

    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, lineOfSight);


        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackSight);
    }
}
