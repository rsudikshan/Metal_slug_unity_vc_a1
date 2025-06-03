using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hbullet : MonoBehaviour
{
    public GameObject player;
    private Rigidbody2D rb;
    private float force = 8f;
    private float timer;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        Vector3 direction = player.transform.position - transform.position;
        rb.velocity = new Vector2(direction.x, direction.y).normalized * force;
        
       
    }

    // Update is called once per frame
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerController Player = collision.gameObject.GetComponent<PlayerController>();
            Player.DamageTaken();
            Destroy(this.gameObject);
        }
    }
}
