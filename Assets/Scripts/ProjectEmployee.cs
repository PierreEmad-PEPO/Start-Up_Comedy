using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectEmployee : Employee
{
    private int technicalSkills;
    private int designSkills;
    private Project assignedProject;

    public void Init(string name, int minSalary, ProjectSpecialization _specialization, int _technicalSkills, int _designSkills)
    {
        base.Init(name, (EmployeeSpecialization)((int)_specialization), minSalary);
        technicalSkills = _technicalSkills;
        designSkills = _designSkills;
    }

    public void AssignProject(Project _assignedProject)
    {
        assignedProject = _assignedProject;
    }
    public override void UpgradeSkills(int increasePercentage)
    {
        technicalSkills += increasePercentage;
        designSkills += increasePercentage;
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
