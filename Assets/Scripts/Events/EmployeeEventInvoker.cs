using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EmployeeEventInvoker : MonoBehaviour 
{
    private UnityEvent<Employee> unityEvent = new UnityEvent<Employee>();

    public void AddListener(UnityAction<Employee> unityAction)
    {
        unityEvent.AddListener(unityAction);
    }

    public void RemoveListener(UnityAction<Employee> unityAction)
    {
        unityEvent.RemoveListener(unityAction);
    }

    public void Invoke(Employee employee)
    {
        unityEvent.Invoke(employee);
    }
}
