using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlacementObject : MonoBehaviour
{

    private void OnMouseDown()
    {
       PlacementSystem.AssignActiveObject(gameObject);
    }
    private void OnTriggerStay(Collider other)
    {
       PlacementSystem.overlap = true;

    }
    private void OnTriggerExit(Collider other)
    {
        PlacementSystem.overlap = false;
    }
}
