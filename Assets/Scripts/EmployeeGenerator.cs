using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EmployeeGenerator : MonoBehaviour
{
    #region Fields
    private string[] employeesNames = { "Boda", "PepoElMfsh5WeElTa3ban", "Q7tanyEl5wel", "Martin", "AlmrkanyElHwyan"};

    private Timer timer;
    private const float timerDuration = 2f;    // For Now

    private EmployeeSpecialization specialization = EmployeeSpecialization.Games;
    private int hrSkill = 100; // From Game Manager

    private int maxSkillFactor = 5;     // For Now
    private int minSkillFactor = 3;     // For Now
    private int maxMinSalaryFactor = 500; // For Now
    private int minMinSalaryFactor = 3; // For Now

    private float maxTechnicaSkillFactor = 3; // For Now
    private float maxDesignSkillFactor = 3;   // For Now
    private float maxMarktingSkillFactor = 3; // For Now
    private float maxHrSkillFacotor = 3;

    private EmployeeEventInvoker onEmployeeGenerated;

    public Timer Timer { get { return timer; } }
    #endregion

    #region Unity Methods

    private void Awake()
    {
        timer = gameObject.AddComponent<Timer>();
        timer.Init(timerDuration, GenerateEmployee);
    }

    private void Start()
    {
        onEmployeeGenerated = gameObject.AddComponent<EmployeeEventInvoker>();
        EventManager.AddEmployeeEventInvoker(EventEnum.OnEmployeeGenerated, onEmployeeGenerated);

        EndGeneration();
    }

    #endregion

    #region Public Methods
    public void StartGeneration(EmployeeSpecialization _specialization)
    {
        timer.Run();
        specialization = _specialization;
        hrSkill = GameManager.StartUp.TotalHrSkills; // from game manager;
        Debug.Log("HRSKILLS " + hrSkill);

    }

    public void EndGeneration()
    {
        timer.Pause();
        
    }

    private void GenerateEmployee()
    {
        string employeeName = employeesNames[RandomGenerator.NextInt(0, employeesNames.Length)];
        int  employeeMinSalary = RandomGenerator.NextInt(minMinSalaryFactor, maxMinSalaryFactor);

        Employee employee = new Employee();

        switch (specialization)
        {
            case EmployeeSpecialization.Games:
                employee = new ProjectEmployee();
                int technicalGamesSkills = (int)(hrSkill * RandomGenerator.NextFloat(.5f, maxTechnicaSkillFactor));
                int designGamesSkills = (int)(hrSkill * RandomGenerator.NextFloat(.5f, maxDesignSkillFactor));
                (employee as ProjectEmployee).Init(employeeName, ProjectSpecialization.Games, employeeMinSalary, technicalGamesSkills, designGamesSkills);
                break;
            case EmployeeSpecialization.Mobile:
                employee = new ProjectEmployee();
                int technicalMobileSkills = (int)(hrSkill * RandomGenerator.NextFloat(.5f, maxTechnicaSkillFactor));
                int designMobileSkills = (int)(hrSkill * RandomGenerator.NextFloat(.5f, maxDesignSkillFactor));
                (employee as ProjectEmployee).Init(employeeName, ProjectSpecialization.Mobile, employeeMinSalary, technicalMobileSkills, designMobileSkills);
                break;
            case EmployeeSpecialization.Web:
                employee = new ProjectEmployee();
                int technicalWebSkills = (int)(hrSkill * RandomGenerator.NextFloat(.5f, maxTechnicaSkillFactor));
                int designWebSkills = (int)(hrSkill * RandomGenerator.NextFloat(.5f, maxDesignSkillFactor));
                (employee as ProjectEmployee).Init(employeeName, ProjectSpecialization.Web, employeeMinSalary, technicalWebSkills, designWebSkills);
                break;
            case EmployeeSpecialization.Marketing:
                employee = new MarketingEmployee();
                int marketingSkills = (int)(hrSkill * RandomGenerator.NextFloat(.5f, maxMarktingSkillFactor));
                (employee as MarketingEmployee).Init(employeeName, employeeMinSalary ,marketingSkills );
                break;
            case EmployeeSpecialization.DataAnalysis:
                employee = new DataAnalysisEmployee();
                (employee as DataAnalysisEmployee).Init(employeeName, EmployeeSpecialization.DataAnalysis ,employeeMinSalary);
                break;
            case EmployeeSpecialization.HR:
                employee = new HrEmployee();
                int hrSkills = (int)(hrSkill * RandomGenerator.NextFloat(.5f,maxHrSkillFacotor));
                (employee as HrEmployee).Init(employeeName, employeeMinSalary, hrSkills);
                break;
        }

        timer.Run();
        onEmployeeGenerated.Invoke(employee);
    }
    #endregion

}
