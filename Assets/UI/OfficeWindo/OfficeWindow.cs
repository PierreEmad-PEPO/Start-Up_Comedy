using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class OfficeWindow : MonoBehaviour
{
    [SerializeField] VisualTreeAsset waitingCard;
    [SerializeField] PlacementSystem placement;
    [SerializeField] GameObject[] employeeModels;
    [SerializeField] EmployeesManager employeesManager;
    VisualElement root;
    VisualElement data;
    VisualElement waiting;

    ListView waitingEmployees;
    Employee employee;
    List<Employee> employees;
    GameObject office;

    VisualElement car;

    // Start is called before the first frame update
    void Start()
    {
        SetVisualElement();
        EventManager.AddEmployeeEventListener(EventEnum.OnEmployeeFired, DestroyCharacter);
    }

    void SetVisualElement()
    {
        root = GetComponent<UIDocument>().rootVisualElement;
        data = root.Q<VisualElement>("Data");
        waiting = root.Q<VisualElement>("Waiting");

        waitingEmployees = root.Q<ListView>("EmployeeList");
        car = root.Q<VisualElement>("Card1");
        InitWaitingList();
        root.Query<Button>("Replace").First().clicked+= () => 
        {
            root.style.display = DisplayStyle.None;
            placement.AssignActiveObject(office);
        };
        root.Query<Button>("Replace").AtIndex(1).clicked += () =>
        {
            root.style.display = DisplayStyle.None;
            placement.AssignActiveObject(office);
        };

        root.Q<Button>("Fire").clicked += () =>
        {
            root.style.display = DisplayStyle.None;
            employeesManager.FireEmployee(employee);
        };

        root.Q<Button>("Exit").clicked += () => { root.style.display = DisplayStyle.None; };

        root.style.display = DisplayStyle.None;
    }

    void InitWaitingList()
    {
        waitingEmployees.makeItem = () =>
        {

            var temp = waitingCard.Instantiate();
            temp.RegisterCallback<ClickEvent>(e => {
                office.GetComponent<Office>().setEmployee(temp.userData as Employee);
                GameObject model = Instantiate(employeeModels[0]);
                model.transform.parent = office.transform.GetChild(0);
                model.transform.localPosition = Vector3.zero;
                model.transform.localRotation = Quaternion.identity;
                model.transform.localScale = Vector3.one;
                ViewData(temp.userData as Employee,office);
            });
            return temp;

        };

        waitingEmployees.bindItem = (item, index) =>
        {
            /*            item.userData = employees[index];
                        item.Q<Label>("Name").text = employees[index].Name;*/
            Bind(employees[index], item);
        };
        waitingEmployees.fixedItemHeight = 110;
        waitingEmployees.itemsSource = employees;

    }

    public void ViewData(Employee _employee, GameObject office) 
    {
        employee = _employee;
        // employeeName.text = _employee.Name;
        Bind(employee,car);
        this.office = office;
        data.style.display = DisplayStyle.Flex;
        waiting.style.display = DisplayStyle.None;
    }

    public void ViewLWaitingList(GameObject office)
    {
        this.office = office;
        employees = GameManager.GetWaitingEmployees();
        waitingEmployees.itemsSource = employees;
        data.style.display = DisplayStyle.None;
        waiting.style.display = DisplayStyle.Flex;
        waitingEmployees.Rebuild();
    }

    public void DestroyCharacter(Employee employee)
    {
        if (employee.IsSet)
        {
            GameManager.GetOffice(employee.SetID).UnsetEmployee();
        }
    }

    void Bind(Employee employee, VisualElement card)
    {
        BindBaseEmployeeUI(employee, card);

        switch (employee.Specialization)
        {
            case EmployeeSpecialization.Games:
                BindGamesEmployee(employee, card);
                break;
            case EmployeeSpecialization.Web:
                BindWebEmployee(employee, card);
                break;
            case EmployeeSpecialization.Mobile:
                BindMobileEmployee(employee, card);
                break;
            case EmployeeSpecialization.HR:
                BindHrEmployee(employee, card);
                break;
            case EmployeeSpecialization.Marketing:
                BindMarketingEmployee(employee, card);
                break;
            case EmployeeSpecialization.DataAnalysis:
                BindDataAnalysisEmployee(employee, card);
                break;
        }
    }
    void BindBaseEmployeeUI(Employee employee, VisualElement item)
    {
        item.userData = employee;

        item.Q<Label>("EmployeeName").text = employee.Name;
        item.Q<Label>("Salary").text = employee.Salary.ToString();
        item.Q<VisualElement>("SecondarySkillsContainer").style.visibility = Visibility.Visible;
        item.Q<VisualElement>("AssigndProjectContainer").style.visibility = Visibility.Visible;

    }
    void BindGamesEmployee(Employee employee, VisualElement item)
    {
        item.Q<VisualElement>("SpecializationIcon").style.backgroundImage = new StyleBackground(Resources.Load<Sprite>("EmployeeLists/Games"));
        /* item.Q<Label>("PrimarySkills").text = (employees[index] as ProjectEmployee).TechnicalSkills.ToString();
         item.Q<Label>("SecondarySkills").text = (employees[index] as ProjectEmployee).DesignSkills.ToString();*/
        ProgressBar prog = item.Q<ProgressBar>("PrimarySkills");
        prog.value = ((employee as ProjectEmployee).TechnicalSkills * 100 / 500);
        prog.title = prog.value.ToString() + "%";
        prog = item.Q<ProgressBar>("SecondarySkills");
        prog.value = ((employee as ProjectEmployee).DesignSkills * 100 / 500);
        prog.title = prog.value.ToString() + "%";
        Project assignedProject = (employee as ProjectEmployee).AssignedProject;
        if (assignedProject != null)
            item.Q<Label>("AssigndProject").text = assignedProject.Name;
        else
            item.Q<Label>("AssigndProject").text = "Not Assigned";
    }
    void BindMobileEmployee(Employee employee, VisualElement item)
    {
        item.Q<VisualElement>("SpecializationIcon").style.backgroundImage = new StyleBackground(Resources.Load<Sprite>("EmployeeLists/Android"));
        ProgressBar prog = item.Q<ProgressBar>("PrimarySkills");
        prog.value = ((employee as ProjectEmployee).TechnicalSkills * 100 / 500);
        prog.title = prog.value.ToString() + "%";
        prog = item.Q<ProgressBar>("SecondarySkills");
        prog.value = ((employee as ProjectEmployee).DesignSkills * 100 / 500);
        prog.title = prog.value.ToString() + "%";

        Project assignedProject = (employee as ProjectEmployee).AssignedProject;
        if (assignedProject != null)
            item.Q<Label>("AssigndProject").text = assignedProject.Name;
        else
            item.Q<Label>("AssigndProject").text = "Not Assigned";
    }
    void BindWebEmployee(Employee employee, VisualElement item)
    {
        item.Q<VisualElement>("SpecializationIcon").style.backgroundImage = new StyleBackground(Resources.Load<Sprite>("EmployeeLists/Web"));
        //item.Q<Label>("PrimarySkills").text = (employees[index] as ProjectEmployee).TechnicalSkills.ToString();
        //item.Q<Label>("SecondarySkills").text = (employees[index] as ProjectEmployee).DesignSkills.ToString();

        ProgressBar prog = item.Q<ProgressBar>("PrimarySkills");
        prog.value = ((employee as ProjectEmployee).TechnicalSkills * 100 / 500);
        prog.title = prog.value.ToString() + "%";
        prog = item.Q<ProgressBar>("SecondarySkills");
        prog.value = ((employee as ProjectEmployee).DesignSkills * 100 / 500);
        prog.title = prog.value.ToString() + "%";

        Project assignedProject = (employee as ProjectEmployee).AssignedProject;
        if (assignedProject != null)
            item.Q<Label>("AssigndProject").text = assignedProject.Name;
        else
            item.Q<Label>("AssigndProject").text = "Not Assigned";
    }

    void BindHrEmployee(Employee employee, VisualElement item)
    {
        item.Q<VisualElement>("SpecializationIcon").style.backgroundImage = new StyleBackground(Resources.Load<Sprite>("EmployeeLists/Hr"));
        item.Q<VisualElement>("PrimaryIcon").style.backgroundImage = new StyleBackground(Resources.Load<Sprite>("EmployeeLists/GeneralSkills"));
        //item.Q<Label>("PrimarySkills").text = (employees[index] as HrEmployee).HrSkill.ToString();

        ProgressBar prog = item.Q<ProgressBar>("PrimarySkills");
        prog.value = ((employee as HrEmployee).HrSkill * 100 / 500);
        prog.title = prog.value.ToString() + "%";

        item.Q<VisualElement>("SecondarySkillsContainer").style.visibility = Visibility.Hidden;
        item.Q<VisualElement>("AssigndProjectContainer").style.visibility = Visibility.Hidden;
    }

    void BindMarketingEmployee(Employee employee, VisualElement item)
    {
        item.Q<VisualElement>("SpecializationIcon").style.backgroundImage = new StyleBackground(Resources.Load<Sprite>("EmployeeLists/Marketing"));
        item.Q<VisualElement>("PrimaryIcon").style.backgroundImage = new StyleBackground(Resources.Load<Sprite>("EmployeeLists/GeneralSkills"));
        // item.Q<Label>("PrimarySkills").text = (employees[index] as MarketingEmployee).MarketingSkill.ToString();
        ProgressBar prog = item.Q<ProgressBar>("PrimarySkills");
        prog.value = ((employee as MarketingEmployee).MarketingSkill * 100 / 500);
        prog.title = prog.value.ToString() + "%";

        item.Q<VisualElement>("SecondarySkillsContainer").style.visibility = Visibility.Hidden;
        item.Q<VisualElement>("AssigndProjectContainer").style.visibility = Visibility.Hidden;
    }
    void BindDataAnalysisEmployee(Employee employee, VisualElement item)
    {
        item.Q<VisualElement>("SpecializationIcon").style.backgroundImage = new StyleBackground(Resources.Load<Sprite>("EmployeeLists/DataAnalysis"));
        item.Q<VisualElement>("PrimarySkillsContainer").style.visibility = Visibility.Hidden;
        item.Q<VisualElement>("SecondarySkillsContainer").style.visibility = Visibility.Hidden;
        item.Q<VisualElement>("AssigndProjectContainer").style.visibility = Visibility.Hidden;
    }
}
