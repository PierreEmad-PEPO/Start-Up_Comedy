using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class GamePlayUI : MonoBehaviour
{
    [SerializeField] UIDocument acceptedProjectList;
    [SerializeField] UIDocument hitingList;
    VisualElement root;
    Button projectsButton;
    Button hrButton;
    Button marketing;
    Button employees;
    // Start is called before the first frame update
    void Start()
    {
        root = GetComponent<UIDocument>().rootVisualElement;
        projectsButton = root.Q<Button>("ProjectsButton");
        hrButton = root.Q<Button>("HrButton");
        marketing = root.Q <Button>("MarketingButton");
        employees = root.Q<Button>("EmployeesButton");
        projectsButton.clicked += () =>
        {
            WindowManager.OpenWindow(WindowName.Projects);
        };

        hrButton.clicked += () =>
        {
            WindowManager.OpenWindow(WindowName.HiringEmployees);
        };

        marketing.clicked += () =>
        {
            WindowManager.OpenWindow(WindowName.Markiting);
        };

        employees.clicked += () =>
        {
            WindowManager.OpenWindow(WindowName.Employees);
        };

    }
}

