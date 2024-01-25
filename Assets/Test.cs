using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    private void Start()
    {
        EventManager.AddEmployeeEventListener(EventEnum.OnEmployeeHired, ListenEmployee);
        EventManager.AddEmployeeEventListener(EventEnum.OnEmployeeCanceled, ListenEmployee);
    }

    private void ListenEmployee(Employee employee)
    {
        Debug.Log(employee.Name);
    }
}
