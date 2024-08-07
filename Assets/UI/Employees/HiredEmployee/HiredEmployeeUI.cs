using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class HiredEmployeeUI : MonoBehaviour
{
    [SerializeField] private VisualTreeAsset EmployeeCardTemplate;
    [SerializeField] private EmployeesManager employeesManager;

    List<Employee> employees;

    VisualElement root;

    ListView employeesList;


    void Start()
    {
        employees = GameManager.HiredEmployee;
        SetVisualElement();


        EventManager.AddProjectEventListener(EventEnum.OnProjectDone, RebuildEmployeeList);
        EventManager.AddProjectEventListener(EventEnum.OnUnAssignProjectFromEmployee, RebuildEmployeeList);
        EventManager.AddProjectEventListener(EventEnum.OnAssignProjectToEmployee, RebuildEmployeeList);
        EventManager.AddProjectEventListener(EventEnum.OnDeadlineEnd, RebuildEmployeeList);
        EventManager.AddEmployeeEventListener(EventEnum.OnEmployeeHired, RebuildEmployeeList);
        EventManager.AddEmployeeEventListener(EventEnum.OnEmployeeFired, RebuildEmployeeList);

    }

    void SetVisualElement()
    {
        root = GetComponent<UIDocument>().rootVisualElement;
        employeesList = root.Q<ListView>("EmloyeesList");
        root.Q<Button>("Exit").clicked += () =>
        {
            root.style.display = DisplayStyle.None;
        };
        InitHiredListView();

        root.style.display = DisplayStyle.None;
    }

    void InitHiredListView()
    {

        employeesList.makeItem = () =>
        {
            var temp = EmployeeCardTemplate.Instantiate();
            temp.Q<Button>("Delete").clicked += () =>
            {
                WindowManager.ShowConfirmationAlert("Are you sure to DELETE this Employee ?!!",
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
        item.Q <Label>("Salary").text = employees[index].Salary.ToString();
        item.Q<VisualElement>("SecondarySkillsContainer").style.visibility = Visibility.Visible;
        item.Q<VisualElement>("AssigndProjectContainer").style.visibility = Visibility.Visible;
    }

    public void ConfirmDelete(Employee employee)
    {   
        // Unassigned Employee From Project

        employeesManager.FireEmployee(employee);
    }

    void BindGamesEmployee(VisualElement item, int index)
    {
        item.Q<VisualElement>("SpecializationIcon").style.backgroundImage = new StyleBackground(Resources.Load<Sprite>("EmployeeLists/Games"));
        /* item.Q<Label>("PrimarySkills").text = (employees[index] as ProjectEmployee).TechnicalSkills.ToString();
         item.Q<Label>("SecondarySkills").text = (employees[index] as ProjectEmployee).DesignSkills.ToString();*/
        ProgressBar prog = item.Q<ProgressBar>("PrimarySkills");
        prog.value = ((employees[index] as ProjectEmployee).TechnicalSkills * 100 / 500);
        prog.title = prog.value.ToString() + "%";
        prog = item.Q<ProgressBar>("SecondarySkills");
        prog.value = ((employees[index] as ProjectEmployee).DesignSkills * 100 / 500);
        prog.title = prog.value.ToString() + "%";
        Project assinedProject = (employees[index] as ProjectEmployee).AssignedProject;
        if (assinedProject != null)
            item.Q<Label>("AssigndProject").text = assinedProject.Name;
        else 
            item.Q<Label>("AssigndProject").text = "Not Assigned";
    }
    void BindMobileEmployee(VisualElement item, int index)
    {
        item.Q<VisualElement>("SpecializationIcon").style.backgroundImage = new StyleBackground(Resources.Load<Sprite>("EmployeeLists/Android"));
        /*item.Q<Label>("PrimarySkills").text = (employees[index] as ProjectEmployee).TechnicalSkills.ToString();
        item.Q<Label>("SecondarySkills").text = (employees[index] as ProjectEmployee).DesignSkills.ToString();*/

        ProgressBar prog = item.Q<ProgressBar>("PrimarySkills");
        prog.value = ((employees[index] as ProjectEmployee).TechnicalSkills * 100 / 500);
        prog.title = prog.value.ToString() + "%";
        prog = item.Q<ProgressBar>("SecondarySkills");
        prog.value = ((employees[index] as ProjectEmployee).DesignSkills * 100 / 500);
        prog.title = prog.value.ToString() + "%";

        Project assindProject = (employees[index] as ProjectEmployee).AssignedProject;
        if (assindProject != null)
            item.Q<Label>("AssigndProject").text = assindProject.Name;
        else
            item.Q<Label>("AssigndProject").text = "Not Assigned";
    }
    void BindWebEmployee(VisualElement item, int index)
    {
        item.Q<VisualElement>("SpecializationIcon").style.backgroundImage = new StyleBackground(Resources.Load<Sprite>("EmployeeLists/Web"));
        //item.Q<Label>("PrimarySkills").text = (employees[index] as ProjectEmployee).TechnicalSkills.ToString();
        //item.Q<Label>("SecondarySkills").text = (employees[index] as ProjectEmployee).DesignSkills.ToString();

        ProgressBar prog = item.Q<ProgressBar>("PrimarySkills");
        prog.value = ((employees[index] as ProjectEmployee).TechnicalSkills * 100 / 500);
        prog.title = prog.value.ToString() + "%";
        prog = item.Q<ProgressBar>("SecondarySkills");
        prog.value = ((employees[index] as ProjectEmployee).DesignSkills * 100 / 500);
        prog.title = prog.value.ToString() + "%";

        Project assinedProject = (employees[index] as ProjectEmployee).AssignedProject;
        if (assinedProject != null)
            item.Q<Label>("AssigndProject").text = assinedProject.Name;
        else
            item.Q<Label>("AssigndProject").text = "Not Assigned";
    }

    void BindHrEmployee(VisualElement item, int index)
    {
        item.Q<VisualElement>("SpecializationIcon").style.backgroundImage = new StyleBackground(Resources.Load<Sprite>("EmployeeLists/Hr"));
        item.Q<VisualElement>("PrimaryIcon").style.backgroundImage = new StyleBackground(Resources.Load<Sprite>("EmployeeLists/GeneralSkills"));
        //item.Q<Label>("PrimarySkills").text = (employees[index] as HrEmployee).HrSkill.ToString();

        ProgressBar prog = item.Q<ProgressBar>("PrimarySkills");
        prog.value = ((employees[index] as HrEmployee).HrSkill * 100 / 500);
        prog.title = prog.value.ToString() + "%";

        item.Q<VisualElement>("SecondarySkillsContainer").style.visibility = Visibility.Hidden;
        item.Q<VisualElement>("AssigndProjectContainer").style.visibility = Visibility.Hidden;
    }

    void BindMarketingEmployee(VisualElement item, int index)
    {
        item.Q<VisualElement>("SpecializationIcon").style.backgroundImage = new StyleBackground(Resources.Load<Sprite>("EmployeeLists/Marketing"));
        item.Q<VisualElement>("PrimaryIcon").style.backgroundImage = new StyleBackground(Resources.Load<Sprite>("EmployeeLists/GeneralSkills"));
       // item.Q<Label>("PrimarySkills").text = (employees[index] as MarketingEmployee).MarketingSkill.ToString();
        ProgressBar prog = item.Q<ProgressBar>("PrimarySkills");
        prog.value = ((employees[index] as MarketingEmployee).MarketingSkill * 100 / 500);
        prog.title = prog.value.ToString() + "%";

        item.Q<VisualElement>("SecondarySkillsContainer").style.visibility = Visibility.Hidden;
        item.Q<VisualElement>("AssigndProjectContainer").style.visibility = Visibility.Hidden;
    }
    void BindDataAnalysisEmployee(VisualElement item, int index)
    {
        item.Q<VisualElement>("SpecializationIcon").style.backgroundImage = new StyleBackground(Resources.Load<Sprite>("EmployeeLists/DataAnalysis"));
        item.Q<VisualElement>("PrimarySkillsContainer").style.visibility = Visibility.Hidden;
        item.Q<VisualElement>("SecondarySkillsContainer").style.visibility = Visibility.Hidden;
        item.Q<VisualElement>("AssigndProjectContainer").style.visibility = Visibility.Hidden;
    }

    void RebuildEmployeeList(Employee employee)
    {
        employeesList.Rebuild();
    }
    void RebuildEmployeeList(Project project)
    {
        employeesList.Rebuild();
    }

}
