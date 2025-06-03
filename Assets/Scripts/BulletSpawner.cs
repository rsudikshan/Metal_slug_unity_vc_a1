using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class BulletSpawner : MonoBehaviour
{
    public Transform shootingPoint;
    public GameObject bullet;
    public PlayerController playerController;

    [SerializeField] private AudioSource normalShotEffect;

    private void Start()
    {
        normalShotEffect = GameObject.FindGameObjectWithTag("BulletSound").GetComponent<AudioSource>();
        playerController = GetComponent<PlayerController>();
    }
    private void Update()
    {
        
            if (Input.GetMouseButtonDown(0))
            {
                normalShotEffect.Play();
                Spawn();

            }
        
        
            
        
    }

    private void Spawn()
    {
        Instantiate(bullet, shootingPoint.position, transform.rotation);
    }


}
