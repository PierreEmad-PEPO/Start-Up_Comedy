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
                    projects.Remove(project);
                    OnProjectDone.Invoke(project);
                    project = null;
                    continue;
                }
                else if (project.IsDeadlineEnd)
                {
                    projects.Remove(project);
                    OnDeadlineEnd.Invoke(project);
                    project = null;
                    continue;
                }                
            }
            if (projects.Count > 0)
                onProjectManagerOneSec.Invoke();
            yield return new WaitForSeconds(1);
        }
    }
}
