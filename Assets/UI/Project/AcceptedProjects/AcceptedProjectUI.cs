using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class AcceptedProjectUI : MonoBehaviour
{
    [SerializeField] VisualTreeAsset assignedProjectCard;
    List<Project> projects;
    ListView assignedProjectListView;
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
    }

    void SetVisualElement ()
    {
        root = gameObject.GetComponent<UIDocument>().rootVisualElement;
        assignedProjectListView = root.Q<ListView>("AssignedList");
        root.Q<Button>("Exit").clicked += () =>
        {
            root.style.display = DisplayStyle.None;
        };
        InitAssignedListViwe();
    }

    void InitAssignedListViwe()
    {
        assignedProjectListView.makeItem = () =>
        {
            var temp = assignedProjectCard.Instantiate();
            return temp;
        };

        assignedProjectListView.bindItem = (item, index) =>
        {
            item.Q<Label>("Name").text = projects[index].Name;

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
        assignedProjectListView.fixedItemHeight = 110;
        assignedProjectListView.itemsSource = projects;

    }

    void AddAssignedProject(Project project) 
    {
        projects.Add(project);
        RebuidProjectsList(project);
    }

    void RebuidProjectsList(Project project)
    {
        assignedProjectListView.Rebuild();
    }
}
