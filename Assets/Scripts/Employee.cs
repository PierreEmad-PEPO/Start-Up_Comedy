using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Employee
{
    #region Feilds

    protected string name;
    protected EmployeeSpecialization specialization;
    protected int salary;
    protected int minSalary;

    #endregion

    #region Props

    public string Name { get { return name; } }
    public EmployeeSpecialization Specialization { get { return specialization; } }
    public int Salary { get { return salary; } set { salary = value; } }
    public int MinSalary { get { return minSalary; } }

    #endregion

    #region public Methods
    public void Init(string name, EmployeeSpecialization specialization, int minSalary)
    {
        this.name = name;
        this.specialization = specialization;
        this.minSalary = minSalary;
    }

    virtual public void UpgradeSkills(int increasePercentage) { }
    #endregion
}
