using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEditor.Progress;

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

    private ProjectEventInvoker onAssigndProjectToEmployee;
    private ProjectEventInvoker onUnAssigndProjectFromEmployee;

    // Start is called before the first frame update
    void Start()
    {
        SetVisualElement();
        root.style.display = DisplayStyle.None;

        onAssigndProjectToEmployee = gameObject.AddComponent<ProjectEventInvoker>();
        EventManager.AddProjectEventInvoker(EventEnum.OnAssigndProjectToEmployee, onAssigndProjectToEmployee);

        onUnAssigndProjectFromEmployee = gameObject.AddComponent<ProjectEventInvoker>();
        EventManager.AddProjectEventInvoker(EventEnum.OnUnAssigndProjectFromEmployee, onUnAssigndProjectFromEmployee);

        EventManager.AddVoidEventListener(EventEnum.OnProjectMangerOneSec, UpdateDynamicVisualElementData);
        EventManager.AddVoidEventListener(EventEnum.OnProjectDone, UpdateDynamicVisualElementData);
        EventManager.AddVoidEventListener(EventEnum.OnDeadlineEnd, UpdateDynamicVisualElementData);
        EventManager.AddProjectEventListener(EventEnum.OnProjectDone, RebuildUnAssignedEmployeesList);
        EventManager.AddProjectEventListener(EventEnum.OnDeadlineEnd, RebuildUnAssignedEmployeesList);
    }

    public void SetTheProject(Project project)
    {
        this.project = project;
        assignedEmployees = GameManager.GetAssignedEmployees(project);
        unAssignedEmployees = GameManager.GetUnAssignedEmployees(project);

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

        InitAssignedEmployeesList();
        InitUnAssignedEmployeesList();

    }

    void InitAssignedEmployeesList()
    {
        assignedEmployeesList.makeItem = () =>
        {
            var template = assignedEmployeeCard.Instantiate();
            template.Q<Button>("Delete").clicked += () =>
            {
                var emp = template.userData as ProjectEmployee;
                var tech = emp.TechicalSkills;
                var des = emp.DesignSkills;
                
                project.DismissEmployee(tech, des);
                emp.AssignProject(null);

                assignedEmployees.Remove(template.userData as Employee);

                unAssignedEmployees.Add(template.userData as Employee);

                assignedEmployeesList.Rebuild();

                onUnAssigndProjectFromEmployee.Invoke(project);
            };
            return template;
        };

        assignedEmployeesList.bindItem = (item, index) =>
        {
            var emp = (assignedEmployees[index] as ProjectEmployee);
            var tech = emp.TechicalSkills;
            var des = emp.DesignSkills;

            item.userData = assignedEmployees[index];

            item.Q<VisualElement>("SpecializationIcon").style.backgroundImage = specializationLogo.style.backgroundImage;
            item.Q<Label>("EmployeeName").text = assignedEmployees[index].Name;
            item.Q<Label>("PrimarySkills").text = tech.ToString();
            item.Q<Label>("SecondarySkills").text = des.ToString();
        };
        assignedEmployeesList.fixedItemHeight = 106;
    }

    void InitUnAssignedEmployeesList()
    {
        unAssignedEmployeesList.makeItem = () =>
        {
            var template = unAssignedEmployeeCard.Instantiate();

            template.Q<Button>("AssignProject").clicked += () =>
            {
                var emp = template.userData as ProjectEmployee;
                var tech = emp.TechicalSkills;
                var des = emp.DesignSkills;

                project.AssignEmployee(tech, des);
                emp.AssignProject(project);

                assignedEmployees.Add(template.userData as Employee);
                unAssignedEmployees.Remove(template.userData as Employee);

                unAssignedEmployeesList.Rebuild();

                onAssigndProjectToEmployee.Invoke(project);

            };

            return template;
        };

        unAssignedEmployeesList.bindItem = (item, index) =>
        {
            var emp = (unAssignedEmployees[index] as ProjectEmployee);
            var tech = emp.TechicalSkills;
            var des = emp.DesignSkills;

            item.userData = unAssignedEmployees[index];

            item.Q<VisualElement>("SpecializationIcon").style.backgroundImage = specializationLogo.style.backgroundImage;
            item.Q<Label>("EmployeeName").text = unAssignedEmployees[index].Name;
            item.Q<Label>("PrimarySkills").text = tech.ToString();
            item.Q<Label>("SecondarySkills").text = des.ToString();
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

        
        assignedEmployeesList.itemsSource = assignedEmployees;
        assignedEmployeesList.Rebuild();

        unAssignedEmployeesList.itemsSource = unAssignedEmployees;
        unAssignedEmployeesList.Rebuild();
    }

    void UpdateDynamicVisualElementData()
    {
        if (project == null) return;

        deadline.text = "";
        int intDeadline = project.Deadline;
        int loop = 3;
        while (loop-- > 0)
        {
            string toAdd = (intDeadline % 60).ToString();
            if (toAdd.Length == 1) toAdd = "0" + toAdd;
            deadline.text = (loop > 0 ? ":" : "") + toAdd + deadline.text;
            intDeadline /= 60;
        }
        

        technicalProgress.value = project.TechnicalProgress;
        technicalProgress.title = "Technical Progress " + technicalProgress.value + "%";
        
        designProgress.value = project.DesignProgress;
        designProgress.title = "Design Progress " + designProgress.value + "%";
    }

    void RebuildUnAssignedEmployeesList(Project project)
    {
        if (project == this.project)
        {
            root.style.display = DisplayStyle.None;
            return;
        }

        unAssignedEmployees = GameManager.GetUnAssignedEmployees(project);
        UpdateStaticVisualElementData();
        UpdateDynamicVisualElementData();
    }
}
