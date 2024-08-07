using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class Project
{
    #region Fields
    private string name;
    private ProjectSpecialization specialization;
    private int deadline;
    private int price;
    private int penalClause;
    private int requiredTechnicalSkills;
    private int requiredDesignSkills;
    private int totalAssignedTechnicalSkills;
    private int totalAssignedDesignSkills;
    private int maxRequiredTechnicalSkills;
    private int maxRequiredDesignSkills;
    public int highValue;

    #endregion

    #region Props
    public string Name { get { return name; } }
    public ProjectSpecialization Specialization { get { return specialization; } }
    public int Deadline { get { return deadline; } }
    public int Price { get { return price; } }
    public int PenalClause { get { return penalClause; } }
    public int RequiredTechnicalSkills { get {  return requiredTechnicalSkills; } }
    public int RequiredDesignSkills {  get { return requiredDesignSkills; } }
    public bool IsDone { get { return (requiredTechnicalSkills <= 0 &&  requiredDesignSkills <= 0); } }
    public bool IsDeadlineEnd { get { return deadline <= 0; } }
    public float TechnicalProgress { get { return Mathf.Min(100f, (float)(maxRequiredTechnicalSkills - requiredTechnicalSkills) / maxRequiredTechnicalSkills * 100f); } }
    public float DesignProgress { get { return Mathf.Min(100f, (float)(maxRequiredDesignSkills - requiredDesignSkills) / maxRequiredDesignSkills * 100f); } }
    public int MaxRequiredTechnicalSkills { get { return maxRequiredTechnicalSkills; } }
    public int MaxRequiredDesignSkills { get { return maxRequiredDesignSkills; } }
    #endregion

    #region Methods
    public void Init(string name, ProjectSpecialization specialization, int deadline, int price, 
                   int penalClause, int requiredTechnicalSkills, int requiredDesignSkills)
    {
        this.name = name;
        this.specialization = specialization;
        this.deadline = deadline;
        this.price = price;
        this.penalClause = penalClause;
        this.requiredTechnicalSkills = requiredTechnicalSkills;
        this.requiredDesignSkills = requiredDesignSkills;
        this.totalAssignedTechnicalSkills = 0;
        this.totalAssignedDesignSkills = 0;
        this.maxRequiredTechnicalSkills = requiredTechnicalSkills;
        this.maxRequiredDesignSkills = requiredDesignSkills;
    }

    public void AssignEmployee(int technicalSkills, int  designSkills)
    {
        totalAssignedTechnicalSkills += technicalSkills;
        totalAssignedDesignSkills += designSkills;
    }

    public void DismissEmployee(int technicalSkills, int designSkills)
    {
        totalAssignedTechnicalSkills -= technicalSkills;
        totalAssignedDesignSkills -= designSkills;
    }

    // Update is called once per frame
    public void UpdateProject()
    {
        if (requiredTechnicalSkills > 0f)
            requiredTechnicalSkills -= totalAssignedTechnicalSkills;
        if (requiredDesignSkills > 0f)
            requiredDesignSkills -= totalAssignedDesignSkills;
        if (deadline > 0f)
            deadline -= 1;


    }
    #endregion
}
