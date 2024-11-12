using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rShobu : MonoBehaviour
{
    public GameObject wing;
    public GameObject body;
    public GameObject bomb;
    public Transform bombPosition;
    public ScoreScript scoreScript;

    private GameObject player;

    private SpriteRenderer wingSprite;
    private SpriteRenderer bodySprite;
    private BoxCollider2D collider;

    private Animator wingAnimator;
    private Animator bodyAnimator;

    private float speed = 0.5f;
    private float timer;
    private Animator anim;

    private int maxHealth = 300;
    private int  currenthealth;
    private int damage = 10;
    // Start is called before the first frame update
    void Start()
    {
        currenthealth = maxHealth;
        wingAnimator = wing.GetComponent<Animator>();
        bodyAnimator = body.GetComponent<Animator>();
        collider = body.GetComponent<BoxCollider2D>();
        wingSprite = wing.GetComponent<SpriteRenderer>();
        bodySprite = body.GetComponent<SpriteRenderer>();
        
        


        player = GameObject.FindGameObjectWithTag("Player");


        wingAnimator.Play("IDLE");
        bodyAnimator.Play("IDLE");
    }

    // Update is called once per frame
    void Update()
    {
        
        timer += Time.deltaTime;
        chase();
        flip();
        if (timer > 2)
        {
            fireBomb();
            timer = 0;
        }
       
    }


    public void flip()
    {
        float wingSpeed = 10f;
        if (player.transform.position.x < wing.transform.position.x)
        {
            Debug.Log("dont flipX");
            wingSprite.flipX = false;
            bodySprite.flipX = false;
           
        }
        else
        {
            Debug.Log("flipX");
            Debug.Log(Time.deltaTime);
            wingSprite.flipX = true;
            bodySprite.flipX = true;
        }
    }
    public void chase()
    {

        float step = speed * Time.deltaTime;

        // Determine the direction to the player
        float distanceThreshold = 0.1f; // Adjust this threshold as needed

        if (Mathf.Abs(player.transform.position.x - wing.transform.position.x) > distanceThreshold)
        {
            float targetX = player.transform.position.x;
            Vector2 targetPosition = new Vector2(targetX, wing.transform.position.y);

            // Smoothly move towards the target position
            wing.transform.position = Vector2.Lerp(wing.transform.position, targetPosition, step);
        }
    }


        public void fireBomb()
        {
            Instantiate(bomb, bombPosition.position, Quaternion.identity);
        }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            Debug.Log("collision");
            currenthealth -= damage;

            if (currenthealth <= 0)
            {
                ScoreScript.instance.IncreaseScore();
                Destroy(this.gameObject);
                Destroy(wing.gameObject);
                Destroy(body.gameObject);
            }

        }

    }


}

