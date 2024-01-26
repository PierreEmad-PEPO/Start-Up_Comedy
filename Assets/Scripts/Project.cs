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
    private float deadline;
    private int price;
    private int penalClause;
    private float requiredTechnicalSkills;
    private float requiredDesignSkills;
    private float totalAssignedTechnicalSkills;
    private float totalAssignedDesignSkills;
    #endregion

    #region Props
    public string Name { get { return name; } }
    public ProjectSpecialization Specialization { get { return specialization; } }
    public float Deadline { get { return deadline; } }
    public int Price { get { return price; } }
    public int PenalClause { get { return penalClause; } }
    public float RequiredTechnicalSkills { get {  return requiredTechnicalSkills; } }
    public float RequiredDesignSkills {  get { return requiredDesignSkills; } }
    public bool IsDone { get { return (requiredTechnicalSkills <= 0 &&  requiredDesignSkills <= 0); } }
    public bool IsDeadlineEnd { get { return deadline <= 0; } }
    #endregion

    #region Methods
    public void Init(string name, ProjectSpecialization specialization, float deadline, int price, 
                   int penalClause, float requiredTechnicalSkills, float requiredDesignSkills)
    {
        this.name = name;
        this.specialization = specialization;
        this.deadline = deadline;
        this.price = price;
        this.penalClause = penalClause;
        this.requiredTechnicalSkills = requiredTechnicalSkills;
        this.requiredDesignSkills = requiredDesignSkills;
        this.totalAssignedTechnicalSkills = 0f;
        this.totalAssignedDesignSkills = 0f;
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
