using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectEmployee : Employee
{
    private int technicalSkills;
    private int designSkills;
    private Project assignedProject;

    public int TechicalSkills { get{ return technicalSkills; }}
    public int DesignSkills { get { return designSkills; }}

    public Project AssignedProject { get { return assignedProject; }}
    public void Init(string name, ProjectSpecialization _specialization, int minSalary, int _technicalSkills, int _designSkills)
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
        technicalSkills += technicalSkills * increasePercentage/100;
        designSkills += designSkills * increasePercentage/100;
    }
}
