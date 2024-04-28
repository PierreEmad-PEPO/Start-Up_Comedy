using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ProjectManager : MonoBehaviour
{
    List<Project> projects;
    private ProjectEventInvoker OnProjectDone;
    private ProjectEventInvoker OnDeadlineEnd;
    VoidEventInvoker onProjectManagerOneSec;

    // Start is called before the first frame update
    void Start()
    {
        projects = GameManager.Projects;

        // Assign Events
        OnProjectDone = gameObject.AddComponent<ProjectEventInvoker>();
        EventManager.AddProjectEventInvoker(EventEnum.OnProjectDone, OnProjectDone);

        OnDeadlineEnd = gameObject.AddComponent<ProjectEventInvoker>();
        EventManager.AddProjectEventInvoker(EventEnum.OnDeadlineEnd, OnDeadlineEnd);

        onProjectManagerOneSec = gameObject.AddComponent<VoidEventInvoker>();
        EventManager.AddVoidEventInvoker(EventEnum.OnProjectMangerOneSec, onProjectManagerOneSec);

        StartCoroutine(UpdateProjects());
    }

    IEnumerator UpdateProjects()
    {
        while (true)
        {
            for (int i = 0; i < projects.Count; i++) 
            {
                var project = projects[i];
                project.UpdateProject();

                if (project.IsDone) 
                {
                    OnProjectDoneEffect(project);
                    projects.RemoveAt(i);
                    OnProjectDone.Invoke(project);
                    i--;
                    continue;
                }
                else if (project.IsDeadlineEnd)
                {
                    OnProjectDeadEffect(project);
                    projects.RemoveAt(i);
                    OnDeadlineEnd.Invoke(project);
                    i--;
                    continue;
                }                
            }
            if (projects.Count > 0)
                onProjectManagerOneSec.Invoke();
            yield return new WaitForSeconds(1);
        }
    }

    void OnProjectDoneEffect (Project project)
    {
        List<Employee> assigndEmployees = GameManager.GetAssignedEmployees(project);
        int extraSkills = 10; // for now
        int increasePopularity = 100;
        GameManager.StartUp.AddMoney(project.Price);
        foreach (ProjectEmployee employee in assigndEmployees)
        {
            employee.UpgradeSkills(extraSkills);
            employee.AssignProject(null);
        }
        GameManager.StartUp.IncreasePopularitySpeed(increasePopularity);
    }

    void OnProjectDeadEffect(Project project)
    {
        List<Employee> assigndEmployees = GameManager.GetAssignedEmployees(project);
        int decreasePopularity = 100; // for now
        GameManager.StartUp.PayMoney(project.PenalClause);
        foreach (ProjectEmployee employee in assigndEmployees)
        {
            employee.AssignProject(null);
        }
        GameManager.StartUp.DecreasePopularitySpeed(decreasePopularity);
    }
}
