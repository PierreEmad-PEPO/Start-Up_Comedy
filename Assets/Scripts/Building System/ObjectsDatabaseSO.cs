using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;

[CreateAssetMenu]
public class ObjectsDatabaseSO : ScriptableObject
{
    public List<Item> items;
}

[Serializable]
public class Item
{
    static int idGenerator = 0;
    [field: SerializeField] public string name { get; private set; }
    public int id { get; private set; }
    [field: SerializeField] public int cost { get; private set; }
    [field: SerializeField] public GameObject prefab { get; private set; }
    [field: SerializeField] public Sprite img { get; private set; }
    [field: SerializeField] public int categoryNumber { get; private set; }


    public Item()
    {
        id = idGenerator++;
    }
}
