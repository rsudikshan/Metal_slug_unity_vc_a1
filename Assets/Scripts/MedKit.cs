using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class MedKit : MonoBehaviour
{

    //private Animator medAnim;

    public PlayerController playerController;

    

    private void Start()
    {
        //medAnim = GetComponent<Animator>();
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    private void Update()
    {
        //medAnim.Play("HealthAnimation");
    }

    public void PickUpAnimation()
    {
        //medAnim.SetBool("HealthPickUp", true);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            
            Destroy(this.gameObject);

        }
    }

    private void OnDestroy()
    {
        playerController.Health();

    }





}


