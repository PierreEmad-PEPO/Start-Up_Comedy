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
    bool isSet = false;
    int setID;

    #endregion

    #region Props

    public string Name { get { return name; } }
    public EmployeeSpecialization Specialization { get { return specialization; } }
    public int Salary { get { return salary; } set { salary = value; } }
    public int MinSalary { get { return minSalary; } }
    public int SetID {  get { return setID; } }
    public bool IsSet { get { return isSet; } }

    #endregion

    #region public Methods
    public void Init(string name, EmployeeSpecialization specialization, int minSalary)
    {
        this.name = name;
        this.specialization = specialization;
        this.minSalary = minSalary;
    }

    virtual public void UpgradeSkills(int increasePercentage) { }

    public virtual void HireEmployee() { GameManager.StartUp.PayMoney(salary / 2); }

    public virtual void Fire()
    {
        GameManager.StartUp.PayMoney(salary / 2);
        GameManager.HiredEmployee.Remove(this);
    }

    public void Set(int setID)
    {
        this.setID = setID;
        isSet = true;
    }
    #endregion
}
