using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlacementSystem : MonoBehaviour
{
    [SerializeField] private ObjectsDatabaseSO dataBase;



    GameObject activeObject = null;
    BoxCollider activeBoxCollider = null;


    private BoxCollider[] colliders = new BoxCollider[10];
    [SerializeField] Camera mainCamera;
    [SerializeField] LayerMask placementObjectLyerMask;

    public ObjectsDatabaseSO DatabaseSO { get { return dataBase; } }


    private void Start()
    {
        dataBase.Initialize();
        gameObject.SetActive(false);
    }

    public void AssignActiveObject(GameObject _gameObject)
    {
        if (activeObject != null)
            activeObject.layer = 6;
        gameObject.SetActive(true);
        activeObject = _gameObject;
        activeBoxCollider = activeObject.GetComponent<BoxCollider>();
        activeObject.layer = 0;
    }



    private void Update()
    {
        // move object
        Vector3 mousPos = InputManager.GetMousePosition();
        Vector3 position = Vector3.MoveTowards(activeObject.transform.position, mousPos, 10 * Time.deltaTime);
        position.y = activeObject.transform.position.y;
        activeObject.transform.position = position;

        // rotate object
        if (Input.GetKeyDown(KeyCode.R))
        {
            activeObject.transform.Rotate(Vector3.up * 45);
        }

        // is collide
        if (Physics.OverlapBoxNonAlloc(activeBoxCollider.transform.position + activeBoxCollider.center, (activeBoxCollider.size) / 2,
            colliders, activeBoxCollider.transform.rotation, placementObjectLyerMask) > 0)
        {
            // red light
        }
        else if (Input.GetMouseButtonDown(0))
        {
            activeObject.layer = 6;
            gameObject.SetActive(false);
        }
        
    }


    public void intiobject(int id)
    {
        GameObject _object = Instantiate(dataBase.items[id].prefab);
        _object.AddComponent<Office>().Init(id);
        AssignActiveObject(_object);

    }

}