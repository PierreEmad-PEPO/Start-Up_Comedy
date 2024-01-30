using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

public class AcceptedProjectUI : MonoBehaviour
{
    [SerializeField] VisualTreeAsset AcceptedProjectCard;
    List<Project> projects;
    ListView acceptedProjectListView;
    VisualElement root;

    // Start is called before the first frame update
    void Start()
    {
        projects = GameManager.Projects;
        SetVisualElement();
        root.style.display = DisplayStyle.None;
        EventManager.AddProjectEventListener(EventEnum.OnProjectAccepted,AddAssignedProject);
        EventManager.AddProjectEventListener(EventEnum.OnDeadlineEnd, RebuidProjectsList);
        EventManager.AddProjectEventListener(EventEnum.OnProjectDone, RebuidProjectsList);
        EventManager.AddVoidEventListener(EventEnum.OnProjectMangerOneSec, UpdateProgressBar);
    }

    void UpdateProgressBar()
    {
        Debug.Log("22222222");
        acceptedProjectListView.Rebuild();
    }

    void SetVisualElement ()
    {
        root = gameObject.GetComponent<UIDocument>().rootVisualElement;
        acceptedProjectListView = root.Q<ListView>("AcceptedList");
        root.Q<Button>("Exit").clicked += () =>
        {
            root.style.display = DisplayStyle.None;
        };
        InitAcceptedProjectListViwe();
    }

    void InitAcceptedProjectListViwe()
    {
        acceptedProjectListView.makeItem = () =>
        {
            var temp = AcceptedProjectCard.Instantiate();
            return temp;
        };

        acceptedProjectListView.bindItem = (item, index) =>
        {
            item.Q<Label>("Name").text = projects[index].Name;
            ProgressBar tichincal = item.Q<ProgressBar>("TechincalProgress");
            tichincal.value = projects[index].TechnicalProgress;
            tichincal.title = "Techincal Progress " + projects[index].TechnicalProgress.ToString() + "%";
            ProgressBar design = item.Q<ProgressBar>("DesignProgress");
            design.value = projects[index].DesignProgress;
            Debug.Log("Techincal Progress " + projects[index].TechnicalProgress.ToString() + "%");
            design.title = "Design Progress " + projects[index].DesignProgress.ToString() + "%";

            switch (projects[index].Specialization)
            {
                case ProjectSpecialization.Games:
                    item.Q<VisualElement>("Icon").style.backgroundImage = new StyleBackground(Resources.Load<Sprite>("EmployeeLists/Games"));
                    break;
                case ProjectSpecialization.Web:
                    item.Q<VisualElement>("Icon").style.backgroundImage = new StyleBackground(Resources.Load<Sprite>("EmployeeLists/Web"));
                    break;
                case ProjectSpecialization.Mobile: 
                    item.Q<VisualElement>("Icon").style.backgroundImage = new StyleBackground(Resources.Load<Sprite>("EmployeeLists/Android"));
                    break;
            }
        };
        acceptedProjectListView.fixedItemHeight = 150;
        acceptedProjectListView.itemsSource = projects;

    }

    void AddAssignedProject(Project project) 
    {
        projects.Add(project);
        RebuidProjectsList(project);
    }

    void RebuidProjectsList(Project project)
    {
        acceptedProjectListView.Rebuild();
    }
}
