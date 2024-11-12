using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeliController1 : MonoBehaviour
{
    private SpriteRenderer sr;

    private GameObject player;
    private Animator anim;

    public Transform startingPoisition;

    public GameObject Bullet;
    public Transform bulletPosition;
    public Transform bulletPosition2;
    private double timer;
    private AudioSource audioS;

    


    private int maxHealth = 200;
    private float speed = 3f;
    private int damage = 10;

    public int currenthealth;

   

    public bool Chase = false;
    public ScoreScript scoreScript;


    // Start is called before the first frame update
    void Start()
    {
        currenthealth = maxHealth;
        player = GameObject.FindGameObjectWithTag("Player");
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        audioS = GetComponent<AudioSource>();
        
        
    }

    // Update is called once per frame
    void Update()
    {

        float distance = Vector2.Distance(transform.position, player.transform.position);
        if (Chase == true)
        {

            chase();
            timer += Time.deltaTime;
            if (timer > 2 && distance < 25)
            {


                timer = 0;
                shoot();
              

            }
        }


        else
        {
            returnOriginalState();
        }
        

        flip();

}


    
    public void chase()
    {
        anim.SetBool("idletoAtk",true);
        float step = speed * Time.deltaTime;

        
        float targetX = player.transform.position.x;
        Vector2 targetPosition = new Vector2(targetX, transform.position.y);

        transform.position = Vector2.MoveTowards(transform.position, targetPosition, step);

    }


    public void returnOriginalState()
    {
        anim.SetBool("idletoAtk", true);
        transform.position = Vector2.MoveTowards(transform.position, startingPoisition.position, speed * Time.deltaTime);
        
    }



    public void flip()
    {
        if (transform.position.x > player.transform.position.x)
        {
            sr.flipX = false;
        }
        else
        {
            sr.flipX = true;
        }
    }

    void shoot()
    {
        if(audioS != null)
        audioS.Play();

        if (transform.position.x > player.transform.position.x)
            Instantiate(Bullet, bulletPosition.position, Quaternion.identity);
        else
            Instantiate(Bullet, bulletPosition2.position, Quaternion.identity);



    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            
            currenthealth -= damage;
            
            if (currenthealth <= 0)
            {
                ScoreScript.instance.IncreaseScore();
                Destroy(this.gameObject);
            }

        }

    }
}

