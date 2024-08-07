using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacementHelper : MonoBehaviour
{
    [SerializeField] Camera mainCamera;
    [SerializeField] LayerMask placementObjectLayerMask;
    [SerializeField] PlacementSystem placementSystem;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonUp(1) && ! WindowManager.isThereWindowOpen())
        {
            Vector3 mousePosition = Input.mousePosition;
            mousePosition.z = mainCamera.nearClipPlane;
            Ray ray = mainCamera.ScreenPointToRay(mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 1000, placementObjectLayerMask))
            {
                GameObject hitObject = hit.transform.gameObject;
                if (hitObject.CompareTag("Office"))
                {
                    Office office = hitObject.GetComponent<Office>();
                    if (office.Employee != null)
                    {
                        WindowManager.GetWindowGameObject(WindowName.Office).
                            GetComponent<OfficeWindow>().ViewData(office.Employee, hitObject);
                        WindowManager.OpenWindow(WindowName.Office);
                    }
                    else
                    {
                        WindowManager.GetWindowGameObject(WindowName.Office).
                            GetComponent<OfficeWindow>().ViewLWaitingList(hitObject);
                        WindowManager.OpenWindow(WindowName.Office);
                    }

                }
                else
                {
                    placementSystem.AssignActiveObject(hitObject);
                }

            }
        }
    }
}
