using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EmployeeGenerator : MonoBehaviour
{
    #region Fields
    private string[] employeesNames = { "ABDELRHMAN", "PIERRE", "ZIAD", "MARTIN", "SAMUEL","KHALED","ABDULLAH","AHMED","ESLAM"};

    private Timer timer;
    private const float timerDuration = 2f;    // For Now

    private EmployeeSpecialization specialization = EmployeeSpecialization.Games;
    private int hrSkill = 50; // From Game Manager

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

    }

    public void EndGeneration()
    {
        timer.Pause();
        
    }

    private void GenerateEmployee()
    {
        Employee employee = new Employee();

        switch (specialization)
        {
            case EmployeeSpecialization.Games:
                employee = GenerateProjectEmployeeFactor(employee, ProjectSpecialization.Games);
                break;
            case EmployeeSpecialization.Mobile:
                employee = GenerateProjectEmployeeFactor(employee, ProjectSpecialization.Mobile);
                break;
            case EmployeeSpecialization.Web:
                employee = GenerateProjectEmployeeFactor(employee, ProjectSpecialization.Web);
                break;
            case EmployeeSpecialization.Marketing:
                employee = new MarketingEmployee();
                int marketingSkills = GenerateSkille();
                (employee as MarketingEmployee).Init(GenerateEmployeeName(), GenerateSarary(marketingSkills), marketingSkills);
                break;
            case EmployeeSpecialization.DataAnalysis:
                employee = new DataAnalysisEmployee();
                (employee as DataAnalysisEmployee).Init(GenerateEmployeeName(), EmployeeSpecialization.DataAnalysis ,GenerateSarary(200f));
                break;
            case EmployeeSpecialization.HR:
                employee = new HrEmployee();
                int _hrSkills = GenerateSkille();
                (employee as HrEmployee).Init(GenerateEmployeeName(), GenerateSarary(_hrSkills), _hrSkills);
                break;
        }

        timer.Run();
        onEmployeeGenerated.Invoke(employee);
    }

    int GenerateSkille()
    {
        return Math.Min((int)(hrSkill * RandomGenerator.NextFloat(.5f, 1.5f)),500);
    }

    string GenerateEmployeeName()
    {
        return employeesNames[RandomGenerator.NextInt(0, employeesNames.Length)];
    }

    int GenerateSarary(float avrgeSkilles)
    {
        return (int)avrgeSkilles * 20;
    }

    ProjectEmployee GenerateProjectEmployeeFactor(Employee employee, ProjectSpecialization spec)
    {
        employee = new ProjectEmployee();
        int technicalGamesSkills = GenerateSkille();
        int designGamesSkills = GenerateSkille();
        float avergSkills = (designGamesSkills + technicalGamesSkills) / 2f; 
        int minSalary = GenerateSarary(avergSkills);
        (employee as ProjectEmployee).Init(GenerateEmployeeName() ,spec, minSalary, technicalGamesSkills, designGamesSkills);
        return (employee as ProjectEmployee);
    }

    #endregion

}
