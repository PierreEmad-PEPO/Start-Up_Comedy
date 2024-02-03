using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class GamePlayUI : MonoBehaviour
{
    VisualElement root;
    Label budget;
    Label popularity;
    Label projects;
    Label employees;

    // Start is called before the first frame update
    void Start()
    {
        SetVisualElement();

        EventManager.AddVoidEventListener(EventEnum.OnBudgetChanged, UpdateBudgetLabel);
        EventManager.AddProjectEventListener(EventEnum.OnProjectAccepted, UpdateProjectsLabel);
        EventManager.AddProjectEventListener(EventEnum.OnDeadlineEnd, UpdateProjectsLabel);
        EventManager.AddProjectEventListener(EventEnum.OnProjectDone, UpdateProjectsLabel);
        EventManager.AddEmployeeEventListener(EventEnum.OnEmployeeHired, UpdateEmployeesLabel);
        EventManager.AddEmployeeEventListener(EventEnum.OnEmployeeFired, UpdateEmployeesLabel);

    }

    void SetVisualElement()
    {
        root = GetComponent<UIDocument>().rootVisualElement;
        // Top
        budget = root.Q<Label>("Budget");
        UpdateBudgetLabel();

        popularity = root.Q<Label>("Popularity");
        UpdatePopularityLabel();

        projects = root.Q<Label>("Projects");
        UpdateProjectsLabel(null);

        employees = root.Q<Label>("Employees");
        UpdateEmployeesLabel(null);

        root.Q<Button>("NormalSpeedButton").clicked += () =>
        {
            Time.timeScale = 1;
        };

        root.Q<Button>("FastSpeedButton").clicked += () =>
        {
            Time.timeScale = 2;
        };

        // Bottom
        root.Q<Button>("ProjectsButton").clicked += () =>
        {
            WindowManager.OpenWindow(WindowName.Projects);
        }; 
        root.Q<Button>("HrButton").clicked += () =>
        {
            WindowManager.OpenWindow(WindowName.HiringEmployees);
        }; 
        root.Q<Button>("MarketingButton").clicked += () =>
        {
            WindowManager.OpenWindow(WindowName.Markiting);
        }; 
        root.Q<Button>("EmployeesButton").clicked += () =>
        {
            WindowManager.OpenWindow(WindowName.Employees);
        }; 

    }

    IEnumerator UpdatePopularity ()
    {
        while (true)
        {
            UpdatePopularity();

            yield return new WaitForSeconds(1);
        }
    }

    private void UpdateEmployeesLabel(Employee employee)
    {
        employees.text = GameManager.HiredEmployee.Count.ToString();
    }

    private void UpdateProjectsLabel(Project project)
    {
        projects.text = GameManager.Projects.Count.ToString();
    }

    private void UpdatePopularityLabel()
    {
        popularity.text = GameManager.StartUp.Popularity.ToString();
    }

    void UpdateBudgetLabel()
    {
        Debug.Log(budget.ToString());
        Debug.Log(GameManager.StartUp.ToString());
        budget.text = GameManager.StartUp.Budget.ToString("N0");
    }
}

