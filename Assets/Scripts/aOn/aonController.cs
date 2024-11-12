using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class aonController : MonoBehaviour
{

    private Rigidbody2D rb;
    private Animator anim;
    private GameObject player;
    
    private float timer;



    public GameObject Bullet;
    public Transform bulletPosition;
    public Transform bulletPosition2;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        float distance = Vector2.Distance(transform.position, player.transform.position);
        if (distance < 8 &&  timer > 0.5 )
        {

            timer = 0;
            
            shoot();
            anim.SetBool("laughtoShoot", true);


        }
        else if(distance> 5)
            anim.SetBool("laughtoShoot", false);

    }


    public void shoot()
    {

        if (transform.position.x > player.transform.position.x )       
            Instantiate(Bullet, bulletPosition.position, Quaternion.identity);
       
        
        else if (transform.position.x < player.transform.position.x )
            Instantiate(Bullet, bulletPosition2.position, Quaternion.identity);
         
    }

    
}
