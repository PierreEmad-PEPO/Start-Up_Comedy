using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EmployeeGenerator : MonoBehaviour
{
    #region Fields
    private List<GameObject> employees = new List<GameObject>();
    private string[] employeesNames = { "Boda", "PepoElMfsh5WeElTa3ban", "Q7tany", "Martin", "AlmrkanyElHwyan"};

    private Timer timer;
    private const float timerDuration = 2f;    // For Now

    private EmployeeSpecialization specialization = EmployeeSpecialization.Games;    // Fore Now
    private int hrSkill;

    private int maxSkillFactor = 5;     // For Now
    private int minSkillFactor = 3;     // For Now
    private int maxMinSalaryFactor = 5; // For Now
    private int minMinSalaryFactor = 3; // For Now

    private float maxTechnicaSkillFactor = 3; // For Now
    private float maxDesignSkillFactor = 3;   // For Now
    private float maxMarktingSkillFactor = 3; // For Now
    private float maxHrSkillFacotor = 3;

    private GameObjectEventInvoker onEmployeeGenerated;
    #endregion

    #region Unity Methods

    private void Start()
    {
        timer = gameObject.AddComponent<Timer>();
        timer.Init(timerDuration, GenerateEmployee);

        onEmployeeGenerated = gameObject.AddComponent<GameObjectEventInvoker>();
        EventManager.AddGameObjectEventInvoker(EventEnum.OnEmployeeGenerated, onEmployeeGenerated);        
    }

    #endregion

    #region Public Methods
    public void StartGeneration(EmployeeSpecialization _specialization, int _hrSkill)
    {
        gameObject.SetActive(true);
        specialization = _specialization;
        hrSkill = _hrSkill; // from game manager;
        timer.Run();
    }

    public void EndGeneration()
    {
        gameObject.SetActive(false);
    }

    private void GenerateEmployee()
    {
        // will be swich
        string employeeName = employeesNames[RandomGenerator.NextInt(0, employeesNames.Length)];
        int  employeeMinSalary = RandomGenerator.NextInt(minMinSalaryFactor, maxMinSalaryFactor);

        // Add Skill;

        GameObject employee = new GameObject(employeeName);


        switch (specialization)
        {
            case EmployeeSpecialization.Games:
                int technicalGamesSkills = (int)(hrSkill * RandomGenerator.NextFloat(.5f, maxTechnicaSkillFactor));
                int designGamesSkills = (int)(hrSkill * RandomGenerator.NextFloat(.5f, maxDesignSkillFactor));
                employee.AddComponent<ProjectEmployee>().Init(employeeName, ProjectSpecialization.Games, employeeMinSalary, technicalGamesSkills, designGamesSkills);
                break;
            case EmployeeSpecialization.Mobile:
                int technicalMobileSkills = (int)(hrSkill * RandomGenerator.NextFloat(.5f, maxTechnicaSkillFactor));
                int designMobileSkills = (int)(hrSkill * RandomGenerator.NextFloat(.5f, maxDesignSkillFactor));
                employee.AddComponent<ProjectEmployee>().Init(employeeName, ProjectSpecialization.Mobile, employeeMinSalary, technicalMobileSkills, designMobileSkills);
                break;
            case EmployeeSpecialization.Web:
                int technicalWebSkills = (int)(hrSkill * RandomGenerator.NextFloat(.5f, maxTechnicaSkillFactor));
                int designWebSkills = (int)(hrSkill * RandomGenerator.NextFloat(.5f, maxDesignSkillFactor));
                employee.AddComponent<ProjectEmployee>().Init(employeeName, ProjectSpecialization.Web, employeeMinSalary, technicalWebSkills, designWebSkills);
                break;
            case EmployeeSpecialization.Marketing:
                int marketingSkills = (int)(hrSkill * RandomGenerator.NextFloat(.5f, maxMarktingSkillFactor));
                employee.AddComponent<MarketingEmployee>().Init(employeeName, employeeMinSalary ,marketingSkills );
                break;
            case EmployeeSpecialization.DataAnalysis:
                employee.AddComponent<DataAnalysisEmployee>().Init(employeeName, EmployeeSpecialization.DataAnalysis ,employeeMinSalary);
                break;
            case EmployeeSpecialization.HR:
                int hrSkills = (int)(hrSkill * RandomGenerator.NextFloat(.5f,maxHrSkillFacotor));
                employee.AddComponent<HrEmployee>().Init(employeeName, employeeMinSalary, hrSkills);
                break;
        }

        employees.Add(employee);
        timer.Run();
        onEmployeeGenerated.Invoke(employee);
    }

    public void AddOnEmployeeGeneratedListener(UnityAction<GameObject> unityAction)
    {
        onEmployeeGenerated.AddListener(unityAction);
    }
    #endregion


}
