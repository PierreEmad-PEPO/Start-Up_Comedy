using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarketingEmployee : Employee
{
    private int marketingSkill;

    public int MarketingSkill { get { return marketingSkill; } }

    public void Init(string name, int minSalary, int _marketingSkill)
    {
        base.Init(name, EmployeeSpecialization.Marketing, minSalary);
        marketingSkill = _marketingSkill;
    }
}
