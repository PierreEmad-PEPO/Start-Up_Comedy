using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HrEmployee : Employee
{
    private int hrSkill;

    public int HrSkill { get { return hrSkill; } }

    public void Init(string name, int minSalary, int _hrSkill)
    {
        base.Init(name, EmployeeSpecialization.HR, minSalary);
        hrSkill = _hrSkill;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
