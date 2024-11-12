using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Poision : MonoBehaviour
{
    public float speed = 2.5f;
    Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void ShootBullet(bool shootRight)
    {
        if(shootRight)
            rb.AddForce(transform.right * speed, ForceMode2D.Impulse);
        else
            rb.AddForce(-transform.right * speed, ForceMode2D.Impulse);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            PlayerController Player = collision.gameObject.GetComponent<PlayerController>();
            Player.DamageTaken();
            Destroy(this.gameObject);
        }
    }
}
