using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class ProjectInfoUI : MonoBehaviour
{
    [SerializeField] private UIDocument uIDocument;
    [SerializeField] private VisualTreeAsset assignedEmployeeCard;
    [SerializeField] private VisualTreeAsset unAssignedEmployeeCard;

    VisualElement assigndEmployeesTab;
    VisualElement unAssigndEmployeesTab;
    string pressedtabName = "tab-pressed";

    private Project project;

    VisualElement root;
    VisualElement specializationLogo;
    Button exitButton;
    Label projectName;
    Label deadline;
    Label price;
    Label penalClause;
    ProgressBar technicalProgress;
    ProgressBar designProgress;
    ListView assignedEmployeesList;
    ListView unAssignedEmployeesList;

    List<Employee> assignedEmployees;
    List<Employee> unAssignedEmployees;

    // Start is called before the first frame update
    void Start()
    {
        SetVisualElement();
        root.style.display = DisplayStyle.None;

        EventManager.AddVoidEventListener(EventEnum.OnProjectMangerOneSec, UpdateDynamicVisualElementData);
    }

    public void SetTheProject(Project project)
    {
        this.project = project;
        assignedEmployees = GameManager.GetAssignedEmployees(project);
        unAssignedEmployees = GameManager.GetUnAssignedEmployees(project);
        Debug.Log(project.Name);

        UpdateStaticVisualElementData();
        UpdateDynamicVisualElementData();
    }

    void SetVisualElement()
    {
        root = uIDocument.rootVisualElement;
        specializationLogo = root.Q<VisualElement>("Logo");
        projectName = root.Q<Label>("ProjectName");
        exitButton = root.Q<Button>("Exit");
        exitButton.clicked += () => { root.style.display = DisplayStyle.None; };
        deadline = root.Q<Label>("Deadline");
        price = root.Q<Label>("Price");
        penalClause = root.Q<Label>("PenalClause");
        technicalProgress = root.Q<ProgressBar>("TechnicalProgress");
        designProgress = root.Q<ProgressBar>("DesignProgress");
        assignedEmployeesList = root.Q<ListView>("AssignedEmployeesList");
        unAssignedEmployeesList = root.Q<ListView>("UnAssignedEmployeesList");

        assigndEmployeesTab = root.Q<VisualElement>("AssignedEmployeesTab");
        unAssigndEmployeesTab = root.Q<VisualElement>("UnAssignedEmployeesTab");

        assigndEmployeesTab.RegisterCallback<ClickEvent>(e => {
            assignedEmployeesList.style.display = DisplayStyle.Flex;
            unAssignedEmployeesList.style.display = DisplayStyle.None;
            if (!assigndEmployeesTab.ClassListContains(pressedtabName))
                assigndEmployeesTab.AddToClassList(pressedtabName);
            if (unAssigndEmployeesTab.ClassListContains(pressedtabName))
                unAssigndEmployeesTab.RemoveFromClassList(pressedtabName);

            assignedEmployeesList.Rebuild();

        });
        unAssigndEmployeesTab.RegisterCallback<ClickEvent>(e => {
            assignedEmployeesList.style.display = DisplayStyle.None;
            unAssignedEmployeesList.style.display = DisplayStyle.Flex;
            if (!unAssigndEmployeesTab.ClassListContains(pressedtabName))
                unAssigndEmployeesTab.AddToClassList(pressedtabName);
            if (assigndEmployeesTab.ClassListContains(pressedtabName))
                assigndEmployeesTab.RemoveFromClassList(pressedtabName);

            unAssignedEmployeesList.Rebuild();
        });

        unAssignedEmployeesList.style.display = DisplayStyle.None;

    }

    void InitAssignedEmployeesList()
    {
        assignedEmployeesList.makeItem = () =>
        {
            return assignedEmployeeCard.Instantiate();
        };

        assignedEmployeesList.bindItem = (item, index) =>
        {
            var emp = (assignedEmployees[index] as ProjectEmployee);
            var tech = emp.TechicalSkills;
            var des = emp.DesignSkills;

            item.Q<VisualElement>("SpecializationIcon").style.backgroundImage = specializationLogo.style.backgroundImage;
            item.Q<Label>("EmployeeName").text = assignedEmployees[index].Name;
            item.Q<Label>("PrimarySkills").text = tech.ToString();
            item.Q<Label>("SecondarySkills").text = des.ToString();
            item.Q<Button>("Delete").clicked += () =>
            {
                project.DismissEmployee(tech, des);
                emp.AssignProject(null);
                unAssignedEmployees.Add(assignedEmployees[index]);
                assignedEmployees.RemoveAt(index);

                assignedEmployeesList.Rebuild();
            };
        };
        assignedEmployeesList.fixedItemHeight = 106;
    }

    void InitUnAssignedEmployeesList()
    {
        unAssignedEmployeesList.makeItem = () =>
        {
            return unAssignedEmployeeCard.Instantiate();
        };

        unAssignedEmployeesList.bindItem = (item, index) =>
        {
            var emp = (unAssignedEmployees[index] as ProjectEmployee);
            var tech = emp.TechicalSkills;
            var des = emp.DesignSkills;

            item.Q<VisualElement>("SpecializationIcon").style.backgroundImage = specializationLogo.style.backgroundImage;
            item.Q<Label>("EmployeeName").text = unAssignedEmployees[index].Name;
            item.Q<Label>("PrimarySkills").text = tech.ToString();
            item.Q<Label>("SecondarySkills").text = des.ToString();
            item.Q<Button>("AssignProject").clicked += () =>
            {
                project.AssignEmployee(tech, des);
                emp.AssignProject(project);

                assignedEmployees.Add(unAssignedEmployees[index]);
                unAssignedEmployees.RemoveAt(index);

                unAssignedEmployeesList.Rebuild();

            };
        };
        unAssignedEmployeesList.fixedItemHeight = 106;
    }

    void UpdateStaticVisualElementData()
    {
        if (project == null) return;

        switch (project.Specialization)
        {
            case ProjectSpecialization.Games:
                specializationLogo.style.backgroundImage = new StyleBackground(Resources.Load<Sprite>("EmployeeLists/Games"));
                break;
            case ProjectSpecialization.Mobile:
                specializationLogo.style.backgroundImage = new StyleBackground(Resources.Load<Sprite>("EmployeeLists/Android"));
                break;
            case ProjectSpecialization.Web:
                specializationLogo.style.backgroundImage = new StyleBackground(Resources.Load<Sprite>("EmployeeLists/Web"));
                break;
        }

        projectName.text = project.Name;

        price.text = "Price : " + project.Price;

        penalClause.text = "Penal Clause : " + project.PenalClause;

        InitAssignedEmployeesList();
        assignedEmployeesList.itemsSource = assignedEmployees;
        assignedEmployeesList.Rebuild();

        InitUnAssignedEmployeesList();
        unAssignedEmployeesList.itemsSource = unAssignedEmployees;
        unAssignedEmployeesList.Rebuild();
    }

    void UpdateDynamicVisualElementData()
    {
        if (project == null) return;

        deadline.text = "";
        int intDeadline = (int)project.Deadline;
        int loop = 3;
        while (loop-- > 0)
        {
            deadline.text += (intDeadline / 60).ToString();
            if (loop > 1) deadline.text += ":";
            intDeadline = intDeadline % 60;
            loop--;
        }

        technicalProgress.value = project.TechnicalProgress;
        technicalProgress.title = "Technical Progress " + technicalProgress.value + "%";
        
        designProgress.value = project.DesignProgress;
        designProgress.title = "Design Progress " + designProgress.value + "%";

    }
}
