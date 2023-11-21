using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmployeeGenerator : MonoBehaviour
{
    #region Fields
    List<Employee> employees = new List<Employee>();
    string[] employeesNames = { "Boda", "PepoElMfsh5WeElTa3ban", "Q7tany", "Martin", "AlmrkanyElHwyan"};

    Timer timer ;
    float timerDuration = 2f;    // For Now

    EmployeeSpecialization specialization = EmployeeSpecialization.Game;    // Fore Now
    int hrSkill;

    int maxSkillFactor = 5;     // For Now
    int minSkillFactor = 3;     // For Now
    int maxMinSalaryFactor = 5; // For Now
    int minMinSalaryFactor = 3; // For Now


    #endregion

    #region Unity Methods

    private void Start()
    {
        timer = gameObject.AddComponent<Timer>();
        timer.Init(timerDuration, GenerateEmployee);
        timer.Run();
    }

    #endregion

    #region Public Methods
    public void StartGeneration(EmployeeSpecialization _specialization, int _hrSkill)
    {
        specialization = _specialization;
        hrSkill = _hrSkill; // from game manager;
        gameObject.SetActive(true);
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

        Employee employee = new Employee(employeeName, specialization, employeeMinSalary);
        employees.Add(employee);
        Debug.Log(employee.Name);
        timer.Run();
        /*switch (specialization)
        {
            case EmployeeSpecialization.Game: GenerateGameEmployee();break;
            case EmployeeSpecialization.Mobile: GenerateMobileEmployee(); break;
            case EmployeeSpecialization.Web: GenerateWebEmployee();break;
            case EmployeeSpecialization.Marketing: GenerateMarketingEmployee();break;
            case EmployeeSpecialization.DataAnalysis: GenerateDataAnalysisEmployee();break;
            case EmployeeSpecialization.HR: GenerateHrEmployee();break; 
        }*/

    }

    public void GenerateGameEmployee() { }
    public void GenerateMobileEmployee() { }
    public void GenerateWebEmployee() { }
    public void GenerateHrEmployee() { }
    public void GenerateMarketingEmployee() { }
    public void GenerateDataAnalysisEmployee() { }
    #endregion


}
