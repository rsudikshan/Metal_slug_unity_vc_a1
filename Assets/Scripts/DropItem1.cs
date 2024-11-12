using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class DropItem1 : MonoBehaviour
{

    public Transform soldier;
    public DropItemTable dropItemTable;

    //public GameObject randomDrop;
    public float dropChance;

    private bool hasSpawnedItem = false;

    private void Start()
    {
    }

   

    public void DropItem()
    {
        float dropRoll = Random.Range(0f, 100f);
        float currentChance = 0f;

        foreach (var dropItem in dropItemTable.dropItems)
        {
            currentChance += dropItem.dropChance;

            if (dropRoll <= currentChance)
            {
                GameObject newItem = Instantiate(dropItem.itemPrefab, transform.position, Quaternion.identity);
                break;
            }
        }

        hasSpawnedItem = true;
    }
}
    


