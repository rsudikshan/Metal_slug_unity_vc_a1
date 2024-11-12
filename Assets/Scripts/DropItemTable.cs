using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DropTableItem
{
    public GameObject itemPrefab;
    public float dropChance;
}

[CreateAssetMenu(fileName = "New Drop Table", menuName = "Drop Table")]
public class DropItemTable : ScriptableObject
{
    public DropTableItem[] dropItems;
}
