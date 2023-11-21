using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class Project : MonoBehaviour
{
    #region Fields
    private new string name;
    private ProjectSpecialization specialization;
    private float deadline;
    private int price;
    private int penalClause;
    private float requiredTechnicalSkills;
    private float requiredDesignSkills;
    private float totalAssignedTechnicalSkills;
    private float totalAssignedDesignSkills;
    private UnityEvent<int> OnProjectDone;
    private UnityEvent<int> OnDeadlineEnd;
    #endregion

    #region Props
    public string Name { get { return name; } }
    public ProjectSpecialization Specialization { get { return specialization; } }
    public float Deadline { get { return deadline; } }
    public int Price { get { return price; } }
    public int PenalClause { get { return penalClause; } }
    public float RequiredTechnicalSkills { get {  return requiredTechnicalSkills; } }
    public float RequiredDesignSkills {  get { return requiredDesignSkills; } }
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

    private void Start()
    {
        // Assign Events
    }

    // Update is called once per frame
    private void Update()
    {
        requiredTechnicalSkills -= totalAssignedTechnicalSkills * Time.deltaTime;
        requiredDesignSkills -= totalAssignedDesignSkills * Time.deltaTime;
        deadline -= Time.deltaTime;

        if (requiredTechnicalSkills <= 0 && requiredDesignSkills <= 0)
        {
            OnProjectDone.Invoke(price);
        }

        if (deadline <= 0)
        {
            OnDeadlineEnd.Invoke(penalClause);
        }
    }

    public void AddOnProjectDoneListener(UnityAction<int> unityAction)
    {
        OnProjectDone.AddListener(unityAction);
    }

    public void AddOnDeadlineEndListener(UnityAction<int> unityAction) 
    {
        OnDeadlineEnd.AddListener(unityAction);
    }
    #endregion
}
