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
    [SerializeField] LayerMask placementObjectLayerMask;

    public ObjectsDatabaseSO DatabaseSO { get { return dataBase; } }


    private void Start()
    {
        dataBase.Initialize();
        gameObject.SetActive(false);
    }

    public void AssignActiveObject(GameObject _gameObject)
    {
        if (activeObject == null)
        {
            gameObject.SetActive(true);
            activeObject = _gameObject;
            activeBoxCollider = activeObject.GetComponent<BoxCollider>();
            activeObject.layer = 0;
        }
    }



    private void Update()
    {
        // move object
        Vector3 mousePos = InputManager.GetMousePosition();
        Vector3 position = Vector3.MoveTowards(activeObject.transform.position, mousePos, 10 * Time.deltaTime);
        position.y = activeObject.transform.position.y;
        activeObject.transform.position = position;

        // rotate object
        if (Input.GetKeyDown(KeyCode.R))
        {
            activeObject.transform.Rotate(Vector3.up * 45);
        }

        // is collide
        Vector3 po = Vector3.zero;
        po.y = activeBoxCollider.center.y;
        
        if (Physics.OverlapBoxNonAlloc(activeBoxCollider.transform.position + po, (activeBoxCollider.size) / 2,
            colliders, activeBoxCollider.transform.rotation, placementObjectLayerMask) > 0)
        {
            // red light
            if (Input.GetMouseButtonDown(0))
                WindowManager.ShowNotificationAlert("You can't place it here.");
        }
        else if (Input.GetMouseButtonDown(0))
        {
            if (Physics.OverlapBoxNonAlloc(InputManager.GetMousePosition() + po, (activeBoxCollider.size) / 2,
            colliders, activeBoxCollider.transform.rotation, placementObjectLayerMask) > 0)
            {

            }
            else
            {
                Vector3 pos = InputManager.GetMousePosition();
                pos.y = activeObject.transform.position.y;
                activeObject.transform.position = pos;
                activeObject.layer = 6;
                gameObject.SetActive(false);
                activeObject = null;
            }
        }
 
    }


    public void InstantiateObject(int id)
    {
        GameObject _object = Instantiate(dataBase.items[id].prefab);
        _object.AddComponent<Office>().Init(id);
        AssignActiveObject(_object);

    }

}