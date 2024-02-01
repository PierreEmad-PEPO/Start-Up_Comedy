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
    ListView assignedEmployessList;
    ListView unAssignedEmployessList;

    List<Employee> assignedEmployees;
    List<Employee> unAssignedEmployees;

    // Start is called before the first frame update
    void Start()
    {
        SetVisualElement();
        UpdateStaticVisualElementData();
        UpdateDynamicVisualElementData();
        root.style.display = DisplayStyle.None;

        EventManager.AddVoidEventListener(EventEnum.OnProjectMangerOneSec, UpdateDynamicVisualElementData);
    }

    public void SetTheProject(Project project)
    {
        this.project = project;
        assignedEmployees = GameManager.GetAssignedEmployees(project);
        unAssignedEmployees = GameManager.GetUnAssignedEmployees(project);

        root.style.display = DisplayStyle.Flex;
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
        assignedEmployessList = root.Q<ListView>("AssignedEmployeesList");
        unAssignedEmployessList = root.Q<ListView>("UnAssignedEmployeesList");
    }

    void InitAssignedEmployeesList()
    {
        assignedEmployessList.makeItem = () =>
        {
            return assignedEmployeeCard.Instantiate();
        };

        assignedEmployessList.bindItem = (item, index) =>
        {
            item.Q<VisualElement>("SpecializationIcon").style.backgroundImage = specializationLogo.style.backgroundImage;
            item.Q<Label>("EmployeeName").text = assignedEmployees[index].Name;
            item.Q<Label>("PrimarySkills").text = (assignedEmployees[index] as ProjectEmployee).TechicalSkills.ToString();
            item.Q<Label>("SecondarySkills").text = (assignedEmployees[index] as ProjectEmployee).DesignSkills.ToString();
            item.Q<Button>("Delete").clicked += () =>
            {

            };
        };
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

        /* 
         * To Remember //\\
         * Assiging employee to project affects employee and project both
         */


        assignedEmployessList.itemsSource = assignedEmployees;
        assignedEmployessList.Rebuild();

        unAssignedEmployessList.itemsSource = unAssignedEmployees;
        unAssignedEmployessList.Rebuild();


    }

    void AssignedEmployeeDeleteButton(int index)
    {
        assignedEmployees.RemoveAt(index);
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
