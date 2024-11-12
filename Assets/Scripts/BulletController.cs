using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BulletController : MonoBehaviour
{
    
    private CircleCollider2D circleCollider;
    public Rigidbody2D bulletBody;
    public EnemyController Enemy;
    public float bulletSpeed;

    private void Start()
    {
        Enemy = GetComponent<EnemyController>();
        bulletBody.velocity = transform.right * bulletSpeed * 5;
        StartCoroutine(Co_DestoryAfterTime());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if(collision.tag == "Enemy")
        {
            Destroy(this.gameObject);
        }
    }

    IEnumerator Co_DestoryAfterTime()
    {
        yield return new WaitForSeconds(6f);
        Destroy(this.gameObject);
    }

}
