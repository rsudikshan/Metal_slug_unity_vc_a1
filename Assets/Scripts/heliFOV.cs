using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class heliFOV : MonoBehaviour
{
    public HeliController1[] enemies;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) {
            foreach (HeliController1 enemy in enemies)
            {
                enemy.Chase = true;
        }
        }
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            foreach (HeliController1 enemy in enemies)
            {
                enemy.Chase = false;
            }
        }
    }
}
