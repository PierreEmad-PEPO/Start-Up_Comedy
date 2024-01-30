using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ProjectInfoUI : MonoBehaviour
{
    [SerializeField] private UIDocument uIDocument;

    private Project project;

    VisualElement root;
    VisualElement specializationLogo;
    Label projectName;
    Label deadline;
    Label price;
    Label penalClause;
    ProgressBar technicalProgress;
    ProgressBar designProgress;
    List<Employee> assignedEmployees = new List<Employee>();
    List<Employee> unAssignedEmployees = new List<Employee>();

    // Start is called before the first frame update
    void Start()
    {
        SetVisualElement();
        UpdateStaticVisualElementData();
        UpdateDynamicVisualElementData();
        root.style.display = DisplayStyle.None;
    }

    public void SetTheProject(Project project)
    {
        this.project = project;
        root.style.display = DisplayStyle.Flex;
        UpdateStaticVisualElementData();
        UpdateDynamicVisualElementData();
    }

    void SetVisualElement()
    {
        root = uIDocument.rootVisualElement;
        specializationLogo = root.Q<VisualElement>("Logo");
        projectName = root.Q<Label>("ProjectName");
        deadline = root.Q<Label>("Deadline");
        price = root.Q<Label>("Price");
        penalClause = root.Q<Label>("PenalClause");
        technicalProgress = root.Q<ProgressBar>("TechnicalProgress");
        designProgress = root.Q<ProgressBar>("DesignProgress");
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

        technicalProgress.highValue = project.MaxRequiredTechnicalSkills;

        designProgress.highValue = project.MaxRequiredDesignSkills;
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
        
        designProgress.value = project.DesignProgress;
    }
}
