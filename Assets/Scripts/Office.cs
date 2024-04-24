using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Office : MonoBehaviour
{
    int baseID;
    int id;
    static int idCount = 0;
    Employee employee;
    Vector3 position = Vector3.zero;
    Quaternion rotation = Quaternion.identity;

    public int ID {  get { return id; } }
    public Employee Employee { get { return employee; } }

    public void Init(int baseID)
    {
        this.baseID = baseID;
        id = idCount++;
    }

    public void setEmployee(Employee employee)
    {
        this.employee = employee;
        employee.Set(id);
        GameManager.Offices.Add(this);
    }

    public void UnsetEmployee()
    {
        employee = null;
        Destroy(transform.GetChild(0).GetChild(0).gameObject);
    }
    public void SaveOfficeState() 
    {
        position = transform.position;
        rotation = transform.rotation;
    }
    
}
