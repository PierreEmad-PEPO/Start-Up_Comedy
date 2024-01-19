using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EmployeeHiringListView : MonoBehaviour
{
    #region Field

    [SerializeField] private VisualTreeAsset EmployeeCardTemplate;
    [SerializeField] private Negotiation negotiationMenu;

    private EmployeeSpecialization em;

    List<Employee> employees = new List<Employee>();

    VisualElement root;

    ListView employeeList;

    EmployeeGenerator employeeGenerator;

    Button StartGenerationButton;
    Button EndGenerationButton;
    EnumField employeeEnum;

    #endregion

    private void Start()
    {
        EventManager.AddEmployeeEventListener(EventEnum.OnEmployeeGenerated, AddEmployee);
        employeeGenerator = GetComponent<EmployeeGenerator>();

        SetVisualElement();
        InitHiringListViwe();

    }

    void SetVisualElement()
    {
        root = GetComponent<UIDocument>().rootVisualElement;
        employeeList = root.Q<ListView>("HiringList");
        StartGenerationButton = root.Q<Button>("Start");
        EndGenerationButton = root.Q<Button>("End");
        employeeEnum = root.Q<EnumField>("EmployeeEnum");
        StartGenerationButton.clicked += () =>
        {
            StartGenerationButton.style.display = DisplayStyle.None;
            EndGenerationButton.style.display = DisplayStyle.Flex;
            EmployeeSpecialization employeeSpecialization = (EmployeeSpecialization)employeeEnum.value;
            employeeEnum.pickingMode = PickingMode.Ignore;
            employeeGenerator.StartGeneration(employeeSpecialization, 50);
        };
        EndGenerationButton.clicked += () =>
        {
            StartGenerationButton.style.display = DisplayStyle.Flex;
            EndGenerationButton.style.display = DisplayStyle.None;
            employeeEnum.pickingMode = PickingMode.Position;
            employeeGenerator.EndGeneration();
        };

    }

    void InitHiringListViwe()
    {
        
        employeeList.makeItem = () =>
        {
            var temp = EmployeeCardTemplate.Instantiate();
            return temp;
           
        };

        employeeList.bindItem = (item, index) =>
        {
            BindBaseEmployeeUI(item, index);

            switch(employees[index].Specialization)
            {
                case EmployeeSpecialization.Games:
                    BindGamesEmployee(item,index);
                    break;
                case EmployeeSpecialization.Web:
                    BindWebEmployee(item,index);
                    break;
                case EmployeeSpecialization.Mobile:
                    BindMobileEmployee(item,index);
                    break;
                case EmployeeSpecialization.HR:
                    BindHrEmployee(item,index);
                    break;
                case EmployeeSpecialization.Marketing:
                    BindMarketingEmployee(item,index);
                    break;
                case EmployeeSpecialization.DataAnalysis:
                    BindDataAnalysisEmployee(item,index);
                    break;
            }
        };
        employeeList.fixedItemHeight = 110;
        employeeList.itemsSource = employees;
    }

    void BindBaseEmployeeUI(VisualElement item, int index)
    {
        item.Q<Label>("EmployeeName").text = employees[index].Name;
        item.Q<Button>("Negotiation").clicked += () =>
        {
            negotiationMenu.SetTheEmployee(employees[index]);
        };
        item.Q<Button>("Delete").clicked += () =>
        {
            employees.RemoveAt(index);
            employeeList.Rebuild();
        };
    }

    void BindGamesEmployee(VisualElement item, int index)
    {
        item.Q<VisualElement>("SpecializationIcon").style.backgroundImage = new StyleBackground (Resources.Load<Sprite>("EmployeeLists/Games"));
        item.Q<Label>("PrimarySkills").text = (employees[index] as ProjectEmployee).TechicalSkills.ToString();
        item.Q<Label>("SecondarySkills").text = (employees[index] as ProjectEmployee).DesignSkills.ToString();
    }
    void BindMobileEmployee(VisualElement item, int index)
    {
        item.Q<VisualElement>("SpecializationIcon").style.backgroundImage = new StyleBackground(Resources.Load<Sprite>("EmployeeLists/Android"));
        item.Q<Label>("PrimarySkills").text = (employees[index] as ProjectEmployee).TechicalSkills.ToString();
        item.Q<Label>("SecondarySkills").text = (employees[index] as ProjectEmployee).DesignSkills.ToString();
    }
    void BindWebEmployee(VisualElement item, int index)
    {
        item.Q<VisualElement>("SpecializationIcon").style.backgroundImage = new StyleBackground(Resources.Load<Sprite>("EmployeeLists/Web"));
        item.Q<Label>("PrimarySkills").text = (employees[index] as ProjectEmployee).TechicalSkills.ToString();
        item.Q<Label>("SecondarySkills").text = (employees[index] as ProjectEmployee).DesignSkills.ToString();
    }

    void BindHrEmployee (VisualElement item, int index)
    {
        item.Q<VisualElement>("SpecializationIcon").style.backgroundImage = new StyleBackground(Resources.Load<Sprite>("EmployeeLists/Hr"));
        item.Q<VisualElement>("PrimaryIcon").style.backgroundImage = new StyleBackground(Resources.Load<Sprite>("EmployeeLists/GeneralSkills"));
        item.Q<Label>("PrimarySkills").text = (employees[index] as HrEmployee).HrSkill.ToString();
        item.Q<VisualElement>("SecondarySkillsContainer").style.visibility = Visibility.Hidden;
    }

    void BindMarketingEmployee(VisualElement item, int index)
    {
        item.Q<VisualElement>("SpecializationIcon").style.backgroundImage = new StyleBackground(Resources.Load<Sprite>("EmployeeLists/Marketing"));
        item.Q<VisualElement>("PrimaryIcon").style.backgroundImage = new StyleBackground(Resources.Load<Sprite>("EmployeeLists/GeneralSkills"));
        item.Q<Label>("PrimarySkills").text = (employees[index] as MarketingEmployee).MarketingSkill.ToString();
        item.Q<VisualElement>("SecondarySkillsContainer").style.visibility = Visibility.Hidden;
    }
    void BindDataAnalysisEmployee(VisualElement item, int index)
    {
        item.Q<VisualElement>("SpecializationIcon").style.backgroundImage = new StyleBackground(Resources.Load<Sprite>("EmployeeLists/DataAnalysis"));
        item.Q<VisualElement>("PrimarySkillsContainer").style.visibility = Visibility.Hidden;
        item.Q<VisualElement>("SecondarySkillsContainer").style.visibility = Visibility.Hidden;
    }
    void AddEmployee(Employee employee)
    {
        em = employee.Specialization;
        employees.Add(employee);
        employeeList.Rebuild();
    }

}
