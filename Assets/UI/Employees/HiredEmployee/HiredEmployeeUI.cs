using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class HiredEmployeeUI : MonoBehaviour
{
    [SerializeField] private VisualTreeAsset EmployeeCardTemplate;

    List<Employee> employees;

    VisualElement root;

    ListView employeesList;

     EmployeeEventInvoker onEmployeeFired;

    void Start()
    {
        employees = GameManager.HiredEmployee;
        SetVisualElement();

        onEmployeeFired = gameObject.AddComponent<EmployeeEventInvoker>();
        EventManager.AddEmployeeEventInvoker(EventEnum.OnEmployeeFired,onEmployeeFired);

        EventManager.AddProjectEventListener(EventEnum.OnProjectDone, RebuildEmoloyeeList);
        EventManager.AddProjectEventListener(EventEnum.OnUnAssigndProjectFromEmployee, RebuildEmoloyeeList);
        EventManager.AddProjectEventListener(EventEnum.OnAssigndProjectToEmployee, RebuildEmoloyeeList);
        EventManager.AddProjectEventListener(EventEnum.OnDeadlineEnd, RebuildEmoloyeeList);
        EventManager.AddEmployeeEventListener(EventEnum.OnEmployeeHired, RebuildEmoloyeeList);

    }

    void SetVisualElement()
    {
        root = GetComponent<UIDocument>().rootVisualElement;
        employeesList = root.Q<ListView>("EmloyeesList");
        root.Q<Button>("Exit").clicked += () =>
        {
            root.style.display = DisplayStyle.None;
        };
        InitHiredListViwe();

        root.style.display = DisplayStyle.None;
    }

    void InitHiredListViwe()
    {

        employeesList.makeItem = () =>
        {
            var temp = EmployeeCardTemplate.Instantiate();
            temp.Q<Button>("Delete").clicked += () =>
            {
                WindowManager.ShowConfirmationAlert("Are you suree to DELETE this Employee ?!!",
                    () => {
                        ConfirmDelete(temp.userData as Employee);
                    });
            };
            return temp;

        };

        employeesList.bindItem = (item, index) =>
        {
            BindBaseEmployeeUI(item, index);

            switch (employees[index].Specialization)
            {
                case EmployeeSpecialization.Games:
                    BindGamesEmployee(item, index);
                    break;
                case EmployeeSpecialization.Web:
                    BindWebEmployee(item, index);
                    break;
                case EmployeeSpecialization.Mobile:
                    BindMobileEmployee(item, index);
                    break;
                case EmployeeSpecialization.HR:
                    BindHrEmployee(item, index);
                    break;
                case EmployeeSpecialization.Marketing:
                    BindMarketingEmployee(item, index);
                    break;
                case EmployeeSpecialization.DataAnalysis:
                    BindDataAnalysisEmployee(item, index);
                    break;
            }
        };
        employeesList.fixedItemHeight = 110;
        employeesList.itemsSource = employees;
    }

    void BindBaseEmployeeUI(VisualElement item, int index)
    {
        item.userData = employees[index];

        item.Q<Label>("EmployeeName").text = employees[index].Name;

    }

    public void ConfirmDelete(Employee employee)
    {   
        // Unassigned Employee From Project
        if (employee is ProjectEmployee)
        {
            ProjectEmployee projectEmployee = (ProjectEmployee) employee;
            if (projectEmployee.AssignedProject != null)
                projectEmployee.AssignedProject.DismissEmployee(projectEmployee.TechicalSkills, projectEmployee.DesignSkills);
        }

        onEmployeeFired.Invoke(null);

        // delet Employee and Rebuild List
        employees.Remove(employee);
        employeesList.Rebuild();
    }

    void BindGamesEmployee(VisualElement item, int index)
    {
        item.Q<VisualElement>("SpecializationIcon").style.backgroundImage = new StyleBackground(Resources.Load<Sprite>("EmployeeLists/Games"));
        item.Q<Label>("PrimarySkills").text = (employees[index] as ProjectEmployee).TechicalSkills.ToString();
        item.Q<Label>("SecondarySkills").text = (employees[index] as ProjectEmployee).DesignSkills.ToString();
        Project assindProject = (employees[index] as ProjectEmployee).AssignedProject;
        if (assindProject != null)
            item.Q<Label>("AssigndProject").text = assindProject.Name;
        else 
            item.Q<Label>("AssigndProject").text = "Not Assignd";
    }
    void BindMobileEmployee(VisualElement item, int index)
    {
        item.Q<VisualElement>("SpecializationIcon").style.backgroundImage = new StyleBackground(Resources.Load<Sprite>("EmployeeLists/Android"));
        item.Q<Label>("PrimarySkills").text = (employees[index] as ProjectEmployee).TechicalSkills.ToString();
        item.Q<Label>("SecondarySkills").text = (employees[index] as ProjectEmployee).DesignSkills.ToString();
        Project assindProject = (employees[index] as ProjectEmployee).AssignedProject;
        if (assindProject != null)
            item.Q<Label>("AssigndProject").text = assindProject.Name;
        else
            item.Q<Label>("AssigndProject").text = "Not Assignd";
    }
    void BindWebEmployee(VisualElement item, int index)
    {
        item.Q<VisualElement>("SpecializationIcon").style.backgroundImage = new StyleBackground(Resources.Load<Sprite>("EmployeeLists/Web"));
        item.Q<Label>("PrimarySkills").text = (employees[index] as ProjectEmployee).TechicalSkills.ToString();
        item.Q<Label>("SecondarySkills").text = (employees[index] as ProjectEmployee).DesignSkills.ToString();
        Project assindProject = (employees[index] as ProjectEmployee).AssignedProject;
        if (assindProject != null)
            item.Q<Label>("AssigndProject").text = assindProject.Name;
        else
            item.Q<Label>("AssigndProject").text = "Not Assignd";
    }

    void BindHrEmployee(VisualElement item, int index)
    {
        item.Q<VisualElement>("SpecializationIcon").style.backgroundImage = new StyleBackground(Resources.Load<Sprite>("EmployeeLists/Hr"));
        item.Q<VisualElement>("PrimaryIcon").style.backgroundImage = new StyleBackground(Resources.Load<Sprite>("EmployeeLists/GeneralSkills"));
        item.Q<Label>("PrimarySkills").text = (employees[index] as HrEmployee).HrSkill.ToString();
        item.Q<VisualElement>("SecondarySkillsContainer").style.visibility = Visibility.Hidden;
        item.Q<Label>("AssigndProject").style.visibility = Visibility.Hidden;
    }

    void BindMarketingEmployee(VisualElement item, int index)
    {
        item.Q<VisualElement>("SpecializationIcon").style.backgroundImage = new StyleBackground(Resources.Load<Sprite>("EmployeeLists/Marketing"));
        item.Q<VisualElement>("PrimaryIcon").style.backgroundImage = new StyleBackground(Resources.Load<Sprite>("EmployeeLists/GeneralSkills"));
        item.Q<Label>("PrimarySkills").text = (employees[index] as MarketingEmployee).MarketingSkill.ToString();
        item.Q<VisualElement>("SecondarySkillsContainer").style.visibility = Visibility.Hidden;
        item.Q<Label>("AssigndProject").style.visibility = Visibility.Hidden;
    }
    void BindDataAnalysisEmployee(VisualElement item, int index)
    {
        item.Q<VisualElement>("SpecializationIcon").style.backgroundImage = new StyleBackground(Resources.Load<Sprite>("EmployeeLists/DataAnalysis"));
        item.Q<VisualElement>("PrimarySkillsContainer").style.visibility = Visibility.Hidden;
        item.Q<VisualElement>("SecondarySkillsContainer").style.visibility = Visibility.Hidden;
        item.Q<Label>("AssigndProject").style.visibility = Visibility.Hidden;
    }

    void RebuildEmoloyeeList(Employee employee)
    {
        employeesList.Rebuild();
    }
    void RebuildEmoloyeeList(Project project)
    {
        employeesList.Rebuild();
    }

}
