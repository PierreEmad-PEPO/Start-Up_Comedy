using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Object = UnityEngine.Object;

public class PlacementSystem : MonoBehaviour
{
    [SerializeField] private ObjectsDatabaseSO dataBase;

    [SerializeField] public Camera mainCamera;

    [SerializeField] public LayerMask placementLayerMask;

    GameObject activeObject = null;
    BoxCollider activeBoxCollider = null;


    private BoxCollider[] colliders = new BoxCollider[10];
    [SerializeField] LayerMask placementObjectLyerMask;


    private void Awake()
    {
        InputManager.placementLayerMask = placementLayerMask;
        InputManager.mainCamera = mainCamera;
        gameObject.SetActive(false);
    }

    public void AssignActiveObject(GameObject _gameObject)
    {
        gameObject.SetActive(true);
        activeObject = Instantiate(_gameObject); // for now
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
        if (Physics.OverlapBoxNonAlloc(activeBoxCollider.transform.position + activeBoxCollider.center, activeBoxCollider.size / 2,
            colliders, activeBoxCollider.transform.rotation, placementObjectLyerMask) > 0)
        {
            // red light
            Debug.Log(colliders.Length);
        }
        else if (Input.GetMouseButtonDown(1))
        {
            activeObject.layer = 6;
            gameObject.SetActive(false);
        }
    }


    public void intiobject(int id)
    {
        GameObject _object = Instantiate(dataBase.items[id].prefab);
        AssignActiveObject(_object);

    }

}