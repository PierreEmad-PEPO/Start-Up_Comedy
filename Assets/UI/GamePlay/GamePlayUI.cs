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
    Button normalSpeed;
    Button fastSpeed;

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

        projects = root.Q<Label>("Projects");
        UpdateProjectsLabel(null);

        employees = root.Q<Label>("Employees");
        UpdateEmployeesLabel(null);

        normalSpeed = root.Q<Button>("NormalSpeedButton");
        fastSpeed = root.Q<Button>("FastSpeedButton");

        normalSpeed.clicked += () =>
        {
            Time.timeScale = 1;

            fastSpeed.RemoveFromClassList("button-pressed");
            normalSpeed.AddToClassList("button-pressed");
        };

        fastSpeed.clicked += () =>
        {
            Time.timeScale = 4;

            normalSpeed.RemoveFromClassList("button-pressed");
            fastSpeed.AddToClassList("button-pressed");
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
        root.Q<Button>("StatusButton").clicked += () =>
        {
            WindowManager.OpenWindow(WindowName.StartUpStatus);
        };
        root.Q<Button>("LoanButton").clicked += () =>
        {
            WindowManager.OpenWindow(WindowName.Loan);
        };
        root.Q<Button>("StoreButton").clicked += () =>
        {
            WindowManager.OpenWindow(WindowName.Store);
        };

    }

    private void UpdateEmployeesLabel(Employee employee)
    {
        employees.text = GameManager.HiredEmployee.Count.ToString();
    }

    private void UpdateProjectsLabel(Project project)
    {
        projects.text = GameManager.Projects.Count.ToString();
    }

    void UpdateBudgetLabel()
    {
        budget.text = GameManager.StartUp.Budget.ToString("N0");
    }
}

