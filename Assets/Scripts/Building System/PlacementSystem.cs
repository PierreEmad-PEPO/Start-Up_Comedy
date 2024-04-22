using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacementSystem : MonoBehaviour
{
      [SerializeField] private ObjectsDatabaseSO dataBase;
      
      [SerializeField] public  Camera mainCamera;

      [SerializeField] public  LayerMask placementLayerMask;
      
      static GameObject activeObject = null;
      

      public static bool overlap = false;

    public ObjectsDatabaseSO DatabaseSO { get { return dataBase; } }
      
      private void Awake()
      {
            InputManager.placementLayerMask = placementLayerMask;
            InputManager.mainCamera = mainCamera;
      }

    private void Start()
    {
        dataBase.Initialize();
    }

    public static void AssignActiveObject(GameObject _gameObject)
      {
            activeObject = _gameObject;
      }



    private void Update()
    {
        if (Input.GetMouseButtonDown(1) && overlap == false)
        {
            activeObject = null;
        }

        if (activeObject != null)
        {
            Vector3 mousPos = InputManager.GetMousePosition();
            Vector3 position = Vector3.MoveTowards(activeObject.transform.position, mousPos, 10 * Time.deltaTime);
            position.y = activeObject.transform.position.y;
            activeObject.transform.position = position;
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            activeObject.transform.Rotate(Vector3.up * 45);

        }
    }
      
      
     public void intiobject(int id)
     {
        Debug.Log(id);
        GameObject _object = Instantiate(dataBase.items[id].prefab);
        AssignActiveObject(_object);

     }
     
}