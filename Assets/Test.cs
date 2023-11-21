using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    private void Start()
    {
        EventManager.AddGameObjectEventListener(EventEnum.OnProjectGenerated, ListenProject);
        EventManager.AddGameObjectEventListener(EventEnum.OnEmployeeGenerated, ListenEmployee);
    }

    private void ListenProject(GameObject project)
    {
        Debug.Log(project);
    }
    private void ListenEmployee(GameObject employee) 
    {
        Debug.Log(employee);
    }
}
