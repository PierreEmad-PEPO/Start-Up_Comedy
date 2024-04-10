using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ObjectsDatabaseSO : ScriptableObject
{
    public List<Item> items;
}

[Serializable]
public class Item
{
    [field: SerializeField] public string name { get; private set; }
    [field: SerializeField] public int id { get; private set; }
    [field: SerializeField] public int cost { get; private set; }
    [field: SerializeField] public GameObject prefab { get; private set; }
}
