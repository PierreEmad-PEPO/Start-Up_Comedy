using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ProjectOfferUI : MonoBehaviour
{
    private Label name;
    private Label specialization;
    private Label deadline;
    private Label price;
    private Label penalClause;
    private Label technicalSkills;
    private Label designSkills;

    private VisualElement root;

    private Timer projectTimer;
    private ProjectGenerator projectGenerator;

    private ProjectEventInvoker onProjectAccepted;

    void Start()
    {
        projectGenerator = gameObject.GetComponent<ProjectGenerator>();
        projectTimer = projectGenerator.ProjectTimer;
        SetVisualElement();
        EventManager.AddProjectEventListener(EventEnum.OnProjectGenerated, SetTheProjectWindo);

        onProjectAccepted = gameObject.AddComponent<ProjectEventInvoker>();
        EventManager.AddProjectEventInvoker(EventEnum.OnProjectAccepted, onProjectAccepted);

        root.style.display = DisplayStyle.None;

    }

    void SetVisualElement()
    {
        root = GetComponent<UIDocument>().rootVisualElement;
        name = root.Q<Label>("NameValue");
        specialization = root.Q<Label>("SpecializationValue");
        deadline = root.Q<Label>("DeadlineValue");
        price = root.Q<Label>("PriceValue");
        penalClause = root.Q<Label>("PenalClauseValue");
        technicalSkills = root.Q<Label>("TechnicalSkillsValue");
        designSkills = root.Q<Label>("DesignSkillsValue"); 

        root.Q<Button>("Refuse").clicked += () => 
        {
            root.style.display = DisplayStyle.None;
            projectTimer.Run();
        };

        root.Q<Button>("Accept").clicked += () =>
        {
            root.style.display = DisplayStyle.None;
            onProjectAccepted.Invoke(root.userData as Project);
            projectTimer.Run();
        };

    }

    void SetTheProjectWindo(Project project)
    {
        root.userData = project;
        name.text = project.Name;
        specialization.text = project.Specialization.ToString();
        deadline.text = project.Deadline.ToString() + "/h"; // forNow
        price.text = project.Price.ToString() + "$";
        penalClause.text = project.PenalClause.ToString() + "$";
        technicalSkills.text = project.RequiredTechnicalSkills.ToString();
        designSkills.text = project.RequiredDesignSkills.ToString();
        root.style.display = DisplayStyle.Flex;

    } 

}
