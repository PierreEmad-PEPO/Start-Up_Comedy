using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Search;
using UnityEngine;

[CreateAssetMenu]
public class ObjectsDatabaseSO : ScriptableObject
{
    public List<Item> items;
    public void Initialize()
    {
        for (int i = 0; i < items.Count; i++) 
        {
            items[i].id = i;
        }
    }
}

[Serializable]
public class Item
{
    [field: SerializeField] public string name { get; private set; }
    [field: SerializeField] public int id { get; set; }
    [field: SerializeField] public int cost { get; private set; }
    [field: SerializeField] public GameObject prefab { get; private set; }
    [field: SerializeField] public Sprite img { get; private set; }
}
