using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MummyController : MonoBehaviour
{
    public Transform player;
    public SpriteRenderer spriteRenderer;
    public float speed;
    public bool flip;
    public float lineOfSight;
    public float shootingRange;

    public float shootingSpeed;
    public float attackDelay = 0.5f;

    public GameObject leftProjectileParent;
    public GameObject rightProjectileParent;
    public Poision poision;


    public Color hitColor = Color.red;
    public float hitDuration;
    public float duration;
    public Renderer enemyRenderer;
    public ScoreScript scoreScript;
    public GameManager gameManager;

    public EnemyHealthBarScript enemyHealthBarScript;

    private Color originalColor;
    private bool isHit = false;


    private Rigidbody2D mummyBody;
    private BoxCollider2D mummyCollider;
    private Animator mummyAnim;

    public int maxHealth = 100;
    public int currenthealth;


    private int damage = 10;
    private float animDuration = 1f;
    float time;

    
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        currenthealth = maxHealth;
        //enemyHealthBarScript.SetMaxHealth(maxHealth);
        originalColor = enemyRenderer.material.color;
        mummyBody = GetComponent<Rigidbody2D>();
        mummyCollider = GetComponent<BoxCollider2D>();
        mummyAnim = GetComponentInChildren<Animator>();

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

        float distanceFromPlayer = Vector2.Distance(player.position, transform.position);
        Vector3 scale = transform.localScale;

        if (player.transform.position.x > transform.position.x)
        {
            spriteRenderer.flipX = true;
            if (distanceFromPlayer < lineOfSight && distanceFromPlayer > shootingRange)
            {
                transform.Translate(speed * Time.deltaTime, 0, 0);
                mummyAnim.SetBool("Walking", true);
                mummyAnim.SetBool("Attacking", false);

            }
            else if (distanceFromPlayer <= shootingRange)
            {
                mummyAnim.SetBool("Walking", false);

                time += Time.deltaTime;
                if (time >= shootingSpeed)
                {
                    time = 0f;
                    mummyAnim.SetBool("Attacking", true);
                    StartCoroutine(Co_ShootPoision(true));
                }
            } 
        }
        else if (player.transform.position.x < transform.position.x)
        {
            spriteRenderer.flipX = false;
            if (distanceFromPlayer < lineOfSight && distanceFromPlayer > shootingRange)
            {
                transform.Translate(speed * Time.deltaTime * -1, 0, 0);
                mummyAnim.SetBool("Walking", true);
                mummyAnim.SetBool("Attacking", false);

            }
            else if (distanceFromPlayer <= shootingRange)
            {
                mummyAnim.SetBool("Walking", false);

                time += Time.deltaTime;
                if (time >= shootingSpeed)
                {
                    time = 0f;
                    mummyAnim.SetBool("Attacking", true);
                    StartCoroutine(Co_ShootPoision(false));
                }
            }
        }
        else
        {
            spriteRenderer.flipX = true;
        }

        transform.localScale = scale;
    }

    IEnumerator Co_ShootPoision(bool value)
    {
        yield return new WaitForSeconds(attackDelay);
        Poision poisionObj;
        if (value)
            poisionObj = Instantiate(poision, leftProjectileParent.transform.position, Quaternion.identity);
        else
            poisionObj = Instantiate(poision, rightProjectileParent.transform.position, Quaternion.identity);

        poisionObj.GetComponent<SpriteRenderer>().flipX = value ? true : false;

        poisionObj.ShootBullet(value);
        yield return new WaitForSeconds(1f);
        mummyAnim.SetBool("Attacking", false);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            WaveSpawner.instance.EnemyKilled();

            enemyRenderer.material.color = hitColor;
            hitDuration = duration;
            isHit = true;
            currenthealth -= damage;
            //enemyHealthBarScript.SetHealth(currenthealth);
            if (currenthealth <= 0)
            {
                mummyAnim.SetBool("isDead", true);
                Destroy(this.gameObject, animDuration);
            }

        }

    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, lineOfSight);
        Gizmos.DrawWireSphere(transform.position, shootingRange);
    }
}
