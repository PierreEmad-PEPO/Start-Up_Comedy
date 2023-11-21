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

    private EmployeeSpecialization specialization = EmployeeSpecialization.Game;    // Fore Now
    private int hrSkill;

    private int maxSkillFactor = 5;     // For Now
    private int minSkillFactor = 3;     // For Now
    private int maxMinSalaryFactor = 5; // For Now
    private int minMinSalaryFactor = 3; // For Now

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
        employee.AddComponent<Employee>().Init(employeeName, specialization, employeeMinSalary);
        employees.Add(employee);
        Debug.Log(employee);
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

        onEmployeeGenerated.Invoke(employee);
    }

    public void GenerateGameEmployee() { }
    public void GenerateMobileEmployee() { }
    public void GenerateWebEmployee() { }
    public void GenerateHrEmployee() { }
    public void GenerateMarketingEmployee() { }
    public void GenerateDataAnalysisEmployee() { }
    public void AddOnEmployeeGeneratedListener(UnityAction<GameObject> unityAction)
    {
        onEmployeeGenerated.AddListener(unityAction);
    }
    #endregion


}
