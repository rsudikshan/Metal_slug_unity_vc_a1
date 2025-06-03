using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DropItem : MonoBehaviour
{
    
    private CircleCollider2D dropCollider;

    public GameObject m_gmanager;
    public ScoreScript scc;

    public EnemyController enemyController;
    //public GameManager m_manager;
    

    private void Awake()
    {
        
    }
    private void Start()
    {
        //this.gameObject.SetActive(false);
        //m_manager = m_anager.GetComponent<GameManager>();
        scc = GameObject.FindGameObjectWithTag("ScoreText"). GetComponent<ScoreScript>();
        dropCollider = GetComponent<CircleCollider2D>();
        //enemyController = GameObject.FindGameObjectWithTag("Enemy").GetComponent<EnemyController>();

    }

    private void Update()
    {
        /*if(enemyController == null)
        {
            this.gameObject.SetActive(true);
        }*/
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            //m_manager.IncreaseScore();
            
            Destroy(this.gameObject);

        }
    }

    private void OnDestroy()
    {
        //this.gameObject.SetActive(false);
        scc.IncreaseScore();
    }

}