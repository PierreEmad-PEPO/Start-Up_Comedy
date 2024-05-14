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

    public override void HireEmployee()
    {
        base.HireEmployee();
        GameManager.StartUp.AddTotalHrSkills(hrSkill/4);
    }

    public override void Fire()
    {
        base.Fire();
        GameManager.StartUp.AddTotalHrSkills(-(hrSkill/3));
    }
}
