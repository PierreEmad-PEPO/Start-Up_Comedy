using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlacementObject : MonoBehaviour
{
    public int id;

    private void OnMouseDown()
    {
       Camera.main.GetComponent<PlacementSystem>().AssignActiveObject(gameObject);
        Debug.Log("555555");
    }
    private void OnTriggerStay(Collider other)
    {
        Camera.main.GetComponent<PlacementSystem>().overlap = true;

    }
    private void OnTriggerExit(Collider other)
    {
        Camera.main.GetComponent<PlacementSystem>().overlap = false;
    }
}
